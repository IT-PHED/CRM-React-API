using System.Globalization;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Factory;
using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Factory.Consumer;
using Application.Abstractions.Messaging;
using Application.Complaints.CreateMeterComplaintsWithoutAccountNo;
using Application.Complaints.Dto;
using Domain.ActivityLog;
using Domain.Common;
using Domain.Complaint;
using Domain.Consumer;
using SharedKernel;

namespace Application.Complaints.CreateMeterComplaintsWithoutAccountNo;

internal sealed class CreateMeterComplaintWithoutAccountNoCommandHandler(
    IComplaintService complaintService,
    IUserContext userContext,
    IDateTimeProvider dateTimeProvider,
    IApplicationDbContext context) : ICommandHandler<CreateMeterComplaintWithoutAccountNoCommand, string>
{
    public async Task<Result<string>> Handle(CreateMeterComplaintWithoutAccountNoCommand command, CancellationToken cancellationToken)
    {
        
        ComplaintType? complaintType = await complaintService.GetComplaintTypeByIdAsync(command.ComplaintTypeId);
        if (complaintType is null)
        {
            return Result.Failure<string>(CommonErrors.CustomErrorMessage("Complaint type does not exist"));
        }

        ComplaintSubType? complaintSubType = complaintType.SubTypes.FirstOrDefault(x => x.Id.ToString() == command.ComplaintSubTypeId);
        if (complaintSubType is null)
        {
            return Result.Failure<string>(CommonErrors.CustomErrorMessage("Complaint sub type does not exist"));
        }

        //if (!await complaintService.CanRegisterConsumerComplaint(consumer.Id, complaintType.Id, complaintSubType.Id))
        //{
        //    return Result.Failure<string>(CommonErrors.CustomErrorMessage("Customer has unresolved complaint already"));
        //}

        DateTime currentMonthYear = dateTimeProvider.UtcNow;

        string ticket = await complaintService.GetTicket();
        string staffId = userContext.UserId;
        string staffEmail = userContext.UserEmail;

        try
        {
            var InsertComplaint = new InsertMasterComplaintDto2(
            ComplaintId: Guid.NewGuid().ToString(),
ComplaintTypeId: complaintType.Id,
ComplaintSubTypeId: complaintSubType.Id,
Status: EComplaintStatus.New.ToString(),
Source: command.Source,
Ticket: ticket,
DateGenerated: dateTimeProvider.UtcNow,
DateResolved: null,
Priority: command.Priority,
Remark: command.Remark ?? "",
ConsName: command.Customer_name,
ConsTelephoneNo: command.MobileNumber,
ConMaxDemand: command.Type,
ConsCategory: "R2",
ConsAddr1: command.Address,
ConEmailId: command.Email,
ConMobileNo: command.MobileNumber,
ConsType: command.Type,
Purpose: "",
MeterMake: "",
MeterDigit: 0,
CreatedBy: userContext.UserId,
CreatedDate: dateTimeProvider.UtcNow,
ModifiedBy: null,
ModifiedDate: null,
DepartmentId: command.DepartmentId,
ConstraintId: Guid.NewGuid().ToString(),
CorrectMeterReading: 0m,
MonthFrom: currentMonthYear,
MonthTo: currentMonthYear,
FilePath: null,
OminiName: command.Customer_name,
OminiPhone: command.MobileNumber,
OminiEmail: command.Email,
AssignTo: userContext.UserId,
MediaLink: null,
InsertConstraint: true,
AutoAllocate: true,
PostToOmini: true,
RegionId: command.RegionId

         );

            Console.WriteLine(InsertComplaint);

            ComplaintTransactionResponse2 insertComplaintFunc = await complaintService.InsertMasterComplaintTransactionWithoutAccountNoAsync(InsertComplaint);

            if (insertComplaintFunc.StatusCode != 0)
            {
                return Result.Failure<string>(CommonErrors.CustomErrorMessage($"DB Transacton failed to log complaint -- {insertComplaintFunc.StatusMessage}!"));
            }

            var activityLog = new UserActivityLog
            {
                CheckIn = dateTimeProvider.UtcNow,
                UserId = staffId,
                Action = EUserLogAction.LOG_COMPLAINT.ToString(),
                Module = EUserLogModule.LOG_COMPLAINT.ToString(),
                Status = EUserLogStatus.SUCCESS.ToString(),
                ReferenceId = Guid.NewGuid().ToString(),
                PageName = EUserLogPageName.COMPLAINT.ToString(),
                Desci = $"{staffEmail} just logged a User complaint from CRM portal with ticket number -- {ticket}",
                EmailAddr = staffEmail,
                EmailSent = false,
                AddOn = dateTimeProvider.UtcNow,
            };

            //if (!string.IsNullOrEmpty(command.AssignToEmail))
            //{
            //    string assignedName = appService.ConvertEmailToName(command.AssignToEmail);
            //    activityLog.Raise(new NotifyAssignedEmailEvent(command.AssignToEmail, ticket, assignedName, dateTimeProvider.UtcNow, command.Priority, complaintType.Name, staffEmail, command.Email, command.MobileNumber, command.Remark ?? ""));
            //}

            context.UserActivityLog.Add(activityLog);
            await context.SaveChangesAsync(cancellationToken);

            return $"{insertComplaintFunc.StatusMessage} with ticket number --- {ticket}";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
            return Result.Failure<string>(CommonErrors.CustomErrorMessage("something went wrong!"));
        }
    }
}

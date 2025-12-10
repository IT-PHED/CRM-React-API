using System.Globalization;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Factory;
using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Factory.Consumer;
using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using Domain.ActivityLog;
using Domain.Common;
using Domain.Complaint;
using Domain.Consumer;
using SharedKernel;

namespace Application.Complaints.CreateMeterComplaints;

internal sealed class CreateMeterComplaintCommandHandler(
    IConsumerService consumerService,
    IComplaintService complaintService,
    IUserContext userContext,
    IDateTimeProvider dateTimeProvider,
    IAppService appService,
    IApplicationDbContext context) : ICommandHandler<CreateMeterComplaintCommand, string>
{
    public async Task<Result<string>> Handle(CreateMeterComplaintCommand command, CancellationToken cancellationToken)
    {
        Consumer? consumer = await consumerService.GetByIdAsync(command.ConsumerNumber);

        if (consumer is null)
        {
            return Result.Failure<string>(CommonErrors.CustomErrorMessage("Customer does not exist"));
        }

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

        if (!await complaintService.CanRegisterConsumerComplaint(consumer.Id, complaintType.Id, complaintSubType.Id))
        {
            return Result.Failure<string>(CommonErrors.CustomErrorMessage("Customer has unresolved complaint already"));
        }

        DateTime currentMonthYear = dateTimeProvider.UtcNow;

        string ticket = await complaintService.GetTicket();
        string staffId = userContext.UserId;
        string staffEmail = userContext.UserEmail;

        try
        {
            var InsertComplaint = new InsertMasterComplaintDto(
             ComplaintId: Guid.NewGuid().ToString(),
             ComplaintTypeId: complaintType.Id,
             ComplaintSubTypeId: complaintSubType.Id,
             Ticket: ticket,
             AssignTo: command.AssignToStaffId,
             AutoAllocate: true,
             ConEmailId: command.Email,
             ConMaxDemand: consumer.Con_MaxDemand,
             ConMobileNo: command.MobileNumber,
             Source: command.Source,
             Priority: command.Priority,
             CreatedBy: userContext.UserId,
             ConRouteNumber: consumer.Con_RouteNumber,
             ConsAddr1: consumer.cons_addr1,
             ConsAddr2: consumer.cons_addr2,
             ConsAddr3: consumer.cons_addr3,
             ConsCategory: consumer.Cons_Category,
             ConsDivisionCode: consumer.Cons_DivisionCode,
             ConsMeterNo: consumer.Cons_MeterNo,
             ConsName: consumer.Cons_Name,
             ConsSectionCode: consumer.Cons_SectionCode,
             ConsTelephoneNo: consumer.Cons_TelephoneNo,
             ConstraintId: Guid.NewGuid().ToString(),
             ConsType: consumer.Cons_Type,
             ConsumerId: consumer.Cons_Acc,
             CreatedDate: dateTimeProvider.UtcNow,
             DateGenerated: dateTimeProvider.UtcNow,
             DepartmentId: command.DepartmentId,
             Dtr: consumer.Con_Transformer_Owner,
             CorrectMeterReading: command.correctMeterReading is int ? (decimal)command.correctMeterReading : 0,
             FilePath: command.File,
             MediaLink: command.MediaLink,
             MeterMake: consumer.Con_MeterMake,
             PostToOmini: true,
             Purpose: consumer.Purpose,
             Status: EComplaintStatus.New.ToString(),
             OminiEmail: command.Email,
             OminiName: consumer.Cons_Name,
             OminiPhone: command.ConsumerNumber,
             Remark: command.Remark ?? "",
             InsertConstraint: true,
             MonthFrom: currentMonthYear,
             MonthTo: currentMonthYear,
             DateResolved: null,
             MeterDigit: 0,
             ModifiedBy: null,
             ModifiedDate: null
         );

            Console.WriteLine(InsertComplaint);

            ComplaintTransactionResponse insertComplaintFunc = await complaintService.InsertMasterComplaintTransactionAsync(InsertComplaint);

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

            if (!string.IsNullOrEmpty(command.AssignToEmail))
            {
                string assignedName = appService.ConvertEmailToName(command.AssignToEmail);
                activityLog.Raise(new NotifyAssignedEmailEvent(command.AssignToEmail, ticket, assignedName, dateTimeProvider.UtcNow, command.Priority, complaintType.Name, staffEmail, command.Email, command.MobileNumber, command.Remark ?? ""));
            }

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

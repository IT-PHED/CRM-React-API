using System.Globalization;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Factory;
using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Factory.ComplaintResolution;
using Application.Abstractions.Messaging;
using Application.ComplaintResolution.Dto;
using Application.Complaints.GetComplaintById;
using Domain.ActivityLog;
using Domain.Common;
using Domain.Complaint;
using SharedKernel;

namespace Application.ComplaintResolution.ResolveNoPowerComplaint;

internal sealed class ResolveNoPowerComplaintCommandHandler(IComplaintService complaintService,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext,
    IComplaintResolutionService complaintResolutionService,
    IApplicationDbContext context,
    IAppService appService) : ICommandHandler<ResolveNoPowerComplaintCommand, GetComplaintByIdQueryResponse>
{
    public async Task<Result<GetComplaintByIdQueryResponse>> Handle(ResolveNoPowerComplaintCommand command, CancellationToken cancellationToken)
    {
        string complaintId = command.ComplaintId.ToString();
        string staffId = userContext.UserId;
        string staffEmail = userContext.UserEmail;

        GetComplaintByIdQueryResponse responseBody = await complaintService.GetComplaintById<GetComplaintByIdQueryResponse>(complaintId);

        if (responseBody is null)
        {
            return Result.Failure<GetComplaintByIdQueryResponse>(CommonErrors.CustomErrorMessage("Complaint does not exist!"));
        }

        if (string.Equals(responseBody.Status, EComplaintStatus.Approved.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            return Result.Failure<GetComplaintByIdQueryResponse>(CommonErrors.CustomErrorMessage("Ticket has been approved already!"));
        }

        try
        {
            var resolveComplaint = new UpdateComplaintDto
            {
                Id = complaintId,
                ComplaintTypeId = responseBody.Complainttypeid,
                ComplaintSubTypeId = responseBody.Complaintsubtypeid,
                ConsumerId = responseBody.Consumerid,
                cons_addr1 = responseBody.Cons_addr1,
                cons_addr2 = responseBody.Cons_addr2,
                cons_addr3 = responseBody.Cons_addr3,
                Cons_Category = responseBody.Cons_Category,
                Cons_Name = responseBody.CONS_NAME,
                Con_EmailId = responseBody.Con_emailid,
                Con_MobileNo = responseBody.Con_MobileNo,
                DateResolved = dateTimeProvider.UtcNow,
                Feedback = command.feedback,
                MeterDigit = responseBody.Meterdigit,
                MeterMake = responseBody.Metermake,
                ModifiedBy = userContext.UserId,
                Priority = responseBody.Priority,
                Purpose = responseBody.Purpose,
                Ticket = responseBody.Ticket,
                Remark = responseBody.Remark,
                ModifiedDate = dateTimeProvider.UtcNow,
                ResolvedBy = userContext.UserId,
                Source = responseBody.Source,
                Status = EComplaintStatus.Approved.ToString(),
            };

            await complaintResolutionService.UpdateComplaintV2(resolveComplaint);

            var activityLog = new UserActivityLog
            {
                CheckIn = dateTimeProvider.UtcNow,
                UserId = staffId,
                Action = EUserLogAction.APPROVE_COMPLAINT.ToString(),
                Module = EUserLogModule.APPROVE_COMPLAINT.ToString(),
                Status = EUserLogStatus.SUCCESS.ToString(),
                ReferenceId = Guid.NewGuid().ToString(),
                PageName = EUserLogPageName.COMPLAINT.ToString(),
                Desci = $"{staffEmail} approved a User complaint from CRM portal with ticket number -- {responseBody.Ticket}",
                EmailAddr = staffEmail,
                EmailSent = false,
                AddOn = dateTimeProvider.UtcNow,
            };

            if (!string.IsNullOrEmpty(responseBody.CreatedByEmail))
            {
                string createdName = appService.ConvertEmailToName(responseBody.CreatedByEmail);
                activityLog.Raise(new NotifyComplaintCreatorChangeInStatusDomainEvent(createdName, responseBody.CreatedByEmail, responseBody.Ticket, responseBody.Cons_Category, responseBody.Remark, EComplaintStatus.Approved.ToString(), staffId, dateTimeProvider.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), command.feedback));
            }

            context.UserActivityLog.Add(activityLog);
            await context.SaveChangesAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<GetComplaintByIdQueryResponse>(Domain.Common.CommonErrors.CustomErrorMessage("failed to update complaint"));
        }

        return responseBody;
    }
}

using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Factory;
using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Factory.ComplaintResolution;
using Application.Abstractions.Messaging;
using Application.Complaints.GetComplaintById;
using Domain.ActivityLog;
using Domain.Common;
using Domain.Complaint;
using SharedKernel;

namespace Application.ComplaintResolution.CloseNoPowerComplaint;

internal sealed class CloseNoPowerComplaintCommandHandler(
    IComplaintService complaintService,
    IUserContext userContext,
    IComplaintResolutionService complaintResolutionService,
    IApplicationDbContext context,
    IAppService appService,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CloseNoPowerComplaintCommand, GetComplaintByIdQueryResponse>
{
    public async Task<Result<GetComplaintByIdQueryResponse>> Handle(CloseNoPowerComplaintCommand command, CancellationToken cancellationToken)
    {
        string complaintId = command.ComplaintId.ToString();
        string staffId = userContext.UserId;
        string staffEmail = userContext.UserEmail;

        GetComplaintByIdQueryResponse complaint = await complaintService.GetComplaintById<GetComplaintByIdQueryResponse>(complaintId);

        if (complaint is null)
        {
            return Result.Failure<GetComplaintByIdQueryResponse>(Domain.Common.CommonErrors.CustomErrorMessage("Complaint does not exist!"));
        }

        if (!string.Equals(complaint.Status, EComplaintStatus.Approved.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            return Result.Failure<GetComplaintByIdQueryResponse>(CommonErrors.CustomErrorMessage("Ticket has not been approved already!"));
        }

        try
        {
            await complaintResolutionService.CloseConsumerComplaint(complaint.Id, staffId, EComplaintStatus.Closed.ToString(), command.Feedback);

            var activityLog = new UserActivityLog
            {
                CheckIn = dateTimeProvider.UtcNow,
                UserId = staffId,
                Action = EUserLogAction.APPROVE_COMPLAINT.ToString(),
                Module = EUserLogModule.APPROVE_COMPLAINT.ToString(),
                Status = EUserLogStatus.SUCCESS.ToString(),
                ReferenceId = Guid.NewGuid().ToString(),
                PageName = EUserLogPageName.COMPLAINT.ToString(),
                Desci = $"{staffEmail} closed a User complaint from CRM portal with ticket number -- {complaint.Ticket}",
                EmailAddr = staffEmail,
                EmailSent = false,
                AddOn = dateTimeProvider.UtcNow,
            };

            if (!string.IsNullOrEmpty(complaint.CreatedByEmail))
            {
                string createdName = appService.ConvertEmailToName(complaint.CreatedByEmail);
                activityLog.Raise(new CloseOutComplaintDomainEvent(complaint.Id, complaint.CreatedByEmail, createdName, complaint.Con_emailid, complaint.CONS_NAME, complaint.Con_MobileNo, complaint.Ticket, complaint.Remark, (DateTime)activityLog.CheckIn, complaint.Cons_Category, EComplaintStatus.Closed.ToString(), staffId, command.Feedback));
            }

            context.UserActivityLog.Add(activityLog);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<GetComplaintByIdQueryResponse>(CommonErrors.CustomErrorMessage("failed to close complaint"));
        }

        return complaint;
    }
}

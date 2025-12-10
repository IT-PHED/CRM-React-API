using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Factory;
using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using Domain.ActivityLog;
using Domain.Common;
using Domain.Complaint;
using SharedKernel;

namespace Application.Complaints.ReassignComplaint;

internal class ReassignComplaintCommandHandler(
    IComplaintService complaintService,
    IUserContext userContext,
    IDateTimeProvider dateTimeProvider,
    IAppService appService,
    IApplicationDbContext context) : ICommandHandler<ReassignComplaintCommand, string>
{
    public async Task<Result<string>> Handle(ReassignComplaintCommand command, CancellationToken cancellationToken)
    {
        string staffId = userContext.UserId;
        string staffEmail = userContext.UserEmail;
        try
        {
            ConsumerComplaintDto ticket = await complaintService.GetComplaintByTicketNumber(command.TicketId);

            if (ticket is null)
            {
                return Result.Failure<string>(Domain.Common.CommonErrors.CustomErrorMessage($"no Ticket with this id {command.TicketId} was found!"));
            }

            Domain.Complaint.ComplaintType complaintType = await complaintService.GetComplaintTypeByIdAsync(ticket.ComplaintTypeId);

            string response = await complaintService.ReassignComplaint(command.TicketId, command.ConsumerId, command.AssignStaffId, userContext.UserId);

            var activityLog = new UserActivityLog
            {
                CheckIn = dateTimeProvider.UtcNow,
                UserId = staffId,
                Action = EUserLogAction.REASSIGN_COMPLAINT.ToString(),
                Module = EUserLogModule.REASSIGN_COMPLAINT.ToString(),
                Status = EUserLogStatus.SUCCESS.ToString(),
                ReferenceId = Guid.NewGuid().ToString(),
                PageName = EUserLogPageName.COMPLAINT.ToString(),
                Desci = $"{staffEmail} reassigned a User complaint from CRM portal with ticket number -- {ticket} to this staff {command.AssignStaffId}",
                EmailAddr = staffEmail,
                EmailSent = false,
                AddOn = dateTimeProvider.UtcNow,
            };

            if (!string.IsNullOrEmpty(command.AssignEmail))
            {
                string assignedName = appService.ConvertEmailToName(command.AssignEmail);
                activityLog.Raise(new NotifyAssignedEmailEvent(command.AssignEmail, ticket.Ticket, assignedName, dateTimeProvider.UtcNow, "High", complaintType.Name, staffEmail, ticket.Email ?? "", ticket.ConsumerNumber, ticket.Remark ?? ""));
            }

            context.UserActivityLog.Add(activityLog);
            await context.SaveChangesAsync(cancellationToken);

            return $"ticket has been reassigned to staff {command.AssignStaffId}, {response}";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Failure<string>(Domain.Common.CommonErrors.CustomErrorMessage("Failed to reassign tickets"));
        }
    }
}

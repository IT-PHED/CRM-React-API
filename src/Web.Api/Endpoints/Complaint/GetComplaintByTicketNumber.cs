using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using Application.Complaints.GetComplaintById;
using Application.Complaints.GetComplaintByTicket;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Complaint;

public class GetComplaintByTicketNumber : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("complaint/ticket/{Id}", async (string Id, IQueryHandler<GetTicketByTicketQuery, ConsumerComplaintDto> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetTicketByTicketQuery(Id);

            Result<ConsumerComplaintDto> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<ConsumerComplaintDto>.Success(value, $"retrieved Ticket By ticket number {Id}")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Complaints).RequireAuthorization();
    }
}

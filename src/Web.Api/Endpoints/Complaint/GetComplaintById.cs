using Application.Abstractions.Messaging;
using Application.Complaints.GetComplaintById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Complaint;

public class GetComplaintById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("complaint/{Id:guid}", async (Guid Id, IQueryHandler<GetComplaintByIdQuery, GetComplaintByIdQueryResponse> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetComplaintByIdQuery(Id);

            Result<GetComplaintByIdQueryResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<GetComplaintByIdQueryResponse>.Success(value, $"Ticket {Id}")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Complaints).RequireAuthorization();
    }
}

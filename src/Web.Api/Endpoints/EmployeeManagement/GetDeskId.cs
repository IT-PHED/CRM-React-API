using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetDeskId;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetDeskId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/deskId", async (IQueryHandler<GetDeskIdQuery, IEnumerable<DeskIdDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetDeskIdQuery();

            Result<IEnumerable<DeskIdDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<DeskIdDto>>.Success(value, "All Desk Ids")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get All Desk Id Info");
    }
}

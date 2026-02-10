using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetDesignations;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetDesignations : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/designations", async (IQueryHandler<GetDesignationsQuery, IEnumerable<DesignationDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetDesignationsQuery();

            Result<IEnumerable<DesignationDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<DesignationDto>>.Success(value, "All Designations")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get All Designations");
    }
}

using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetSmsType;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetSmsType : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/sms-type", async (IQueryHandler<GetSmsTypeQuery, IEnumerable<SmsInfoDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetSmsTypeQuery();

            Result<IEnumerable<SmsInfoDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<SmsInfoDto>>.Success(value, "All Crm SMS Type")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get Crm Sms Type");
    }
}

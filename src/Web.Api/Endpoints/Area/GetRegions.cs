using Application.Abstractions.Messaging;
using Application.Area.GetRegions;
using Application.Configuration.Dto;
using SharedKernel;
using Web.Api.Endpoints.ComplaintResolution;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Area;

public class GetRegions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("area/regions", async (IQueryHandler<GetRegionsQuery, IEnumerable<EnumsResponseDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetRegionsQuery();

            Result<IEnumerable<EnumsResponseDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<EnumsResponseDto>>.Success(value, "All Regions")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Area).WithDescription("Fetch all the regions");
    }
}

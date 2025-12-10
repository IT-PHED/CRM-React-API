using Application.Abstractions.Messaging;
using Application.Area.Dto;
using Application.Area.GetArea;
using Application.Configuration.Dto;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Area;

public class GetArea : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("area", async (IQueryHandler<GetAreaQuery, AreaResponseDto> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetAreaQuery();

            Result<AreaResponseDto> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<AreaResponseDto>.Success(value, "All Areas")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Area).WithDescription("Fetch all the areas");
    }
}

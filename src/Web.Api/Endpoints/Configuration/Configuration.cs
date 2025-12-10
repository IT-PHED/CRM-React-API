using Application.Abstractions.Messaging;
using Application.Configuration.Dto;
using Application.Configuration.GetComplaintSubType;
using Application.Configuration.GetComplaintType;
using Application.Configuration.GetComplaintTypesAndSub;
using Application.Configuration.GetPriority;
using Application.Configuration.GetSources;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Configuration;

public class Configuration : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("configuration/priority", async (IQueryHandler<GetPriorityQuery, IEnumerable<EnumsResponseDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetPriorityQuery();

            Result<IEnumerable<EnumsResponseDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<EnumsResponseDto>>.Success(value, "All Priority")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Configuration).WithDescription("Fetch All priority");

        app.MapGet("configuration/sources", async (IQueryHandler<GetSourcesQuery, IEnumerable<EnumsResponseDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetSourcesQuery();

            Result<IEnumerable<EnumsResponseDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<EnumsResponseDto>>.Success(value, "All Source")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Configuration).WithDescription("Fetch All Source");

        app.MapGet("configuration/complaint-types-and-sub", async (IQueryHandler<GetComplaintTypesAndSubQuery, IEnumerable<ComplaintTypeResponseDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetComplaintTypesAndSubQuery();

            Result<IEnumerable<ComplaintTypeResponseDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<ComplaintTypeResponseDto>>.Success(value, "Complaints Types and sub types")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Configuration).WithDescription("Fetch All Complaint types and sub types");

        app.MapGet("configuration/complaint-types", async (IQueryHandler<GetComplaintTypeQuery, IEnumerable<ComplaintTypeResponseDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetComplaintTypeQuery();
            Result<IEnumerable<ComplaintTypeResponseDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<ComplaintTypeResponseDto>>.Success(value, "Complaints Types")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Configuration).WithDescription("Fetch All Complaint types");

        app.MapGet("configuration/complaint-subtypes", async (IQueryHandler<GetComplaintSubTypeQuery, IEnumerable<ComplaintSubTypeResponseDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetComplaintSubTypeQuery();

            Result<IEnumerable<ComplaintSubTypeResponseDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<ComplaintSubTypeResponseDto>>.Success(value, "All Complaints Sub Types")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Configuration).WithDescription("Fetch All Complaint Sub types");
    }
}

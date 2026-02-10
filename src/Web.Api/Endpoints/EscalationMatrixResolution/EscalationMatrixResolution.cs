using Application.Abstractions.Messaging;
using Application.EscalationMatrixResolution.CommentAndResolve;
using Application.EscalationMatrixResolution.Dto;
using Application.EscalationMatrixResolution.GetEscalationMatrixResolution;
using Application.EscalationMatrixResolution.MyOpenTickets;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EscalationMatrixResolution;

public class EscalationMatrixResolution : IEndpoint
{
    internal sealed record Request(List<EscalationMatrixResolutionDto> payload);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("escalation-matrix-resolution/comment-resolve", async ([FromBody] Request request,
            ICommandHandler<CommentAndResolveCommand, IEnumerable<EscalationMatrixResolutionDto>> handler, CancellationToken cancellationToken) =>
        {
            var command = new CommentAndResolveCommand(request.payload);

            Result<IEnumerable<EscalationMatrixResolutionDto>> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<EscalationMatrixResolutionDto>>.Success(value, "Escalation matrix resolved successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EscalationMatrixResolution).WithDescription("Command and resolve in Escalation Matrix Resolution");

        app.MapGet("escalation-matrix-resolution/my-open-tickets/{Uid}", async (string Uid, ICommandHandler<MyOpenTicketsCommand, IEnumerable<EscalationMatrixResolutionDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new MyOpenTicketsCommand(Uid);

            Result<IEnumerable<EscalationMatrixResolutionDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<EscalationMatrixResolutionDto>>.Success(value, "My Open ticket fetched successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EscalationMatrixResolution).WithDescription("Get My Open tickets Escalation Matrix Resolution");

        app.MapGet("escalation-matrix-resolution", async ([FromQuery] string ticket, [FromQuery] string Uid, ICommandHandler<GetEscalationMatrixResolutionCommand, IEnumerable<EscalationMatrixResolutionDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetEscalationMatrixResolutionCommand(Uid, ticket);

            Result<IEnumerable<EscalationMatrixResolutionDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<EscalationMatrixResolutionDto>>.Success(value, "Escalation matrix resolution fetched successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EscalationMatrixResolution).WithDescription("My Escalation Matrix Resolution");
    }
}

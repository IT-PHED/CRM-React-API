using Application.Abstractions.Messaging;
using Application.Auth.Dto;
using Application.Auth.LoginUser;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

public class LoginUser : IEndpoint
{
    public sealed record Request(string Username, string Password);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/login", async ([FromBody] Request request, ICommandHandler<LoginUserCommand, LoginResponse> handler, CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Username, request.Password);

            Result<LoginResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<LoginResponse>.Success(value, "User loggedin successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Auth);
    }
}

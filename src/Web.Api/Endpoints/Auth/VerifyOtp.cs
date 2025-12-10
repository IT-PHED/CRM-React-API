using Application.Abstractions.Messaging;
using Application.Auth.Dto;
using Application.Auth.VerifyUserOtp;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

public class VerifyOtp : IEndpoint
{
    public sealed record Request(string Email, string StaffId, string Otp);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/verify-otp", async ([FromBody] Request request, ICommandHandler<VerifyUserOtpCommand, VerifyOtpResponse> handler, CancellationToken cancellationToken) =>
        {
            var command = new VerifyUserOtpCommand(request.Email, request.StaffId, request.Otp);

            Result<VerifyOtpResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<VerifyOtpResponse>.Success(value, "success")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Auth);
    }
}

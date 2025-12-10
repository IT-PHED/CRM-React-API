using Application.Abstractions.Messaging;
using Application.Auth.Dto;

namespace Application.Auth.VerifyUserOtp;

public sealed record VerifyUserOtpCommand(string Email, string StaffId, string Otp) : ICommand<VerifyOtpResponse>;

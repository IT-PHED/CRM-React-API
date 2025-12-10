using Application.Abstractions.Messaging;
using Application.Auth.Dto;

namespace Application.Auth.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<LoginResponse>;

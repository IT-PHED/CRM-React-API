using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetSmsType;

public sealed record GetSmsTypeQuery : IQuery<IEnumerable<SmsInfoDto>>;

using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetDeskId;

public sealed record GetDeskIdQuery : IQuery<IEnumerable<DeskIdDto>>;

using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetGroupPrivileges;

public sealed record GetGroupPrivilegesQuery(int GroupId) : IQuery<IEnumerable<ScreenPrivilegeDto>>;

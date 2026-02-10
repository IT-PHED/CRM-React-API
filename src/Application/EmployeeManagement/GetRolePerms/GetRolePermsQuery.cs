using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetRolePerms;

public sealed record GetRolePermsQuery : IQuery<IEnumerable<CrmRoleMenu>>;

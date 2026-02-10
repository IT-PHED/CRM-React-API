using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetGroupMemberPermissions;

public sealed record GetGroupMemberPermissionsQuery(string UserId) : IQuery<CrmUserPermissionDto>;

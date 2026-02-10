using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetUserGroup;

public sealed record GetUserGroupQuery(string? userId = null) : IQuery<UserGroupDto>;

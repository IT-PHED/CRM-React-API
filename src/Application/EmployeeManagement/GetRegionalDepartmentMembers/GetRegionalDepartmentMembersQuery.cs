using Application.Abstractions.Messaging;
using Application.Auth.Dto;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetRegionalDepartmentMembers;

public sealed record GetRegionalDepartmentMembersQuery(string DepartmentId, string AccountNumber) : IQuery<IEnumerable<UserRegionalProfileDto>>;

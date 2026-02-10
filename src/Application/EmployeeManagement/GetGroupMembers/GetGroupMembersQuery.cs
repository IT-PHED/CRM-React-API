using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetGroupMembers;

public sealed record GetGroupMembersQuery(int GroupId) : IQuery<IEnumerable<UserRegionalProfileDto>>;

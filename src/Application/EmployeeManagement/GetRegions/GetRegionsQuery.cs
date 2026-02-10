using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetRegions;

public sealed record GetRegionsQuery : IQuery<IEnumerable<object>>;

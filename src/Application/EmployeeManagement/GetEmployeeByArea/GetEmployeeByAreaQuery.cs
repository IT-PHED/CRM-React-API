using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetEmployeeByArea;

public sealed record GetEmployeeByAreaQuery(string Ibc, string? Bsc = null) : IQuery<IEnumerable<EmployeeByIbcBscDto>>;

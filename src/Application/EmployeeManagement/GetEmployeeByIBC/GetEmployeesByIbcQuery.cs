using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetEmployeeByIBC;

public sealed record GetEmployeesByIbcQuery(string Ibc) : IQuery<IEnumerable<EmployeeByIbcBscDto>>;

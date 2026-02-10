using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;

namespace Application.EmployeeManagement.GetEmployees;

public sealed record GetEmployeesQuery() : IQuery<IEnumerable<EmployeeDto>>;

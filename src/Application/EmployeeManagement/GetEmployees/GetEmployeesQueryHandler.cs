using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Dapper;
using SharedKernel;

namespace Application.EmployeeManagement.GetEmployees;

internal sealed class GetEmployeesQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetEmployeesQuery, IEnumerable<EmployeeDto>>
{
    public async Task<Result<IEnumerable<EmployeeDto>>> Handle(GetEmployeesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<EmployeeDto> employees = await employeeManagementService.EmployeesAsync();

            if (employees == null)
            {
                return Result.Failure<IEnumerable<EmployeeDto>>(Domain.Common.CommonErrors.CustomErrorMessage("No employee data was found!"));
            }

            return employees.AsList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<EmployeeDto>>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch employee data"));
        }
    }
}

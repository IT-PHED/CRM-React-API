using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Dapper;
using SharedKernel;

namespace Application.EmployeeManagement.GetEmployeeByIBC;

internal sealed class GetEmployeesByIbcQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetEmployeesByIbcQuery, IEnumerable<EmployeeByIbcBscDto>>
{
    public async Task<Result<IEnumerable<EmployeeByIbcBscDto>>> Handle(GetEmployeesByIbcQuery query, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(query.Ibc))
        {
            return Result.Failure<IEnumerable<EmployeeByIbcBscDto>>(Domain.Common.CommonErrors.CustomErrorMessage("Ibc parameter is required!"));
        }

        try
        {
            IReadOnlyList<EmployeeByIbcBscDto> employees = await employeeManagementService.EmployeesByIBCAsync(query.Ibc);

            if (employees == null)
            {
                return Result.Failure<IEnumerable<EmployeeByIbcBscDto>>(Domain.Common.CommonErrors.CustomErrorMessage("No employee data was found!"));
            }

            return employees.AsList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<EmployeeByIbcBscDto>>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch employee data"));
        }
    }
}

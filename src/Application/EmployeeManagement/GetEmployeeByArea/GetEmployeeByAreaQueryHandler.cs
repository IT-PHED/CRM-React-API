using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Dapper;
using SharedKernel;

namespace Application.EmployeeManagement.GetEmployeeByArea;

internal sealed class GetEmployeeByAreaQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetEmployeeByAreaQuery, IEnumerable<EmployeeByIbcBscDto>>
{
    public async Task<Result<IEnumerable<EmployeeByIbcBscDto>>> Handle(GetEmployeeByAreaQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<EmployeeByIbcBscDto> employees = await employeeManagementService.EmployeesByIBCAndBSCAsync(query.Ibc, query.Bsc ?? "");

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

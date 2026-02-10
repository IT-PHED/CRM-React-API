using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using SharedKernel;

namespace Application.EmployeeManagement.GetRegions;

internal sealed class GetRegionsQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetRegionsQuery, IEnumerable<object>>
{
    public async Task<Result<IEnumerable<object>>> Handle(GetRegionsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<EmpRegionsDto>? regions = await employeeManagementService.GetRegions();

            if (regions is null)
            {
                return Result.Failure<IEnumerable<object>>(Domain.Common.CommonErrors.CustomErrorMessage("No data was found"));
            }

            var regionsList = regions.Select(c => new
            {
                id = c.ID,
                Region = c.REGIONAL_OFFICES.Trim()
            }).ToList();

            regionsList.Insert(0, new
            {
                id = "0",
                Region = "Head Office"
            });

            return regionsList;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<object>>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch regions"));
        }
    }
}

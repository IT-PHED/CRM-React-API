using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using SharedKernel;

namespace Application.EmployeeManagement.GetDesignations;

internal sealed class GetDesignationsQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetDesignationsQuery, IEnumerable<DesignationDto>>
{
    public async Task<Result<IEnumerable<DesignationDto>>> Handle(GetDesignationsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<DesignationDto> designationDtos = await employeeManagementService.GetDesignationsAsync();

            if (designationDtos == null)
            {
                return Result.Failure<IEnumerable<DesignationDto>>(Domain.Common.CommonErrors.CustomErrorMessage("No Designation was found!"));
            }

            return designationDtos.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<DesignationDto>>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch designation data"));
        }
    }
}

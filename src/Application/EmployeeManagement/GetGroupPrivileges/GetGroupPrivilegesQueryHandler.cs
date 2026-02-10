using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Dapper;
using SharedKernel;

namespace Application.EmployeeManagement.GetGroupPrivileges;

internal class GetGroupPrivilegesQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetGroupPrivilegesQuery, IEnumerable<ScreenPrivilegeDto>>
{
    public async Task<Result<IEnumerable<ScreenPrivilegeDto>>> Handle(GetGroupPrivilegesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<ScreenPrivilegeDto> grpPrivileges = await employeeManagementService.GetGroupPrivileges(query.GroupId);

            if (grpPrivileges == null)
            {
                return Result.Failure<IEnumerable<ScreenPrivilegeDto>>(Domain.Common.CommonErrors.CustomErrorMessage("No group privilege was found!"));
            }

            return grpPrivileges.AsList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<ScreenPrivilegeDto>>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch group privilege data"));
        }
    }
}

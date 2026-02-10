using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using SharedKernel;

namespace Application.EmployeeManagement.GetGroupMemberPermissions;

internal sealed class GetGroupMemberPermissionsQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetGroupMemberPermissionsQuery, CrmUserPermissionDto>
{
    public async Task<Result<CrmUserPermissionDto>> Handle(GetGroupMemberPermissionsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            CrmUserPermissionDto crmPerm = await employeeManagementService.GetEmployeeCRMPermission(query.UserId);

            if (crmPerm == null)
            {
                return Result.Failure<CrmUserPermissionDto>(Domain.Common.CommonErrors.CustomErrorMessage("No crm permission was found!"));
            }

            return crmPerm;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<CrmUserPermissionDto>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch crm user permission data"));
        }
    }
}

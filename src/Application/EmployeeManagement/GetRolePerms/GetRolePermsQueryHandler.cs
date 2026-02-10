using Application.Abstractions.Factory.EmployeeManagement;
using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Dapper;
using SharedKernel;

namespace Application.EmployeeManagement.GetRolePerms;

internal sealed class GetRolePermsQueryHandler(IEmployeeManagementService employeeManagementService) : IQueryHandler<GetRolePermsQuery, IEnumerable<CrmRoleMenu>>
{
    public async Task<Result<IEnumerable<CrmRoleMenu>>> Handle(GetRolePermsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<CrmRoleMenu> user = await employeeManagementService.GetCrmRoleMenus();

            if (user == null)
            {
                return Result.Failure<IEnumerable<CrmRoleMenu>>(Domain.Common.CommonErrors.CustomErrorMessage("No user crm role was found!"));
            }

            return user.AsList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<CrmRoleMenu>>(Domain.Common.CommonErrors.CustomErrorMessage("failed to user crm role menu"));
        }
    }
}

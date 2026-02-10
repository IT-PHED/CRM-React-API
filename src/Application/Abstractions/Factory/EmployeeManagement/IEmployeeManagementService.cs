using Application.EmployeeManagement.Dto;
using Domain.User;

namespace Application.Abstractions.Factory.EmployeeManagement;

public interface IEmployeeManagementService
{
    Task<IReadOnlyList<EmployeeDto>> EmployeesAsync();
    Task<IReadOnlyList<EmployeeByIbcBscDto>> EmployeesByIBCAsync(string ibc);
    Task<IReadOnlyList<EmployeeByIbcBscDto>> EmployeesByIBCAndBSCAsync(string ibc, string bsc);
    Task<IReadOnlyList<SmsInfoDto>> GetSmsType();
    Task<IReadOnlyList<EmpRegionsDto>> GetRegions();
    Task<IReadOnlyList<DeskIdDto>> GetDeskId();
    Task<IReadOnlyList<CrmRoleMenu>> GetCrmRoleMenus();
    Task<UserGroupDto> GetUserGroup(string userId);
    Task<object> UpdateUserCRMRole(string staffId, string newRole);
    Task<IReadOnlyList<ScreenPrivilegeDto>> GetGroupPrivileges(int groupid);
    Task<IReadOnlyList<UserProfile>> GetRegionalDepartmentMember(string departmentId, string accountNumber);
    Task<IReadOnlyList<UserProfile>> GetGroupMembers(int groupid);
    Task<CrmUserPermissionDto> GetEmployeeCRMPermission(string userId);
    Task<IReadOnlyList<DesignationDto>> GetDesignationsAsync();
}

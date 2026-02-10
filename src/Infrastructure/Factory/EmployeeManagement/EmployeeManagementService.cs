using System.Data;
using Application.Abstractions.Data;
using Application.Abstractions.Factory.EmployeeManagement;
using Application.EmployeeManagement.Dto;
using Dapper;
using Domain.User;
using Infrastructure.Utils;
using Oracle.ManagedDataAccess.Client;
using SharedKernel;

namespace Infrastructure.Factory.EmployeeManagement;

internal sealed class EmployeeManagementService(IUnitOfWork unitOfWork) : IEmployeeManagementService
{
    public async Task<IReadOnlyList<EmployeeDto>> EmployeesAsync()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<EmployeeDto> employees = await unitOfWork.Connection.QueryAsync<EmployeeDto>(EmployeeStoreProcedureNames.GET_ALL_EMPLOYEE, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return employees.AsList();
    }

    /// <summary>
    /// Get all employees from an AREA
    /// </summary>
    /// <param name="ibc">the ibc zone</param>
    /// <param name="bsc">the bsc zone</param>
    /// <returns>All employees from an AREA</returns>
    public async Task<IReadOnlyList<EmployeeByIbcBscDto>> EmployeesByIBCAndBSCAsync(string ibc, string bsc)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_ibc", ibc);
        param.Add("p_bsc", bsc);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<EmployeeByIbcBscDto> employees = await unitOfWork.Connection.QueryAsync<EmployeeByIbcBscDto>(EmployeeStoreProcedureNames.GET_EMPLOYEE_IBC_BSC, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return employees.AsList();
    }

    /// <summary>
    /// Get all employees from an IBC
    /// </summary>
    /// <param name="ibc">the zone</param>
    /// <returns>All employees from an IBC</returns>
    public async Task<IReadOnlyList<EmployeeByIbcBscDto>> EmployeesByIBCAsync(string ibc)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_ibc", ibc);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<EmployeeByIbcBscDto> employees = await unitOfWork.Connection.QueryAsync<EmployeeByIbcBscDto>(EmployeeStoreProcedureNames.GET_EMPLOYEE_IBC_BSC, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return employees.AsList();
    }

    public async Task<IReadOnlyList<CrmRoleMenu>> GetCrmRoleMenus()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);

        IEnumerable<CrmRoleMenu> roleMenu = await unitOfWork.Connection.QueryAsync<CrmRoleMenu>(EmployeeStoreProcedureNames.GET_CRM_ROLE_MENU, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return roleMenu.AsList();
    }

    public async Task<IReadOnlyList<DesignationDto>> GetDesignationsAsync()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<DesignationDto> designations = await unitOfWork.Connection.QueryAsync<DesignationDto>(EmployeeStoreProcedureNames.GET_ALL_DESIGNATIONS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return designations.AsList();
    }

    public async Task<IReadOnlyList<DeskIdDto>> GetDeskId()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorNamePResult, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add("P_ACTION", "List");

        IEnumerable<DeskIdDto> deskIdList = await unitOfWork.Connection.QueryAsync<DeskIdDto>(EmployeeStoreProcedureNames.GET_ALL_DESKID, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return deskIdList.AsList();
    }

    public async Task<CrmUserPermissionDto> GetEmployeeCRMPermission(string userId)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_userid", userId);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);

        CrmUserPermissionDto employee = await unitOfWork.Connection.QueryFirstOrDefaultAsync<CrmUserPermissionDto>(EmployeeStoreProcedureNames.GET_USER_CRM_PERMISSIONS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return employee;
    }

    public async Task<IReadOnlyList<UserProfile>> GetGroupMembers(int groupid)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_groupId", groupid);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);

        IEnumerable<UserProfile> escalators = await unitOfWork.Connection.QueryAsync<UserProfile>(EmployeeStoreProcedureNames.GET_ALL_GROUP_MEMBERS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return escalators.AsList();
    }

    public async Task<IReadOnlyList<ScreenPrivilegeDto>> GetGroupPrivileges(int groupid)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_group", groupid);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<ScreenPrivilegeDto> result = await unitOfWork.Connection.QueryAsync<ScreenPrivilegeDto>(EmployeeStoreProcedureNames.GET_GROUP_PRIVILEGES, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return result.AsList();
    }

    public async Task<IReadOnlyList<UserProfile>> GetRegionalDepartmentMember(string departmentId, string accountNumber)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_DEPARTMENTID", departmentId);
        param.Add("P_ACCOUNT_NUMBER", accountNumber);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);

        IEnumerable<UserProfile> escalators = await unitOfWork.Connection.QueryAsync<UserProfile>(EmployeeStoreProcedureNames.GET_REGIONAL_DEPARTMENT_MEMBERS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return escalators.AsList();
    }

    public async Task<IReadOnlyList<EmpRegionsDto>> GetRegions()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);

        IEnumerable<EmpRegionsDto> regions = await unitOfWork.Connection.QueryAsync<EmpRegionsDto>(EmployeeStoreProcedureNames.GET_REGIONS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return regions.AsList();
    }

    public async Task<IReadOnlyList<SmsInfoDto>> GetSmsType()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<SmsInfoDto> smsType = await unitOfWork.Connection.QueryAsync<SmsInfoDto>(EmployeeStoreProcedureNames.GET_SMS_TITLES, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return smsType.AsList();
    }

    public async Task<UserGroupDto> GetUserGroup(string userId)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_userId", userId);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<UserGroupDto> result = await unitOfWork.Connection.QueryAsync<UserGroupDto>(EmployeeStoreProcedureNames.GET_USER_GROUP, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return result.FirstOrDefault();
    }

    public async Task<object> UpdateUserCRMRole(string staffId, string newRole)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_STAFF_ID", staffId);
        param.Add("P_NEW_CRM_ROLE", newRole);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);

        IEnumerable<dynamic> user = await unitOfWork.Connection.QueryAsync(EmployeeStoreProcedureNames.UPDATE_CRM_USER_PERM, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return user;
    }
}

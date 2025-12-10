using System.Data;
using Application.Abstractions.Data;
using Application.Abstractions.Factory;
using Dapper;
using Domain.User;
using Infrastructure.Utils;
using Oracle.ManagedDataAccess.Client;
using SharedKernel;

namespace Infrastructure.Factory;

internal sealed class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public async Task<UserProfile> GetUser(string emailOrStaffId)
    {
        var param = new OracleDynamicParameter();
        param.Add("c_select", OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add("p_EMAIL", emailOrStaffId);

        UserProfile? user = await unitOfWork.Connection.QueryFirstOrDefaultAsync<UserProfile>(
            UserStoreProcedureNames.GET_USER_BY_EMAIL, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

        return user;
    }

    public async Task updateOTP(string email, string staffId, string otp)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_email", email, DbType.String, ParameterDirection.Input);
        param.Add("p_staffId", staffId, DbType.String, ParameterDirection.Input);
        param.Add("p_code", otp, DbType.String, ParameterDirection.Input);
        param.Add("p_action", "update-otp", DbType.String, ParameterDirection.Input);
        param.Add("p_oldPassword", null, DbType.String, ParameterDirection.Input);
        param.Add("p_password", null, DbType.String, ParameterDirection.Input);
        param.Add("p_result", OracleDbType.RefCursor, ParameterDirection.Output);

        await unitOfWork.Connection.QueryFirstOrDefaultAsync(UserStoreProcedureNames.UPDATE_USER_OTP, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
    }

    public async Task verifyOtp(string email, string staffId, string otp)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_email", email, DbType.String, ParameterDirection.Input);
        param.Add("p_staffId", staffId, DbType.String, ParameterDirection.Input);
        param.Add("p_code", otp, DbType.String, ParameterDirection.Input);
        param.Add("p_action", "verify-otp", DbType.String, ParameterDirection.Input);
        param.Add("p_oldPassword", null, DbType.String, ParameterDirection.Input);
        param.Add("p_password", null, DbType.String, ParameterDirection.Input);
        param.Add("p_result", OracleDbType.RefCursor, ParameterDirection.Output);
        await unitOfWork.Connection.QueryFirstOrDefaultAsync(UserStoreProcedureNames.UPDATE_USER_OTP, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
    }
}

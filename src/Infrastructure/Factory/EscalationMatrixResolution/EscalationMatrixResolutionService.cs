using System.Data;
using System.Text;
using Application.Abstractions.Data;
using Application.Abstractions.Factory.EscalationMatrixResolution;
using Application.EscalationMatrixResolution.Dto;
using Dapper;
using Infrastructure.Utils;
using Oracle.ManagedDataAccess.Client;
using SharedKernel;

namespace Infrastructure.Factory.EscalationMatrixResolution;

internal sealed class EscalationMatrixResolutionService(IUnitOfWork unitOfWork) : IEscalationMatrixResolutionService
{
    public async Task<IReadOnlyList<EscalationMatrixResolutionDto>> CommentAndGetSLA(EscalationMatrixResolutionDto model)
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add("P_TICKET", model.TICKET);
        param.Add("P_EMPID", model.EMPID);
        param.Add("P_COMMENT", model.ESCALATION_REMARK);
        param.Add("P_CLOSE", model.ShouldCloseTicket);
        IEnumerable<EscalationMatrixResolutionDto> slaClosureLog = new List<EscalationMatrixResolutionDto>(); // Initialize as an empty list

        try
        {
            slaClosureLog = await unitOfWork.Connection.QueryAsync<EscalationMatrixResolutionDto>("SPN_CRM_SLA_ESCALATION_MATRIX_COMMENT_AND_OR_CLOSE", param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
            slaClosureLog = slaClosureLog.OrderBy(dto => dto.levelname);
            // Handle the result if needed
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception occurred: {ex.Message}");
        }

        return slaClosureLog.AsList();
    }

    public string decodeString(string base64Encoded)
    {
        byte[] decodedData = Convert.FromBase64String(base64Encoded);
        string decodedString = Encoding.UTF8.GetString(decodedData);
        return decodedString;
    }

    public async Task<IReadOnlyList<EscalationMatrixResolutionDto>> GetMyOpenSLATickets(string uid)
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add("P_EMPID", uid);
        IEnumerable<EscalationMatrixResolutionDto> slaClosureLog = new List<EscalationMatrixResolutionDto>(); // Initialize as an empty list

        try
        {
            slaClosureLog = await unitOfWork.Connection.QueryAsync<EscalationMatrixResolutionDto>("SPN_CRM_GET_SLA_ESCALATION_MATRIX_MY_OPEN_TICKETS", param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
            slaClosureLog = slaClosureLog.OrderBy(dto => dto.levelname);
            // Handle the result if needed
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception occurred: {ex.Message}");
        }
        return slaClosureLog.AsList();
    }

    public async Task<IReadOnlyList<EscalationMatrixResolutionDto>> GetSLAForTicket(string ticket)
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);

        param.Add("P_TICKET", ticket);
        IEnumerable<EscalationMatrixResolutionDto> slaClosureLog = new List<EscalationMatrixResolutionDto>(); // Initialize as an empty list

        try
        {
            slaClosureLog = await unitOfWork.Connection.QueryAsync<EscalationMatrixResolutionDto>("SPN_CRM_SLA_ESCALATION_MATRIX_LIST_FOR_RESOLUTION", param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
            slaClosureLog = slaClosureLog.OrderBy(dto => dto.levelname);
            // Handle the result if needed
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception occurred: {ex.Message}");
        }
        return slaClosureLog.AsList();
    }
}

using System.Data;
using System.Globalization;
using Application.Abstractions.Data;
using Application.Abstractions.Factory.Dashboard;
using Application.Dashboard.Dto;
using Dapper;
using Infrastructure.Utils;
using Oracle.ManagedDataAccess.Client;
using SharedKernel;

namespace Infrastructure.Factory.Dashboard;

internal sealed class DashboardService(IUnitOfWork unitOfWork) : IDashboardService
{
    public async Task<List<DashboardDto>> GetCategoryWiseSummary(DateTime fromDate, DateTime toDate, string? divId = "0")
    {
        string divID = divId?.ToString();
        string newFromDate = fromDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        string newToDate = toDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add("p_fromdate", newFromDate);
        param.Add("p_todate", newToDate);
        param.Add("p_division", divID);
        param.Add("p_bsc", '0');
        IEnumerable<DashboardDto> ticketsCatg = await unitOfWork.Connection.QueryAsync<DashboardDto>(DashboardStoreProcedureNames.GET_CATEGORY_TICKETS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return ticketsCatg.AsList();
    }

    public async Task<List<DashboardDto>> GeteDateWiseSummary(DateTime fromDate, DateTime toDate, string? divId = "0")
    {
        string divID = divId?.ToString();
        string newFromDate = fromDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        string newToDate = toDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add("p_fromdate", newFromDate);
        param.Add("p_todate", newToDate);
        param.Add("p_division", divID);
        param.Add("p_bsc", '0');
        IEnumerable<DashboardDto> ticketDateWise = await unitOfWork.Connection.QueryAsync<DashboardDto>(DashboardStoreProcedureNames.GET_DATEWISE_TICKETS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return ticketDateWise.AsList();
    }

    public async Task<List<DashboardDto>> GeteLocationWiseSummary(DateTime fromDate, DateTime toDate, string? divId = "0")
    {
        string divID = divId?.ToString();
        string newFromDate = fromDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        string newToDate = toDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add("p_fromdate", newFromDate);
        param.Add("p_todate", newToDate);
        param.Add("p_division", divID);
        param.Add("p_bsc", '0');
        IEnumerable<DashboardDto> ticketLocWise = await unitOfWork.Connection.QueryAsync<DashboardDto>(DashboardStoreProcedureNames.GET_LOCATIONWISE_TICKETS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return ticketLocWise.AsList();
    }

    public async Task<List<DashboardDto>> GetSlaCountSummary(DateTime fromDate, DateTime toDate, string? divId = "0")
    {
        string divID = divId?.ToString();
        string newFromDate = fromDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        string newToDate = toDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add("p_fromdate", newFromDate);
        param.Add("p_todate", newToDate);
        param.Add("p_division", divID);
        param.Add("p_bsc", '0');
        IEnumerable<DashboardDto> ticketSlaDuration = await unitOfWork.Connection.QueryAsync<DashboardDto>(DashboardStoreProcedureNames.GET_SLA_COUNT_SUMMARY, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return ticketSlaDuration.AsList();
    }

    public async Task<List<DashboardDto>> GetSlaDIVCountSummary(DateTime fromDate, DateTime toDate, string? divId = "0")
    {
        string divID = divId?.ToString();
        string newFromDate = fromDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        string newToDate = toDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add("p_fromdate", newFromDate);
        param.Add("p_todate", newToDate);
        param.Add("p_division", divID);
        param.Add("p_bsc", '0');
        IEnumerable<DashboardDto> ticketSlaDuration = await unitOfWork.Connection.QueryAsync<DashboardDto>(DashboardStoreProcedureNames.GET_SLA_DIVCOUNT_SUMMARY, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return ticketSlaDuration.AsList();
    }

    public async Task<List<DashboardDto>> GetSlaDurationSummary(DateTime fromDate, DateTime toDate, string? divId = "0")
    {
        string divID = divId?.ToString();
        string newFromDate = fromDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        string newToDate = toDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add("p_fromdate", newFromDate);
        param.Add("p_todate", newToDate);
        param.Add("p_division", divID);
        param.Add("p_bsc", '0');
        IEnumerable<DashboardDto> ticketSlaDuration = await unitOfWork.Connection.QueryAsync<DashboardDto>(DashboardStoreProcedureNames.GET_SLADURATION_TICKETS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return ticketSlaDuration.AsList();
    }

    public async Task<List<DashboardDto>> GetTicketSummary(string? divId = "0", DateTime? fromDate = null, DateTime? toDate = null)
    {
        string divID = divId?.ToString();
        string newFromDate = fromDate?.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        string newToDate = toDate?.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add("p_fromdate", newFromDate);
        param.Add("p_todate", newToDate);
        param.Add("p_division", divID);
        param.Add("p_bsc", '0');

        IEnumerable<DashboardDto> ticketSummary = await unitOfWork.Connection.QueryAsync<DashboardDto>(DashboardStoreProcedureNames.GET_TICKET_SUMMARY, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return ticketSummary.AsList();
    }
}

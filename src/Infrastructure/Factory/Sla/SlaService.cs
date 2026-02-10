using System.Data;
using System.Globalization;
using Application.Abstractions.Data;
using Application.Abstractions.Factory.SLA;
using Application.Sla.Dto;
using Dapper;
using Infrastructure.Utils;
using Oracle.ManagedDataAccess.Client;
using SharedKernel;

namespace Infrastructure.Factory.Sla;

internal sealed class SlaService(IUnitOfWork unitOfWork) : ISlaService
{
    public async Task<IReadOnlyList<SlaDto>> GetSLAReport(string ibc, string bsc, string category, string subCategory, DateTime fromDate, DateTime toDate)
    {
        string newFromDate = fromDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        string newToDate = toDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);

        param.Add("P_IBC", ibc);
        param.Add("P_BSC", bsc);
        param.Add("P_CATEGORY", category);
        param.Add("P_SUBCATEGORY", subCategory);
        param.Add("P_FROMDATE", newFromDate);
        param.Add("P_TODATE", newToDate);


        IEnumerable<SlaDto> slaClosureLog = await unitOfWork.Connection.QueryAsync<SlaDto>(TicketProcedureNames.GET_SLA_DETAIL, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return slaClosureLog.AsList();
    }
}

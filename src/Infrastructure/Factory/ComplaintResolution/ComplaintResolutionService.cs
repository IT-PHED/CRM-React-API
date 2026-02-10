using System.Data;
using Application.Abstractions.Data;
using Application.Abstractions.Factory.ComplaintResolution;
using Application.ComplaintResolution.Dto;
using Dapper;
using Infrastructure.UnitOfWork;
using Infrastructure.Utils;
using Oracle.ManagedDataAccess.Client;
using SharedKernel;

namespace Infrastructure.Factory.ComplaintResolution;

internal sealed class ComplaintResolutionService(IUnitOfWork unitOfWork) : IComplaintResolutionService
{
    public double CalculateAmount(double unitConsumed, TariffInWrongMeterReadingResolutionDto tariff)
    {
        double electricityConsumed = tariff.TariffRate * unitConsumed;
        double vat = electricityConsumed * tariff.VAT / 100;
        double amount = electricityConsumed + vat;

        return amount;
    }

    /// <summary>
    /// Update logged customer complaint status (Close complaint)
    /// </summary>
    /// <param name="id">complaint identity</param>
    /// <param name="closedBy">who is carrying out the update</param>
    /// <param name="status">the modified customer complaint status</param>
    /// <returns></returns>
    public async Task CloseConsumerComplaint(string id, string closedBy, string status, string closedByRemark)
    {
        var param = new DynamicParameters();
        param.Add("P_ID", id, DbType.String, ParameterDirection.Input);
        param.Add("P_STATUS", status.ToString(), DbType.String, ParameterDirection.Input);
        param.Add("p_closedBy", closedBy, DbType.String, ParameterDirection.Input);
        param.Add("p_closedRemark", closedByRemark, DbType.String, ParameterDirection.Input);

        await unitOfWork.Connection.ExecuteAsync(ComplaintStoreProcedureNames.CLOSE_CONSUMER_COMPLAINT_STATUS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
    }


    public async Task<WrongMeterReadingResolutionDto> GetBillingInfoByConsumerIdAndCorrectMeterReadingGreaterLessThanCurrentMeterReading(string complaintId, double correctMeterReading)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_complaintId", complaintId);
        param.Add("p_correctMeterReading", correctMeterReading);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add(CursorConstant.CursorName2, OracleDbType.RefCursor, ParameterDirection.Output);

        SqlMapper.GridReader result = await unitOfWork.Connection.QueryMultipleAsync(ComplaintStoreProcedureNames.GET_BILLINGINFO_BY_CONSUMERID_AND_CORRECTMETERREADING_LT_CURRENTMETERREADING, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

        IEnumerable<BillingInfoInWrongMeterReadingResolutionDto> billingInfos = await result.ReadAsync<BillingInfoInWrongMeterReadingResolutionDto>();
        TariffInWrongMeterReadingResolutionDto? tariff = await result.ReadFirstOrDefaultAsync<TariffInWrongMeterReadingResolutionDto>();

        var wrongMeterReadingResolution = new WrongMeterReadingResolutionDto
        {
            BillingInfo = billingInfos.AsList(),
            Tariff = tariff
        };

        return wrongMeterReadingResolution;
    }

    public async Task<TariffInWrongMeterReadingResolutionDto> GetConsumerTariff(string consumerId)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_consumerNumber", consumerId);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        TariffInWrongMeterReadingResolutionDto? tariff = await unitOfWork.Connection.QueryFirstOrDefaultAsync<TariffInWrongMeterReadingResolutionDto>(ConsumerStoreProcedureNames.GET_TARIFF_BY_CONSUMER_ID, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return tariff;
    }

    public async Task UpdateComplaintV2(UpdateComplaintDto payload)
    {
        var param = new DynamicParameters();
        param.Add("p_id", payload.Id, DbType.String, ParameterDirection.Input);
        param.Add("p_dateResolved", payload.DateResolved, DbType.DateTime, ParameterDirection.Input);
        param.Add("p_name", payload.Cons_Name, DbType.String, ParameterDirection.Input);
        param.Add("p_status", payload.Status.ToString(), DbType.String, ParameterDirection.Input);
        param.Add("p_cons_addr1", payload.cons_addr1, DbType.String, ParameterDirection.Input);
        param.Add("p_cons_addr2", payload.cons_addr2, DbType.String, ParameterDirection.Input);
        param.Add("p_cons_addr3", payload.cons_addr3, DbType.String, ParameterDirection.Input);
        param.Add("p_con_EmailId", payload.Con_EmailId, DbType.String, ParameterDirection.Input);
        param.Add("p_con_MobileNo", payload.Con_MobileNo, DbType.String, ParameterDirection.Input);
        param.Add("p_purpose", payload.Purpose, DbType.String, ParameterDirection.Input);
        param.Add("p_cons_Category", payload.Cons_Category, DbType.String, ParameterDirection.Input);
        param.Add("p_meterDigit", payload.MeterDigit, DbType.String, ParameterDirection.Input);
        param.Add("p_meterMake", payload.MeterMake, DbType.String, ParameterDirection.Input);
        param.Add("p_modifiedBy", payload.ModifiedBy, DbType.String, ParameterDirection.Input);
        param.Add("p_modifiedDate", payload.ModifiedDate, DbType.DateTime, ParameterDirection.Input);
        param.Add("p_ResolvedBy", payload.ResolvedBy, DbType.String, ParameterDirection.Input);
        param.Add("p_Feedback", payload.Feedback, DbType.String, ParameterDirection.Input);
        await unitOfWork.Connection.ExecuteAsync(ComplaintStoreProcedureNames.UPDATE_CONSUMER_COMPLAINT_V2, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
    }
}

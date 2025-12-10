using System.Data;
using System.Globalization;
using Application.Abstractions.Data;
using Application.Abstractions.Factory.Complaint;
using Application.Complaints.Dto;
using Dapper;
using Domain.Complaint;
using Infrastructure.UnitOfWork;
using Infrastructure.Utils;
using Oracle.ManagedDataAccess.Client;
using SharedKernel;

namespace Infrastructure.Factory.Complaint;

public sealed class ComplaintService(IUnitOfWork unitOfWork) : IComplaintService
{
    public async Task<ComplaintTransactionResponse> InsertMasterComplaintTransactionAsync(InsertMasterComplaintDto data)
    {
        var parameters = new OracleDynamicParameter();
        parameters.Add("p_complaint_id", data.ComplaintId);
        parameters.Add("p_complainttypeid", data.ComplaintTypeId);
        parameters.Add("p_complaintsubtypeid", data.ComplaintSubTypeId);
        parameters.Add("p_consumerid", data.ConsumerId);
        parameters.Add("p_status", data.Status);
        parameters.Add("p_source", data.Source);
        parameters.Add("p_ticket", data.Ticket);
        parameters.Add("p_dategenerated", data.DateGenerated);
        parameters.Add("p_dateresolved", data.DateResolved);
        parameters.Add("p_priority", data.Priority);
        parameters.Add("p_remark", data.Remark);
        parameters.Add("p_dtr", data.Dtr);
        parameters.Add("p_cons_name", data.ConsName);
        parameters.Add("p_cons_meterno", data.ConsMeterNo);
        parameters.Add("p_cons_telephoneno", data.ConsTelephoneNo);
        parameters.Add("p_con_maxdemand", data.ConMaxDemand);
        parameters.Add("p_cons_category", data.ConsCategory);
        parameters.Add("p_cons_addr1", data.ConsAddr1);
        parameters.Add("p_cons_addr2", data.ConsAddr2);
        parameters.Add("p_cons_addr3", data.ConsAddr3);
        parameters.Add("p_cons_divisioncode", data.ConsDivisionCode);
        parameters.Add("p_cons_sectioncode", data.ConsSectionCode);
        parameters.Add("p_con_routenumber", data.ConRouteNumber);
        parameters.Add("p_con_emailid", data.ConEmailId);
        parameters.Add("p_con_mobileno", data.ConMobileNo);
        parameters.Add("p_cons_type", data.ConsType);
        parameters.Add("p_purpose", data.Purpose);
        parameters.Add("p_metermake", data.MeterMake);
        parameters.Add("p_meterdigit", data.MeterDigit);
        parameters.Add("p_createdby", data.CreatedBy);
        parameters.Add("p_createddate", data.CreatedDate);
        parameters.Add("p_modifiedby", data.ModifiedBy);
        parameters.Add("p_modifieddate", data.ModifiedDate);
        parameters.Add("p_departmentid", data.DepartmentId);
        parameters.Add("p_assignto", data.AssignTo);
        parameters.Add("p_mediaurl", data.MediaLink);

        parameters.Add("p_constraint_id", data.ConstraintId);
        parameters.Add("p_correctmeterreading", data.CorrectMeterReading);
        parameters.Add("p_monthfrom", data.MonthFrom);
        parameters.Add("p_monthto", data.MonthTo);
        parameters.Add("p_filepath", data.FilePath);

        parameters.Add("p_omini_name", data.OminiName);
        parameters.Add("p_omini_phone", data.OminiPhone);
        parameters.Add("p_omini_email", data.OminiEmail);

        parameters.Add("p_insert_constraint", data.InsertConstraint ? 1 : 0);
        parameters.Add("p_auto_allocate", data.AutoAllocate ? 1 : 0);
        parameters.Add("p_post_to_omini", data.PostToOmini ? 1 : 0);

        parameters.Add("o_status_code", dbType: DbType.Decimal, direction: ParameterDirection.Output);
        parameters.Add("o_status_message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

        try
        {
            await unitOfWork.Connection.ExecuteAsync(ComplaintStoreProcedureNames.INSERT_MASTER_COMPLAINT, parameters, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            Type type = parameters.GetType();
            System.Reflection.PropertyInfo? statusCodeProp = type.GetProperty("o_status_code");
            System.Reflection.PropertyInfo? statusMessageProp = type.GetProperty("o_status_message");

            object? rawStatusCode = statusCodeProp?.GetValue(parameters);
            string statusMessage = (string)statusMessageProp?.GetValue(parameters) ?? "Complaint created successfully";

            int statusCode = rawStatusCode switch
            {
                decimal d => Convert.ToInt32(d),
                int i => i,
                string s when int.TryParse(s, out int parsed) => parsed,
                _ => 0
            };

            return new ComplaintTransactionResponse(statusCode, statusMessage);
        }
        catch (OracleException ex)
        {
            Console.WriteLine($"Oracle Error: {ex.Message}");
            return new ComplaintTransactionResponse(-99, $"Database Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
            return new ComplaintTransactionResponse(-999, $"Application Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Check if a customer complaint is logged already
    /// </summary>
    /// <param name="consumerId">customer identity</param>
    /// <param name="complaintTypeId">customer complaint type</param>
    /// <param name="complaintSubTypeId">customer complaint sub type</param>
    /// <returns>true or false</returns>
    public async Task<bool> CanRegisterConsumerComplaint(string consumerId, string complaintTypeId, string complaintSubTypeId)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_consumerId", consumerId);
        param.Add("p_complaintType", complaintTypeId);
        param.Add("p_complaintSubType", complaintSubTypeId);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        bool isValid = await unitOfWork.Connection.QueryFirstOrDefaultAsync<bool>(AppprovalStoreProcedureNames.CAN_REGISTER_CONSUMER_COMPLAINT, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return isValid;
    }

    public async Task<ComplaintType> GetComplaintTypeByIdAsync(string complaintTypeId)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_Id", complaintTypeId);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add(CursorConstant.CursorName2, OracleDbType.RefCursor, ParameterDirection.Output);

        SqlMapper.GridReader result = await unitOfWork.Connection.QueryMultipleAsync(ComplaintStoreProcedureNames.GET_COMPLAINT_TYPE_WITH_SUBTYPE_BY_ID, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        ComplaintType? complaintType = await result.ReadFirstOrDefaultAsync<ComplaintType>();
        IEnumerable<ComplaintSubType> complaintSubTypes = await result.ReadAsync<ComplaintSubType>();

        foreach (ComplaintSubType subType in complaintSubTypes)
        {
            complaintType?.AddSubType(subType.Name, subType.Code, subType.CreatedBy, subType.CreatedDate, subType.ModifiedBy, subType.ModifiedDate, subType.Id);
        }

        return complaintType;
    }

    public async Task<string> GetTicket()
    {
        var param = new OracleDynamicParameter();
        param.Add("p_ticketdate", DateTime.UtcNow);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        string? ticket = await unitOfWork.Connection.QueryFirstOrDefaultAsync<string>(ComplaintStoreProcedureNames.SP_CRM_GENERATE_TICKET, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return ticket;
    }

    public async Task<T> GetComplaintById<T>(string complaintId)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_Id", complaintId);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        T? complaint = await unitOfWork.Connection.QueryFirstOrDefaultAsync<T>(ComplaintStoreProcedureNames.GET_COMPLAINT_BY_ID, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return complaint;
    }

    public async Task<ConsumerComplaintDto> GetComplaintByTicketNumber(string ticketNumber)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_ticketNumber", ticketNumber);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        ConsumerComplaintDto? complaint = await unitOfWork.Connection.QueryFirstOrDefaultAsync<ConsumerComplaintDto>(ComplaintStoreProcedureNames.GET_COMPLAINT_BY_TICKETNUMBER_V2, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return complaint;
    }

    public async Task<IReadOnlyList<CrmComplaintDto>> GetAllComplaints(int pageNumber, int pageSize, string? searchTerm = null, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_PageNumber", pageNumber, DbType.Int32, ParameterDirection.Input);
        param.Add("p_PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
        param.Add("p_SortColumn", "C1.CREATEDDATE", DbType.String, ParameterDirection.Input);
        param.Add("p_SortOrder", "DESC", DbType.String, ParameterDirection.Input);
        param.Add("p_SearchTerm", searchTerm, DbType.String, ParameterDirection.Input);

        param.Add("p_DateFrom", dateFrom, DbType.Date, ParameterDirection.Input);
        param.Add("p_DateTo", dateTo, DbType.Date, ParameterDirection.Input);

        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<CrmComplaintDto> crmList = await unitOfWork.Connection.QueryAsync<CrmComplaintDto>(ComplaintStoreProcedureNames.QUERY_ALL_COMPLAINTS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return crmList.AsList();
    }

    public async Task<IReadOnlyList<CrmComplaintDto>> GetAllDepartmentComplaint(string departmentId, int? pageNumber, int? pageSize, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_PageNumber", pageNumber);
        param.Add("p_PageSize", pageSize);
        param.Add("p_SortColumn", "C1.CREATEDDATE");
        param.Add("p_SortOrder", "DESC");
        param.Add("p_SearchTerm", departmentId);
        param.Add("p_DateFrom", dateFrom);
        param.Add("p_DateTo", dateTo);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<CrmComplaintDto> crmList = await unitOfWork.Connection.QueryAsync<CrmComplaintDto>(ComplaintStoreProcedureNames.SPN_CRM_GET_COMPLAINTS_BY_DEPARTMENTID, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return crmList.AsList();
    }

    public async Task<IReadOnlyList<CrmComplaintDto>> GetAllDepartmentComplaintAndStatus(string departmentId, string status, int? pageNumber, int? pageSize, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_PageNumber", pageNumber);
        param.Add("p_PageSize", pageSize);
        param.Add("p_SortColumn", "C1.CREATEDDATE");
        param.Add("p_SortOrder", "DESC");
        param.Add("p_SearchTerm", departmentId);
        param.Add("p_DateFrom", dateFrom);
        param.Add("p_DateTo", dateTo);
        param.Add("p_Status", status);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<CrmComplaintDto> crmList = await unitOfWork.Connection.QueryAsync<CrmComplaintDto>(ComplaintStoreProcedureNames.GET_COMPLAINTS_BY_DEPARTMENTID_STATUS, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return crmList.AsList();
    }

    public async Task<IReadOnlyList<CrmComplaintDto>> QueryComplaintRegionAndStatus(string departmentId, string region, string status)
    {
        string statusParam = status.ToUpper(CultureInfo.InvariantCulture);

        var param = new OracleDynamicParameter();
        param.Add("p_DepartmentId", departmentId, DbType.String, ParameterDirection.Input);
        param.Add("p_RegionId", region, DbType.String, ParameterDirection.Input);
        param.Add("p_Status", statusParam, DbType.String, ParameterDirection.Input);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);

        IEnumerable<CrmComplaintDto> crmList = await unitOfWork.Connection.QueryAsync<CrmComplaintDto>(ComplaintStoreProcedureNames.QUERY_COMPLAINTS_BY_STATUS_REGION_DEPT, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return crmList.AsList();
    }

    public async Task<string> ReassignComplaint(string ticketId, string consumerId, string reassignStaffId, string? actorId = null)
    {
        var param = new OracleDynamicParameter();
        param.Add("P_consumerid", consumerId);
        param.Add("P_ticketId", ticketId);
        param.Add("P_reassignedStaffId", reassignStaffId);
        param.Add("P_ActorId", actorId);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<dynamic> responseList = await unitOfWork.Connection.QueryAsync(ComplaintStoreProcedureNames.REASSIGN_TICKET, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        if (responseList.Any())
        {
            return "success";
        }
        return "something went wrong";
    }
}

using System.Data;
using Application.Abstractions.Data;
using Application.Abstractions.Factory.Consumer;
using Application.Customers.Dto;
using Dapper;
using Domain.Complaint;
using Infrastructure.UnitOfWork;
using Infrastructure.Utils;
using Oracle.ManagedDataAccess.Client;
using SharedKernel;

namespace Infrastructure.Factory.Consumer;

internal sealed class ConsumerService(IUnitOfWork unitOfWork) : IConsumerService
{
    public Task<Domain.Consumer.Consumer> GetByComplaintIdAsync(string complaintId)
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.Consumer.Consumer> GetByIdAsync(string consumerId)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_Id", consumerId);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add(CursorConstant.CursorName2, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add(CursorConstant.CursorName3, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add(CursorConstant.CursorName4, OracleDbType.RefCursor, ParameterDirection.Output);
        SqlMapper.GridReader result = await unitOfWork.Connection.QueryMultipleAsync(ConsumerStoreProcedureNames.GET_CUSTOMER_FULL_DETAIL_BY_ID, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

        Domain.Consumer.Consumer? consumer = await result.ReadFirstOrDefaultAsync<Domain.Consumer.Consumer>();
        IEnumerable<Domain.Complaint.ComplaintModel> complaintsModel = await result.ReadAsync<Domain.Complaint.ComplaintModel>();
        IEnumerable<Dto.ComplaintDto.ComplaintTypeDto> complaintTypesDto = await result.ReadAsync<Dto.ComplaintDto.ComplaintTypeDto>();
        IEnumerable<Dto.ComplaintDto.ComplaintSubTypeDto> complaintSubTypesDto = await result.ReadAsync<Dto.ComplaintDto.ComplaintSubTypeDto>();

        var complaintTypes = new List<ComplaintType>();
        foreach (Dto.ComplaintDto.ComplaintTypeDto complaintTypeDto in complaintTypesDto)
        {
            complaintTypeDto.SubTypes = complaintSubTypesDto.Where(x => x.ComplaintId == complaintTypeDto.Id).ToList();
            var complaintType = new ComplaintType(complaintTypeDto.Name, complaintTypeDto.Code, complaintTypeDto.CreatedBy, complaintTypeDto.CreatedDate, complaintTypeDto.ModifiedBy, complaintTypeDto.ModifiedDate, complaintTypeDto.Id);
            complaintTypeDto.SubTypes.ForEach(x => complaintType.AddSubType(x.Name, x.Code, x.CreatedBy, x.CreatedDate, x.ModifiedBy, x.ModifiedDate, x.Id));
            complaintTypes.Add(complaintType);
        }

        foreach (ComplaintModel complaintModel in complaintsModel)
        {
            ComplaintType? complaintType = complaintTypes.FirstOrDefault(x => x.Id == complaintModel.ComplaintTypeId);

            ComplaintSubType? complaintSubType = complaintType?.SubTypes?
                .FirstOrDefault(x => x.Id == complaintModel.ComplaintSubTypeId);

            EComplaintStatus status = Enum.Parse<EComplaintStatus>(complaintModel.Status);
            complaintModel.ComplaintStatus = status;
            complaintModel.ComplainSubType = complaintSubType;
            complaintModel.Consumer = consumer;

            consumer?.AddComplaint(complaintModel);

        }

        return consumer;
    }

    public async Task<IReadOnlyList<ConsumerDto>> GetConsumerBySearchCriteria(string? ConsumerName = null, string? ConsumerNumber = null, string? MeterNumber = null, string? MobileNumber = null, string? PrevTicketNumber = null)
    {
        var param = new OracleDynamicParameter();
        param.Add("p_consumerNumber", ConsumerNumber);
        param.Add("p_consumerName", ConsumerName);
        param.Add("p_meterNumber", MeterNumber);
        param.Add("p_mobileNumber", MobileNumber);
        param.Add("p_previousTicketNumber", PrevTicketNumber);
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);

        IEnumerable<ConsumerDto> consumers = await unitOfWork.Connection.QueryAsync<ConsumerDto>(ConsumerStoreProcedureNames.GET_CUSTOMER_BY_SEARCH_CRITERIA, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return consumers.AsList();
    }
}

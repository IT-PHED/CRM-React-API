using System.Data;
using Application.Abstractions.Data;
using Application.Abstractions.Factory.Configuration;
using Application.Configuration.Dto;
using Dapper;
using Infrastructure.Dto.ComplaintDto;
using Infrastructure.UnitOfWork;
using Infrastructure.Utils;
using Oracle.ManagedDataAccess.Client;
using SharedKernel;

namespace Infrastructure.Factory.Configuration;
internal sealed class ConfigurationService(IUnitOfWork unitOfWork) : IConfigurationService
{
    public async Task<IReadOnlyList<ComplaintTypeResponseDto>> ComplaintTypesAsyncV2()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add(CursorConstant.CursorName2, OracleDbType.RefCursor, ParameterDirection.Output);

        SqlMapper.GridReader result = await unitOfWork.Connection.QueryMultipleAsync(ComplaintStoreProcedureNames.GET_COMPLAINT_TYPE_WITH_SUBTYPE, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        IEnumerable<ComplaintTypeResponseDto> complaintTypes = await result.ReadAsync<ComplaintTypeResponseDto>();
        IEnumerable<ComplaintSubTypeResponseDto> complaintSubTypes = await result.ReadAsync<ComplaintSubTypeResponseDto>();

        foreach (ComplaintTypeResponseDto complaintType in complaintTypes)
        {
            complaintType.SubTypes = complaintSubTypes.Where(x => x.ComplaintId == complaintType.Id).ToList();
        }

        return complaintTypes.ToList();
    }

    public async Task<IReadOnlyList<ComplaintSubTypeResponseDto>> GetComplaintSubTypesAsync()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<ComplaintSubTypeResponseDto> complaints = await unitOfWork.Connection.QueryAsync<ComplaintSubTypeResponseDto>(ConfigurationStoreProcedureNames.GET_COMPLAINTSUBTYPE, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return complaints.AsList();
    }

    public async Task<IReadOnlyList<ComplaintTypeResponseDto>> GetComplaintTypesAsync()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<ComplaintTypeResponseDto> complaints = await unitOfWork.Connection.QueryAsync<ComplaintTypeResponseDto>(ConfigurationStoreProcedureNames.GET_COMPLAINTTYPE, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return complaints.AsList();
    }

    public async Task<IReadOnlyList<EnumsResponseDto>> GetPriorities()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<EnumsResponseDto> priorities = await unitOfWork.Connection.QueryAsync<EnumsResponseDto>(AppprovalStoreProcedureNames.GET_PRIORITY, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return priorities.AsList();
    }

    public async Task<IReadOnlyList<EnumsResponseDto>> GetSources()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<EnumsResponseDto> sources = await unitOfWork.Connection.QueryAsync<EnumsResponseDto>(AppprovalStoreProcedureNames.GET_SOURCE, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        return sources.AsList();
    }
}

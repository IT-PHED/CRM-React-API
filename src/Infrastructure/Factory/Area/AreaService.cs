using System.Data;
using Application.Abstractions.Data;
using Application.Abstractions.Factory.Area;
using Application.Area.Dto;
using Application.Configuration.Dto;
using Dapper;
using Infrastructure.Utils;
using Oracle.ManagedDataAccess.Client;
using SharedKernel;

namespace Infrastructure.Factory.Area;

internal sealed class AreaService(IUnitOfWork unitOfWork) : IAreaService
{
    public async Task<AreaResponseDto> GetAreaAsync()
    {
        var param = new OracleDynamicParameter();
        param.Add(CursorConstant.CursorName, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add(CursorConstant.CursorName2, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add(CursorConstant.CursorName3, OracleDbType.RefCursor, ParameterDirection.Output);
        param.Add(CursorConstant.CursorName4, OracleDbType.RefCursor, ParameterDirection.Output);
        SqlMapper.GridReader result = await unitOfWork.Connection.QueryMultipleAsync
            (AreaStoreProcedureNames.GET_IBC_BSC_CATEGORY, param, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

        IEnumerable<EnumsResponseDto> ibcs = await result.ReadAsync<EnumsResponseDto>();
        IEnumerable<BscDto> bscs = await result.ReadAsync<BscDto>();
        IEnumerable<CategoryDto> categories = await result.ReadAsync<CategoryDto>();
        IEnumerable<EnumsResponseDto> regions = await result.ReadAsync<EnumsResponseDto>();

        var area = new AreaResponseDto
        {
            IBCs = ibcs.AsList(),
            BSCs = bscs.AsList(),
            Categories = categories.AsList(),
            Region = regions.AsList()
        };
        return area;
    }

    public async Task<IReadOnlyList<EnumsResponseDto>> GetRegions()
    {
        var param = new OracleDynamicParameter();
        param.Add("p_Result", OracleDbType.RefCursor, ParameterDirection.Output);
        IEnumerable<EnumsResponseDto> result = await unitOfWork.Connection.QueryAsync<EnumsResponseDto>
            (AreaStoreProcedureNames.GET_Regions
            , param
            , transaction: unitOfWork.Transaction
            , commandType: CommandType.StoredProcedure);
        return result.AsList();
    }
}

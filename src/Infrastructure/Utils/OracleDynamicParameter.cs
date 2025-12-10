using System.Data;
using Dapper;
using Oracle.ManagedDataAccess.Client;

namespace Infrastructure.Utils;
public interface IDynamicParameters
{
    void AddParameters(IDbCommand command, SqlMapper.Identity identity);
}
public class OracleDynamicParameter : SqlMapper.IDynamicParameters
{
    private readonly DynamicParameters dynamicParameters = new DynamicParameters();

    private readonly List<OracleParameter> oracleParameters = new List<OracleParameter>();

    public void Add(string name, object? value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null)
    {
        dynamicParameters.Add(name, value, dbType, direction, size);
    }
    public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction)
    {
        var oracleParameter = new OracleParameter(name, oracleDbType, direction);
        oracleParameters.Add(oracleParameter);
    }

    public void Add(string name, OracleDbType oracleDbType, object? value = null, ParameterDirection? direction = null)
    {
        var oracleParameter = new OracleParameter(name, oracleDbType, value, direction ?? ParameterDirection.Input);

        if (value is string[] resultArr)
        {
            oracleParameter.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            oracleParameter.Size = resultArr.Length;
            oracleParameter.Value = resultArr;
        }
        else
        {
            oracleParameter.Value = value;
        }

        oracleParameters.Add(oracleParameter);
    }


    public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
    {
        ((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);


        if (command is OracleCommand oracleCommand)
        {
            oracleCommand.Parameters.AddRange(oracleParameters.ToArray());
        }
    }
}

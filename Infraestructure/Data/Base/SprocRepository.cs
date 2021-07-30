using Microsoft.Extensions.Configuration;

using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Dapper;
using Application.Common;
using Application.Interfaces.Infraestructure.Data.Base;
using System;

namespace Infraestructure.Data
{
    public class SprocRepository : ISprocRepository
    {
        private readonly IDbConnection _dbConnection;
        public SprocRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void ChangeConectionString(string ConnectionString)
        {
            _dbConnection.ConnectionString = ConnectionString;
        }

        public async Task<T> ExecuteFirstOrDefaultSpAsync<T>(string spName, IEnumerable<ParameterStored> Parameter, CancellationToken cancellationToken)
        {

            var valueParameter = new DynamicParameters();

            foreach (ParameterStored item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }

            var result = await _dbConnection.QueryFirstOrDefaultAsync<T>(
                new CommandDefinition
                    (
                     spName
                    , parameters: valueParameter
                    , cancellationToken: cancellationToken
                    , commandType: CommandType.StoredProcedure
                    ));

            return result;
        }

        public async Task<T> ExecuteFirstOrDefaultSpAsync<T>(string spName, CancellationToken cancellationToken)
        {

            var result = await _dbConnection.QueryFirstOrDefaultAsync<T>(
                new CommandDefinition(spName
                    , cancellationToken: cancellationToken
                    , commandType: CommandType.StoredProcedure));
            return result;
        }

        public async Task<IEnumerable<T>> ExecuteSpAsync<T>(string spName, IEnumerable<ParameterStored> Parameter, CancellationToken cancellationToken)
        {

            var valueParameter = new DynamicParameters();

            foreach (ParameterStored item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }


            var result = await _dbConnection.QueryAsync<T>(
                new CommandDefinition(spName
                    , parameters: valueParameter
                    , cancellationToken: cancellationToken
                    , commandType: CommandType.StoredProcedure));
            return result;
        }

        public async Task<IEnumerable<T>> ExecuteSpAsync<T>(string spName, CancellationToken cancellationToken)
        {

            var result = await _dbConnection.QueryAsync<T>(
                new CommandDefinition(spName
                    , cancellationToken: cancellationToken
                    , commandType: CommandType.StoredProcedure));
            return result;
        }

        public async Task<int> ExecuteSpSingleAsync(string spName, IEnumerable<ParameterStored> Parameter, CancellationToken cancellationToken)
        {
            var valueParameter = new DynamicParameters();

            foreach (ParameterStored item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }

            return await _dbConnection.ExecuteAsync(new CommandDefinition(spName, parameters: valueParameter, cancellationToken: cancellationToken, commandType: CommandType.StoredProcedure));
        }

        public async Task<int> ExecuteSpSingleWithOutPutAsync(string spName, IEnumerable<ParameterStored> Parameter, string output, CancellationToken cancellationToken)
        {
            var valueParameter = new DynamicParameters();

            foreach (ParameterStored item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }

            await _dbConnection.ExecuteAsync(new CommandDefinition(spName, parameters: valueParameter, cancellationToken: cancellationToken, commandType: CommandType.StoredProcedure));

            return valueParameter.Get<int>(output);
        }

        public async Task<List<string>> ExecuteSpSingleWithOutPutAsync(string spName, IEnumerable<ParameterStored> Parameter, List<Tuple<string,string>> parametersOut, CancellationToken cancellationToken)
        {
            var valueParameter = new DynamicParameters();

            foreach (ParameterStored item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }

            await _dbConnection.
                ExecuteAsync(new CommandDefinition(spName, parameters: valueParameter, cancellationToken: cancellationToken, commandType: CommandType.StoredProcedure));

            List<string> result = new List<string>();

            foreach (var item in parametersOut)
                
                if(item.Item1 == "Int")
                result.Add(valueParameter.Get<int>(item.Item2).ToString());
                else if (item.Item1 == "String")
                result.Add(valueParameter.Get<string>(item.Item2).ToString());

            return result;
        }

        public async Task<int> ExecuteSpSingleAsync(string spName, CancellationToken cancellationToken)
        {
            return await _dbConnection.ExecuteAsync(new CommandDefinition(spName, cancellationToken: cancellationToken, commandType: CommandType.StoredProcedure));
        }

        public IEnumerable<T> ExecuteSp<T>(string spName, IEnumerable<ParameterStored> Parameter)
        {

            var valueParameter = new DynamicParameters();

            foreach (ParameterStored item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }

            var result = _dbConnection.Query<T>(
                new CommandDefinition(spName
                    , parameters: valueParameter
                    , commandType: CommandType.StoredProcedure));
            return result;
        }

        public IEnumerable<T> ExecuteSp<T>(string spName)
        {
            var result = _dbConnection.Query<T>(
                new CommandDefinition(spName
                    , commandType: CommandType.StoredProcedure));
            return result;
        }

        public int ExecuteSpSingle(string spName, IEnumerable<ParameterStored> Parameter)
        {
            var valueParameter = new DynamicParameters();

            foreach (ParameterStored item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }

            return _dbConnection.Execute(new CommandDefinition(spName, parameters: valueParameter, commandType: CommandType.StoredProcedure));
        }

        public int ExecuteSpSingle(string spName)
        {
            return _dbConnection.Execute(new CommandDefinition(spName, commandType: CommandType.StoredProcedure));
        }
    }
}

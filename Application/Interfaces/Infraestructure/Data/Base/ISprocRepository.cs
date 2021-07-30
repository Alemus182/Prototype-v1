using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Application.Common;
using System;

namespace Application.Interfaces.Infraestructure.Data.Base
{
    public interface ISprocRepository
    {
        void ChangeConectionString(string ConnectionString);
        Task<IEnumerable<T>> ExecuteSpAsync<T>(string spName, CancellationToken cancellationToken);
        Task<IEnumerable<T>> ExecuteSpAsync<T>(string spName, IEnumerable<ParameterStored> Parameter, CancellationToken cancellationToken);
        Task<T> ExecuteFirstOrDefaultSpAsync<T>(string spName, IEnumerable<ParameterStored> Parameter, CancellationToken cancellationToken);
        Task<T> ExecuteFirstOrDefaultSpAsync<T>(string spName, CancellationToken cancellationToken);
        Task<int> ExecuteSpSingleAsync(string spName, IEnumerable<ParameterStored> Parameter, CancellationToken cancellationToken);
        Task<int> ExecuteSpSingleWithOutPutAsync(string spName, IEnumerable<ParameterStored> Parameter, string output, CancellationToken cancellationToken);
        Task<List<string>> ExecuteSpSingleWithOutPutAsync(string spName, IEnumerable<ParameterStored> Parameters, List<Tuple<string, string>> parametersOut, CancellationToken cancellationToken);
        Task<int> ExecuteSpSingleAsync(string spName, CancellationToken cancellationToken);
        IEnumerable<T> ExecuteSp<T>(string spName, IEnumerable<ParameterStored> Parameter);
        IEnumerable<T> ExecuteSp<T>(string spName);
        int ExecuteSpSingle(string spName, IEnumerable<ParameterStored> Parameter);
        int ExecuteSpSingle(string spName);
    }
}

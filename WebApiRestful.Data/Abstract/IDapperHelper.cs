using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.WebApiRestful.Data.Abstract
{
    public interface IDapperHelper<T> where T : class
    {
        /// <summary>
        /// Execute raw query not return any values
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parammeters"></param>
        void ExecuteNotReturn(string query, DynamicParameters parammeters = null);
        Task<T> ExecuteReturnScalar<T>(string query, DynamicParameters parammeters = null);
        Task<IEnumerable<T>> ExecuteSqlReturnList<T>(string query, DynamicParameters parammeters = null);
        Task<IEnumerable<T>> ExecuteStoreProcedureReturnList<T>(string query, DynamicParameters parammeters = null);
    }
}

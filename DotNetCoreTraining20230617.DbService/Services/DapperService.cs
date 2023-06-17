using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreTraining20230617.DbService.Services
{
    public class DapperService
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public DapperService(string connectionString)
        {
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }

        public DapperService(SqlConnectionStringBuilder connectionString)
        {
            _sqlConnectionStringBuilder = connectionString;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        }

        public async Task<List<T>> Query<T>(string query, object parameters)
        {
            using (IDbConnection db = CreateConnection())
            {
                var result = await db.QueryAsync<T>(query, parameters);
                return result.ToList();
            }
        }

        public async Task<int> Execute(string query, object parameters)
        {
            using (IDbConnection db = CreateConnection())
            {
                return await db.ExecuteAsync(query, parameters);
            }
        }
    }
}

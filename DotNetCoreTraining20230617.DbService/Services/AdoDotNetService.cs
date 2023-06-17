using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreTraining20230617.DbService.Services
{
    public class AdoDotNetService
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public AdoDotNetService(string connectionString)
        {
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }

        public AdoDotNetService(SqlConnectionStringBuilder connectionString)
        {
            _sqlConnectionStringBuilder = connectionString;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        }

        public async Task<DataTable> Query(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection sqlConnection = CreateConnection();
            await sqlConnection.OpenAsync();

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            foreach (var sqlParameter in sqlParameters)
            {
                cmd.Parameters.AddWithValue(sqlParameter.ParameterName, sqlParameter.Value);
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            await sqlConnection.CloseAsync();

            return dt;
        }

        public async Task<List<T>> Query<T>(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection sqlConnection = CreateConnection();
            await sqlConnection.OpenAsync();

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            foreach (var sqlParameter in sqlParameters)
            {
                cmd.Parameters.AddWithValue(sqlParameter.ParameterName, sqlParameter.Value);
            }

            //SqlDataAdapter adapter = new SqlDataAdapter();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            await sqlConnection.CloseAsync();

            var jsonStr = JsonConvert.SerializeObject(dt);
            return JsonConvert.DeserializeObject<List<T>>(jsonStr);
        }

        public async Task<int> Execute(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection sqlConnection = CreateConnection();
            await sqlConnection.OpenAsync();

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            foreach (var sqlParameter in sqlParameters)
            {
                cmd.Parameters.AddWithValue(sqlParameter.ParameterName, sqlParameter.Value);
            }
            int result = await cmd.ExecuteNonQueryAsync();

            await sqlConnection.CloseAsync();

            return result;
        }
    }

    public class SqlParameterModel
    {
        public string ParameterName { get; set; }
        public object Value { get; set; }
    }
}

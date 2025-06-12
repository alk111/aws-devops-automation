using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace OMartInfra.Repositories
{
    public  class CentralRepository
    {
        private readonly string _connectionString;

        public CentralRepository(IConfiguration configuration, string connectionStringName)
        {
            _connectionString = configuration.GetConnectionString(connectionStringName)!;
        }
        protected async Task<T> ExecuteQueryAsync<T>(string query, object parameters)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var stopwatch = Stopwatch.StartNew();
                    var result = await connection.QueryAsync<T>(query, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    string elapsed = stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.fff");


                    return result.FirstOrDefault()!;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected async Task<List<T>> ExecuteQueryListAsync<T>(string query, object parameters)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var stopwatch = Stopwatch.StartNew();
                    var result = await connection.QueryAsync<T>(query, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    string elapsed = stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.fff");


                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        protected async Task<IEnumerable<T>> ExecuteQueryWithDynamicParametersAsync<T>(string storedProcedure, DynamicParameters parameters)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    var result = await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    string elapsed = stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.fff");


                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

    }
}

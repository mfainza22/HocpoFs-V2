using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;

namespace DataAccessLayer
{
    public class DBContext
    {
        public DBContext()
        {

        }

        public static T GetRecord<T>(string query, List<ParameterInfo> parameters = null, CommandType commandType = CommandType.Text)
        {
            logQuery(query);
            T objRecord = default;
            using (IDbConnection objConnection = new SqlConnection(ServerUtils.ConnectionString()))
            {
                objConnection.Open();
                objRecord = SqlMapper.Query<T>(objConnection, query, parameters.ToDynamicParams(), commandType: commandType).FirstOrDefault();
                objConnection.Close();
            }
            return objRecord;
        }

        public static async Task<T> GetRecordAsync<T>(string query, List<ParameterInfo> parameters = null, CommandType commandType = CommandType.Text)
        {
            logQuery(query);
            T objRecord = default;
            using (IDbConnection objConnection = new SqlConnection(ServerUtils.ConnectionString()))
            {
                objConnection.Open();
                var r = await SqlMapper.QueryAsync<T>(objConnection, query, parameters.ToDynamicParams(), commandType: commandType);
                objRecord = r.FirstOrDefault();
                objConnection.Close();
            }
            return objRecord;
        }

        public static List<T> GetRecords<T>(string query, List<ParameterInfo> parameters = null, CommandType commandType = CommandType.Text)
        {
            logQuery(query);
            List<T> recordList = new List<T>();
            using (SqlConnection objConnection = new SqlConnection(ServerUtils.ConnectionString()))
            {
                objConnection.Open();
                recordList = SqlMapper.Query<T>(objConnection, query, parameters.ToDynamicParams(), commandType: commandType).ToList();
                objConnection.Close();
            }
            return recordList;
        }

        public static async Task<IEnumerable<T>> GetRecordsAsync<T>(string query, List<ParameterInfo> parameters = null, CommandType commandType = CommandType.Text)
        {
            logQuery(query);
            using (SqlConnection objConnection = new SqlConnection(ServerUtils.ConnectionString()))
            {
                objConnection.Open();
                var r = await SqlMapper.QueryAsync<T>(objConnection, query, parameters.ToDynamicParams(), commandType: commandType);
                objConnection.Close();
                return r;
            }
        }

        public static DataTable GetRecords(string query, List<ParameterInfo> parameters = null, CommandType commandType = CommandType.Text)
        {
            logQuery(query);
            var dt = new DataTable();
            using (IDbConnection objConnection = new SqlConnection(ServerUtils.ConnectionString()))
            {
                objConnection.Open();
                var reader = SqlMapper.ExecuteReader(objConnection, query, parameters.ToDynamicParams(), commandType: commandType);
                dt.Load(reader);

                objConnection.Close();
            }
            return dt;
        }

        public static int ExecuteQuery(string query, List<ParameterInfo> parameters = null, CommandType commandType = CommandType.Text)
        {
            logQuery(query);
            int success = 0;
            using (IDbConnection objConnection = new SqlConnection(ServerUtils.ConnectionString()))
            {
                objConnection.Open();
                success = SqlMapper.Execute(objConnection, query, parameters.ToDynamicParams(), commandType: commandType);
                objConnection.Close();
            }
            return success;
        }

        public static async Task<int> ExecuteQueryAsync(string query, List<ParameterInfo> parameters = null, CommandType commandType = CommandType.Text)
        {
            logQuery(query);
            int success = 0;
            using (IDbConnection objConnection = new SqlConnection(ServerUtils.ConnectionString()))
            {
                objConnection.Open();
                success = await SqlMapper.ExecuteAsync(objConnection, query, parameters.ToDynamicParams(), commandType: commandType);
                objConnection.Close();
            }
            return success;
        }

        public static long ExecuteQueryWithIdentityInt64(string query, List<ParameterInfo> parameters = null, CommandType commandType = CommandType.Text)
        {
            logQuery(query);
            var dt = new DataTable();
            using (IDbConnection objConnection = new SqlConnection(ServerUtils.ConnectionString()))
            {
                objConnection.Open();
                var reader = SqlMapper.ExecuteReader(objConnection, query, parameters.ToDynamicParams(), commandType: commandType);
                dt.Load(reader);

                objConnection.Close();
            }
            dt.Dispose();
            return Convert.ToInt64(dt.Rows[0].ItemArray[0]);
        }

        public static long GetRecordCount(string query)
        {
            logQuery(query);
            using (IDbConnection objConnection = new SqlConnection(ServerUtils.ConnectionString()))
            {
                objConnection.Open();
                var result = SqlMapper.Query<int>(objConnection, query);
                objConnection.Close();
                return result.First();
            }
        }

        public static void ExecuteTransaction(List<TransactionQuery> trans)
        {

            using (IDbConnection objConnection = new SqlConnection(ServerUtils.ConnectionString()))
            {
                objConnection.Open();
                using (var transaction = objConnection.BeginTransaction())
                {
                    try
                    {
                        foreach (var tran in trans)
                        {
                            SqlMapper.Execute(objConnection, tran.Query.ToString(), tran.Parameters.ToDynamicParams(), commandType: CommandType.Text, transaction: transaction);
                        }
                        transaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.Write(ex);
                        transaction.Rollback();
                    }

                }
            }
        }

        private static void logQuery(string query)
        {
            System.Diagnostics.Debug.WriteLine(query);
            // _logger.LogInformation(query);
        }

    }
}

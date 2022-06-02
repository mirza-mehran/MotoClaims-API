using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.ModelMapper;
using MotoClaims.Entities.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;

namespace MotoClaims.DataAccess.Generics
{
    public class DBGenerics
    {
        private CommandType CommandType = CommandType.Text;
        private string ConnectionString = DBHelper.ConnectionString;
        private int CommandTimeout = 30;

        /// <summary>
        /// The factory
        /// </summary>
        public static readonly DbProviderFactory Factory = DbProviderFactories.GetFactory("System.Data.SqlClient");

        #region retun integer,long

        //scaller for insert and non query for update
        /// <summary>
        /// Executes the scalar int32.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Int32.</returns>
        public int ExecuteScalarInt32(string query, params DbParameter[] prms)
        {
            int id = -1;

            object o = ExecuteScalar(query, prms);

            if (o != null && o != DBNull.Value)
            {
                id = int.Parse(o.ToString());
            }

            return id;
        }

        /// <summary>
        /// Executes the scalar int32 sp.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Int32.</returns>
        public int ExecuteScalarInt32Sp(string query, params DbParameter[] prms)
        {
            return ExecuteScalarSP(query, CommandType.StoredProcedure, prms);
        }

        /// <summary>
        /// Executes the scalar sp int32.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Int32.</returns>
        public int ExecuteScalarSPInt32(string query, params DbParameter[] prms)
        {
            int id = -1;

            object o = ExecuteScalarCountSP(query, CommandType.StoredProcedure, prms);

            if (o != null && o != DBNull.Value)
            {
                id = int.Parse(o.ToString());
            }

            return id;
        }

        /// <summary>
        /// Executes the scalar int32.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>System.Int32.</returns>
        public int ExecuteScalarInt32(string query)
        {
            int id = -1;

            object o = ExecuteScalar(query, null);

            if (o != null && o != DBNull.Value)
            {
                id = int.Parse(o.ToString());
            }

            return id;
        }

        /// <summary>
        /// Executes the scalar decimal.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>System.Decimal.</returns>
        public decimal ExecuteScalarDecimal(string query)
        {
            decimal id = Decimal.MinValue;

            object o = ExecuteScalar(query, null);

            if (o != null && o != DBNull.Value)
            {
                id = decimal.Parse(o.ToString());
            }

            return id;
        }

        /// <summary>
        /// Executes the scalar string.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>System.String.</returns>
        public string ExecuteScalarString(string query)
        {
            string id = "-1";

            object o = ExecuteScalar(query, null);

            if (o != null && o != DBNull.Value)
            {
                id = o.ToString();
            }

            return id;
        }

        /// <summary>
        /// Executes the scalar nvarchar.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.String.</returns>
        public string ExecuteScalarNvarchar(string query, params DbParameter[] prms)
        {
            string id = "-1";

            object o = ExecuteScalarwithSP(query, prms);

            if (o != null && o != DBNull.Value)
            {
                id = o.ToString();
            }

            return id;
        }

        /// <summary>
        /// Executes the scalarwith sp.
        /// </summary>
        /// <param name="sprocName">Name of the sproc.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Object.</returns>
        public object ExecuteScalarwithSP(string sprocName, params DbParameter[] prms)
        {
            object o = null;

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = sprocName;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();

                        o = command.ExecuteScalar();

                        connection.Close();
                    }
                }
            }
            finally
            {
            }

            return o;
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Int32.</returns>
        public int ExecuteNonQuery(string query, params DbParameter[] prms)
        {
            int effRows = -1;

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType;
                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();
                        effRows = command.ExecuteNonQuery();//no of effected rows
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }

            return effRows;
        }

        /// <summary>
        /// Executes the scalar int64.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Int64.</returns>
        public long ExecuteScalarInt64(string query, params DbParameter[] prms)
        {
            long id = -1;

            object o = ExecuteScalar(query, prms);

            if (o != null && o != DBNull.Value)
            {
                id = long.Parse(o.ToString());
            }
            //first column of first rown for insert
            return id;
        }

        /// <summary>
        /// Executes the scalar int64.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>System.Int64.</returns>
        public long ExecuteScalarInt64(string query)
        {
            long id = -1;

            object o = ExecuteScalar(query, null);

            if (o != null && o != DBNull.Value)
            {
                id = long.Parse(o.ToString());
            }

            return id;
        }

        #endregion retun integer,long

        #region return List ds,dt and obj

        /// <summary>
        /// Executes the data table.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>DataTable.</returns>
        public DataTable ExecuteDataTable(string query, params DbParameter[] prms)
        {
            DataTable dtResult = new DataTable();
            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.Parameters.Clear();
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType;
                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        //connection.Open();
                        DbDataAdapter adapter = Factory.CreateDataAdapter();
                        adapter.SelectCommand = command;

                        // Fill the DataTable.
                        DataTable table = new DataTable();
                        adapter.Fill(dtResult);

                        //connection.Close();
                    }
                }
            }
            catch (Exception exp)
            { }
            finally
            {
            }

            return dtResult;
        }

        /// <summary>
        /// Executes the data set.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>DataSet.</returns>
        public DataSet ExecuteDataSet(string query, params DbParameter[] prms)
        {
            DataSet dsResult = new DataSet();
            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType;
                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        //connection.Open();
                        DbDataAdapter adapter = Factory.CreateDataAdapter();
                        adapter.SelectCommand = command;

                        // Fill the DataTable.

                        adapter.Fill(dsResult);

                        //connection.Close();
                    }
                }
            }
            finally
            {
            }

            return dsResult;
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Object.</returns>
        public object ExecuteScalar(string query, params DbParameter[] prms)
        {
            object o = null;

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType;
                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();

                        o = command.ExecuteScalar();

                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }

            return o;
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Object.</returns>
        public object ExecuteScalar(string query, CommandType commandType, params DbParameter[] prms)
        {
            object o = null;

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = commandType;
                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }
                        //command.Parameters.Add("@status");
                        command.Parameters["@Result"].Direction = ParameterDirection.Output;
                        connection.Open();

                        o = command.ExecuteScalar();
                        o = Convert.ToInt32(command.Parameters["@Result"].Value);
                        connection.Close();
                    }
                }
            }
            finally
            {
            }

            return o;
        }

        /// <summary>
        /// Executes the scalar count sp.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Object.</returns>
        public object ExecuteScalarCountSP(string query, CommandType commandType, params DbParameter[] prms)
        {
            object o = null;

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = commandType;
                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }
                        connection.Open();
                        o = command.ExecuteScalar();
                        connection.Close();
                    }
                }
            }
            finally
            {
            }

            return o;
        }

        /// <summary>
        /// Executes the scalar sp.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Int32.</returns>
        public int ExecuteScalarSP(string query, CommandType commandType, params DbParameter[] prms)
        {
            int effRows = -1;

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = commandType;
                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }
                        connection.Open();
                        effRows = command.ExecuteNonQuery();//no of effected rows
                        connection.Close();
                    }
                }
            }
            finally
            {
            }

            return effRows;
        }

        #endregion return List ds,dt and obj

        #region return generic model

        /// <summary>
        /// Executes the single.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns>T.</returns>
        public T ExecuteSingle<T>(string query) where T : new()
        {
            return ExecuteSingle<T>(query, null);
        }

        /// <summary>
        /// Executes the single.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>T.</returns>
        public T ExecuteSingle<T>(string query, params DbParameter[] prms) where T : new()
        {
            IList<T> list = ExecuteList<T>(query, prms);

            if (list.Count > 0)
                return list[0];

            T obj = default(T);

            return obj;
        }

        //.For Returning the Single Object wiht SP with out Parameters
        /// <summary>
        /// Executes the singlewith sp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sprocName">Name of the sproc.</param>
        /// <returns>T.</returns>
        public T ExecuteSinglewithSP<T>(string sprocName) where T : new()
        {
            return ExecuteSinglewithSP<T>(sprocName, null);
        }

        //.For Returning the Single Object wiht SP with Parameters
        /// <summary>
        /// Executes the singlewith sp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sprocName">Name of the sproc.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>T.</returns>
        public T ExecuteSinglewithSP<T>(string sprocName, params DbParameter[] prms) where T : new()
        {
            IList<T> list = ExecuteListSp<T>(sprocName, prms);

            if (list.Count > 0)
                return list[0];

            T obj = default(T);

            return obj;
        }

        #endregion return generic model

        #region return list

        /// <summary>
        /// Executes the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns>IList&lt;T&gt;.</returns>
        public IList<T> ExecuteList<T>(string query) where T : new()
        {
            return ExecuteList<T>(query, null);
        }

        /// <summary>
        /// Executes the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>IList&lt;T&gt;.</returns>
        public IList<T> ExecuteList<T>(string query, params DbParameter[] prms) where T : new()
        {
            IList<T> objectList = new List<T>();
            T obj = default(T);

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType;
                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();

                        using (DbDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    obj = new T();

                                    IDataMapper mapper = obj as IDataMapper;
                                    mapper.MapProperties(dataReader);

                                    objectList.Add(obj);
                                }
                            }

                            dataReader.Close();
                        }
                    }
                }
            }
            finally
            {
            }

            return objectList;
        }

        /// <summary>
        /// Executes the list sp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>IList&lt;T&gt;.</returns>
        public IList<T> ExecuteListSp<T>(string query, params DbParameter[] prms) where T : new()
        {
            IList<T> objectList = new List<T>();
            T obj = default(T);

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();

                        using (DbDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    obj = new T();

                                    IDataMapper mapper = obj as IDataMapper;
                                    mapper.MapProperties(dataReader);

                                    objectList.Add(obj);
                                }
                            }

                            dataReader.Close();
                        }
                    }
                }
            }
            finally
            {
            }

            return objectList;
        }

        /// <summary>
        /// Executes the lists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>IList&lt;T&gt;.</returns>
        public IList<T> ExecuteLists<T>(string query, params DbParameter[] prms) where T : new()
        {
            IList<T> objectList = new List<T>();
            T obj = default(T);

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType;
                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();

                        using (DbDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    obj = new T();
                                    objectList.Add(obj);
                                }
                            }

                            dataReader.Close();
                        }
                    }
                }
            }
            finally
            {
            }

            return objectList;
        }

        /// <summary>
        /// Executes the string list.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="CommandType">Type of the command.</param>
        /// <param name="resultColumnName">Name of the result column.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>IList&lt;System.String&gt;.</returns>
        public IList<string> ExecuteStringList(string query, CommandType CommandType, string resultColumnName, params DbParameter[] prms)
        {
            IList<string> objectList = new List<string>();

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType;
                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();

                        using (DbDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    string obj = dataReader.GetString(resultColumnName);
                                    objectList.Add(obj);
                                }
                            }

                            dataReader.Close();
                        }
                    }
                }
            }
            finally
            {
            }

            return objectList;
        }

        /// <summary>
        /// Executes the int32 list.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="resultColumnName">Name of the result column.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>IList&lt;System.Int32&gt;.</returns>
        public IList<int> ExecuteInt32List(string query, string resultColumnName, params DbParameter[] prms)
        {
            //int recordCount = 0;

            IList<int> objectList = new List<int>();

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;

                        command.Connection = connection;

                        command.CommandType = CommandType;

                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();

                        using (DbDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int obj = dataReader.GetInt32(resultColumnName);

                                    objectList.Add(obj);
                                }
                            }

                            dataReader.Close();
                        }
                    }
                }
            }
            finally
            {
            }

            return objectList;
        }

        #endregion return list

        #region return dictionary

        /// <summary>
        /// Executes the int32 key dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="resultColumnName">Name of the result column.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>IDictionary&lt;System.Int32, T&gt;.</returns>
        public IDictionary<int, T> ExecuteInt32KeyDictionary<T>(string query, string resultColumnName, params DbParameter[] prms) where T : new()
        {
            //int recordCount = 0;

            IDictionary<int, T> objectList = new Dictionary<int, T>();

            T obj = default(T);

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;

                        command.Connection = connection;

                        command.CommandType = CommandType;

                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();

                        using (DbDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    obj = new T();

                                    IDataMapper mapper = obj as IDataMapper;

                                    mapper.MapProperties(dataReader);

                                    int key = dataReader.GetInt32(resultColumnName);

                                    objectList.Add(key, obj);
                                }
                            }

                            dataReader.Close();
                        }
                    }
                }
            }
            finally
            {
            }

            return objectList;
        }

        /// <summary>
        /// Executes the string key dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="resultColumnName">Name of the result column.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>IDictionary&lt;System.String, T&gt;.</returns>
        public IDictionary<string, T> ExecuteStringKeyDictionary<T>(string query, string resultColumnName, params DbParameter[] prms) where T : new()
        {
            //int recordCount = 0;

            IDictionary<string, T> objectList = new Dictionary<string, T>();

            T obj = default(T);

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;

                        command.Connection = connection;

                        command.CommandType = CommandType;

                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();

                        using (DbDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    obj = new T();

                                    IDataMapper mapper = obj as IDataMapper;

                                    mapper.MapProperties(dataReader);

                                    string key = dataReader.GetString(resultColumnName);

                                    objectList.Add(key, obj);
                                }
                            }

                            dataReader.Close();
                        }
                    }
                }
            }
            finally
            {
            }

            return objectList;
        }

        /// <summary>
        /// Executes the int32 key value dictionary.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="resultColumnName">Name of the result column.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>IDictionary&lt;System.Int32, System.Int32&gt;.</returns>
        public IDictionary<int, int> ExecuteInt32KeyValueDictionary(string query, string resultColumnName, params DbParameter[] prms)
        {
            //int recordCount = 0;

            IDictionary<int, int> objectList = new Dictionary<int, int>();

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;

                        command.Connection = connection;

                        command.CommandType = CommandType;

                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();

                        using (DbDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int key = dataReader.GetInt32(resultColumnName);

                                    objectList.Add(key, key);
                                }
                            }

                            dataReader.Close();
                        }
                    }
                }
            }
            finally
            {
            }

            return objectList;
        }

        /// <summary>
        /// Gets the data table from sp.
        /// </summary>
        /// <param name="sprocName">Name of the sproc.</param>
        /// <param name="paramList">The parameter list.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetDataTableFromSp(string sprocName, Dictionary<string, string> paramList)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sprocName;
                cmd.Connection = conn;
                //loop through the dictionary
                foreach (string key in paramList.Keys)
                {
                    cmd.Parameters.Add(key, paramList[key]);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                //release the resource
                cmd = null;
                da = null;
                return ds;
            }
        }

        /// <summary>
        /// Executes the string key value dictionary.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="resultColumnName">Name of the result column.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>IDictionary&lt;System.String, System.String&gt;.</returns>
        public IDictionary<string, string> ExecuteStringKeyValueDictionary(string query, string resultColumnName, params DbParameter[] prms)
        {
            //int recordCount = 0;

            IDictionary<string, string> objectList = new Dictionary<string, string>();

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;

                        command.Connection = connection;

                        command.CommandType = CommandType;

                        command.CommandText = query;

                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();

                        using (DbDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    string key = dataReader.GetString(resultColumnName);

                                    objectList.Add(key, key);
                                }
                            }

                            dataReader.Close();
                        }
                    }
                }
            }
            finally
            {
            }

            return objectList;
        }

        /// <summary>
        /// Executes the scalarwith out putparameter.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="output">The output.</param>
        /// <param name="dict">The dictionary.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Object.</returns>
        public object ExecuteScalarwithOutPutparameter(string query, DbParameter[] output, out Dictionary<string, string> dict, params DbParameter[] prms)
        {
            object o = null;

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType;
                        command.CommandText = query;
                        if (output != null)
                        {
                            foreach (SqlParameter par in output)
                            {
                                if (par != null)
                                {
                                    par.Direction = ParameterDirection.Output;
                                    command.Parameters.Add(par);
                                }
                            }
                        }
                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();
                        o = command.ExecuteScalar();

                        dict = new Dictionary<string, string>();

                        foreach (SqlParameter par in output)
                        {
                            if (par != null)
                            {
                                dict.Add(command.Parameters[par.ParameterName].ParameterName, command.Parameters[par.ParameterName].Value.ToString());
                            }
                        }
                        connection.Close();
                    }
                }
            }
            finally
            {
            }

            return o;
        }

        /// <summary>
        /// Executes the int32with out putparameter sp.
        /// </summary>
        /// <param name="StroProc">The stro proc.</param>
        /// <param name="output">The output.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Int32.</returns>
        public int ExecuteInt32withOutPutparameterSp(string StroProc, DbParameter[] output, params DbParameter[] prms)
        {
            int id = -1;

            object o = ExecuteScalarwithOutPutparameterSp(StroProc, output, prms);

            if (o != null && o != DBNull.Value)
            {
                id = int.Parse(o.ToString());
            }

            return id;
        }

        /// <summary>
        /// Executes the int64with out putparameter sp.
        /// </summary>
        /// <param name="StroProc">The stro proc.</param>
        /// <param name="output">The output.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Int64.</returns>
        public long ExecuteInt64withOutPutparameterSp(string StroProc, DbParameter[] output, params DbParameter[] prms)
        {
            long id = -1;

            object o = ExecuteScalarwithOutPutparameterSpfroLong(StroProc, output, prms);

            if (o != null && o != DBNull.Value)
            {
                id = long.Parse(o.ToString());
            }

            return id;
        }

        /// <summary>
        /// Executes the scalarwith out putparameter spfro long.
        /// </summary>
        /// <param name="StroProc">The stro proc.</param>
        /// <param name="output">The output.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Object.</returns>
        public object ExecuteScalarwithOutPutparameterSpfroLong(string StroProc, DbParameter[] output, params DbParameter[] prms)
        {
            object o = null;

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = StroProc;
                        if (output != null)
                        {
                            foreach (SqlParameter par in output)
                            {
                                if (par != null)
                                {
                                    par.Direction = ParameterDirection.Output; par.DbType = DbType.Int64;
                                    var value = command.Parameters.Contains(par);
                                    command.Parameters.Add(par);
                                }
                            }
                        }
                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();
                        o = command.ExecuteScalar();

                        //dict = new Dictionary<string, string>();

                        foreach (SqlParameter par in output)
                        {
                            if (par != null)
                            {
                                // dict.Add(command.Parameters[par.ParameterName].ParameterName, command.Parameters[par.ParameterName].Value.ToString());
                                o = command.Parameters[par.ParameterName].Value.ToString();
                            }
                        }
                        connection.Close();
                    }
                }
            }
            finally
            {
            }

            return o;
        }

        /// <summary>
        /// Executes the scalarwith out putparameter sp.
        /// </summary>
        /// <param name="StroProc">The stro proc.</param>
        /// <param name="output">The output.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns>System.Object.</returns>
        public object ExecuteScalarwithOutPutparameterSp(string StroProc, DbParameter[] output, params DbParameter[] prms)
        {
            object o = null;

            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = StroProc;
                        if (output != null)
                        {
                            foreach (SqlParameter par in output)
                            {
                                if (par != null)
                                {
                                    par.Direction = ParameterDirection.Output;
                                    command.Parameters.Add(par);
                                }
                            }
                        }
                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();
                        o = command.ExecuteScalar();

                        //dict = new Dictionary<string, string>();

                        foreach (SqlParameter par in output)
                        {
                            if (par != null)
                            {
                                // dict.Add(command.Parameters[par.ParameterName].ParameterName, command.Parameters[par.ParameterName].Value.ToString());
                                o = command.Parameters[par.ParameterName].Value.ToString();
                            }
                        }
                        connection.Close();
                    }
                }
            }
            finally
            {
            }

            return o;
        }

        /// <summary>
        /// Executes the boolwith out putparameter sp.
        /// </summary>
        /// <param name="StroProc">The stro proc.</param>
        /// <param name="output">The output.</param>
        /// <param name="prms">The PRMS.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ExecuteBoolwithOutPutparameterSp(string StroProc, DbParameter[] output, params DbParameter[] prms)
        {
            object o = null;
            bool _returnVal = false;
            try
            {
                using (DbConnection connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;

                    using (DbCommand command = Factory.CreateCommand())
                    {
                        command.CommandTimeout = CommandTimeout;
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = StroProc;
                        if (output != null)
                        {
                            foreach (SqlParameter par in output)
                            {
                                if (par != null)
                                {
                                    par.Direction = ParameterDirection.Output;
                                    command.Parameters.Add(par);
                                }
                            }
                        }
                        if (prms != null)
                        {
                            command.Parameters.AddRange(prms);
                        }

                        connection.Open();
                        o = command.ExecuteScalar();

                        //dict = new Dictionary<string, string>();

                        foreach (SqlParameter par in output)
                        {
                            if (par != null)
                            {
                                // dict.Add(command.Parameters[par.ParameterName].ParameterName, command.Parameters[par.ParameterName].Value.ToString());
                                _returnVal = Convert.ToBoolean(command.Parameters[par.ParameterName].Value);
                            }
                        }
                        connection.Close();
                    }
                }
            }
            finally
            {
            }

            return _returnVal;
        }

        #endregion return dictionary

        #region

        private IParamBuilder _paramBuilder;

        protected IParamBuilder ParamBuilder
        {
            get
            {
                return _paramBuilder ?? new SqlParamBuilder();
            }
        }

        #endregion

        #region FCM Notification
        public void SendMessage(string fcm,string Notifititile,string NotifiBody)
        {
            var data = new
            {
                to = fcm,
                data = new
                {
                    NotificationTitile = Notifititile,
                    NotificationBody = NotifiBody,
                }
            };

            SendNotification(data);
        }

        public void SendNotification(object data)
        {
            //var serializer = new .JavaScriptSerializer();
            var json = JsonConvert.SerializeObject(data);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            SendNotification(byteArray);
        }

        public void SendNotification(Byte[] byteArray)
        {
            try
            {
                string server_api_key = ConfigurationManager.AppSettings["SERVER_API_KEY"];
                string sender_id = ConfigurationManager.AppSettings["SENDER_ID"];

                WebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                webRequest.Method = "post";
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add($"Authorization: key={server_api_key}");
                webRequest.Headers.Add($"Sender: id={sender_id}");

                webRequest.ContentLength = byteArray.Length;
                Stream dataStream = webRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse webResponse = webRequest.GetResponse();
                dataStream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(dataStream);

                string sResponseFromServer = streamReader.ReadToEnd();
                streamReader.Close();
                dataStream.Close();
                webResponse.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
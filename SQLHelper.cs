using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
namespace TheCricketSeasonScheduler
{

    public class SQLHelper : IDisposable
    {
        SqlCommand SQLCmd = new SqlCommand();
        #region "Constructors  Destructor"
        public SQLHelper()
        {
            _errormessage = "";
            SQLConn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["CONSTR"]);
            _connectionString = ConfigurationManager.AppSettings["CONSTR"];
            //Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["CONSTR"]);
            SQLReader = null;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposedStatus)
        {
            if (disposedStatus)
            {
                if (SQLConn != null)
                {
                    if (SQLConn.State == ConnectionState.Open)
                        SQLConn.Close();
                    SQLConn = null;
                }
                GC.SuppressFinalize(this);
            }


        }
        #endregion

        #region "Private Members"

        SqlDataReader SQLReader = null;
        SqlConnection SQLConn;
        private string _errormessage = "";
        private string _connectionString = ConfigurationManager.AppSettings["CONSTR"];
        private int _recordaffect = 0;
        private bool _flag = false;

        #endregion

        #region "Properties"
        public bool HasError
        {
            get
            {

                if (!string.IsNullOrEmpty(_errormessage))
                    return true;
                else
                    return false;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errormessage;
            }
        }
        #endregion

        #region "ExecuteNonQuery Methods"
        /// <summary>
        /// Execute stored procedure on database
        /// </summary>
        /// <param name="StroredProcedureName"></param>
        /// <param name="ParaMeterCollection"></param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string StroredProcedureName, SqlParameter[] ParaMeterCollection)
        {
            SqlCommand SQLCmd = new SqlCommand();
            try
            {

                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = StroredProcedureName;
                SQLCmd.CommandType = CommandType.StoredProcedure;
                //Loop for Paramets
                SQLCmd.Parameters.Clear();
                for (int i = 0; i < ParaMeterCollection.Length; i++)
                {
                    SQLCmd.Parameters.Add(ParaMeterCollection[i]);
                }
                //End of for loop
                SQLConn.Open();
                _recordaffect = SQLCmd.ExecuteNonQuery();
                SQLConn.Close();
                if (_recordaffect > 0)
                    _flag = true;
                else
                    _flag = false;
            }
            catch (Exception e)
            {
                _errormessage = e.Message;
                Console.WriteLine("ERROR >>" + _errormessage);

            }
            finally
            {
                SQLCmd = null;
            }
            return _flag;
        }
        /// <summary>
        /// Execute inline query on database
        /// </summary>
        /// <param name="SqlQuery"></param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string SqlQuery)
        {
            SqlCommand SQLCmd = new SqlCommand();
            try
            {

                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = SqlQuery;
                SQLCmd.CommandType = CommandType.Text;
                SQLConn.Open();
                SQLCmd.ExecuteNonQuery();
                _recordaffect = SQLCmd.ExecuteNonQuery();
                SQLConn.Close();
                if (_recordaffect > 0)
                    _flag = true;
                else
                    _flag = false;

            }
            catch (Exception e)
            {
                _errormessage = e.Message;

            }
            finally
            {
                SQLCmd = null;
            }
            return _flag;
        }
        /// <summary>
        /// Execute stored procedure on database with out parameter
        /// </summary>
        /// <param name="StroredProcedureName"></param>
        /// <param name="ParaMeterCollection"></param>
        /// <param name="OutPutParamerterName"></param>
        /// <param name="OutPutParamerterValue"></param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string StroredProcedureName, SqlParameter[] ParaMeterCollection, string OutPutParamerterName, out string OutPutParamerterValue)
        {
            SqlCommand SQLCmd = new SqlCommand();
            OutPutParamerterValue = "0";
            try
            {

                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = StroredProcedureName;
                SQLCmd.CommandType = CommandType.StoredProcedure;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Length; i++)
                {
                    SQLCmd.Parameters.Add(ParaMeterCollection[i]);
                }
                //End of for loop
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.InputOutput;

                SQLConn.Open();
                _recordaffect = SQLCmd.ExecuteNonQuery();
                SQLConn.Close();
                OutPutParamerterValue = Convert.ToString(SQLCmd.Parameters[OutPutParamerterName].Value);
                if (_recordaffect > 0)
                    _flag = true;
                else
                    _flag = false;
            }
            catch (Exception e)
            {
                _errormessage = e.Message;


            }
            finally
            {
                SQLCmd = null;
            }
            return _flag;
        }
        #endregion

        #region "ExecuteScaler Methods"
        /// <summary>
        /// Execute scalar with stored procedure
        /// </summary>
        /// <param name="StroredProcedureName"></param>
        /// <param name="ParaMeterCollection"></param>
        /// <returns></returns>
        public object ExecuteScaler(string StroredProcedureName, SqlParameter[] ParaMeterCollection)
        {
            object SQlObj = null;
            SqlCommand SQLCmd = new SqlCommand();
            try
            {

                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = StroredProcedureName;
                SQLCmd.CommandType = CommandType.StoredProcedure;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Length; i++)
                {
                    SQLCmd.Parameters.Add(ParaMeterCollection[i]);
                }
                //End of for loop
                SQLConn.Open();
                SQlObj = SQLCmd.ExecuteScalar();
                SQLConn.Close();
            }
            catch (Exception e)
            {
                _errormessage = e.Message;

            }
            finally
            {
                SQLCmd = null;
            }
            return SQlObj;
        }
        /// <summary>
        /// Execute scaler with inline query
        /// </summary>
        /// <param name="SqlQuery"></param>
        /// <returns></returns>
        public object ExecuteScaler(string SqlQuery)
        {
            object SQlObj = null;
            SqlCommand SQLCmd = new SqlCommand();
            try
            {
                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = SqlQuery;
                SQLCmd.CommandType = CommandType.Text;
                SQLConn.Open();
                SQlObj = SQLCmd.ExecuteScalar();
                SQLConn.Close();
            }
            catch (Exception e)
            {
                _errormessage = e.Message;

            }
            finally
            {
                SQLCmd = null;
            }
            return SQlObj;
        }

        #endregion

        #region "ExecuteDataTable Methods"
        /// <summary>
        /// Execute storedprocedure and get returned values in a datatable
        /// </summary>
        /// <param name="StroredProcedureName"></param>
        /// <param name="ParaMeterCollection"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string StroredProcedureName, SqlParameter[] ParaMeterCollection)
        {
            SqlDataAdapter SQLAdapter = new SqlDataAdapter();
            DataTable SQLDataTable = new DataTable();
            SqlCommand SQLCmd = new SqlCommand();

            try
            {

                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = StroredProcedureName;
                SQLCmd.CommandType = CommandType.StoredProcedure;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Length; i++)
                {
                    SQLCmd.Parameters.Add(ParaMeterCollection[i]);
                }
                //End of for loop
                SQLAdapter.SelectCommand = SQLCmd;
                SQLConn.Open();
                SQLAdapter.Fill(SQLDataTable);
                SQLConn.Close();
            }
            catch (Exception e)
            {
                _errormessage = e.Message;

            }
            finally
            {
                SQLCmd = null; SQLAdapter = null;
            }
            return SQLDataTable;
        }
        /// <summary>
        /// Execute inline query and return data in datatable
        /// </summary>
        /// <param name="SqlQuery"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string SqlQuery)
        {
            SqlDataAdapter SQLAdapter = new SqlDataAdapter();
            DataTable SQLDataTable = new DataTable();
            SqlCommand SQLCmd = new SqlCommand();
            try
            {
                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = SqlQuery;
                SQLCmd.CommandType = CommandType.Text;
                SQLAdapter.SelectCommand = SQLCmd;
                SQLConn.Open();
                SQLAdapter.Fill(SQLDataTable);
                SQLConn.Close();
            }
            catch (Exception e)
            {
                _errormessage = e.Message;

            }
            finally
            {
                SQLCmd = null; SQLAdapter = null;
            }
            return SQLDataTable;
        }
        #endregion

        #region "ExecuteDataSet Methods"
        /// <summary>
        /// Execute storedprocedure and get data in dataset
        /// </summary>
        /// <param name="StroredProcedureName"></param>
        /// <param name="ParaMeterCollection"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string StroredProcedureName, SqlParameter[] ParaMeterCollection)
        {
            SqlDataAdapter SQLAdapter = new SqlDataAdapter();
            DataSet SQLDataSet = new DataSet();
            SqlCommand SQLCmd = new SqlCommand();

            try
            {

                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = StroredProcedureName;
                SQLCmd.CommandType = CommandType.StoredProcedure;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Length; i++)
                {
                    SQLCmd.Parameters.Add(ParaMeterCollection[i]);
                }
                //End of for loop
                SQLAdapter.SelectCommand = SQLCmd;
                SQLConn.Open();
                SQLAdapter.Fill(SQLDataSet);
                SQLConn.Close();
            }
            catch (Exception e)
            {
                _errormessage = e.Message;

            }
            finally
            {
                SQLCmd = null; SQLAdapter = null;
            }
            return SQLDataSet;
        }
        /// <summary>
        /// Execute inline query and get data into dataset
        /// </summary>
        /// <param name="SqlQuery"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string SqlQuery)
        {
            SqlDataAdapter SQLAdapter = new SqlDataAdapter();
            DataSet SQLDataSet = new DataSet();
            SqlCommand SQLCmd = new SqlCommand();
            try
            {
                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = SqlQuery;
                SQLCmd.CommandType = CommandType.Text;
                SQLAdapter.SelectCommand = SQLCmd;
                SQLConn.Open();
                SQLAdapter.Fill(SQLDataSet);
                SQLConn.Close();
            }
            catch (Exception e)
            {
                _errormessage = e.Message;

            }
            finally
            {
                SQLCmd = null; SQLAdapter = null;
            }
            return SQLDataSet;
        }
        #endregion

        public DbDataReader ExecuteReader(string procedureName, SqlParameter[] parameters = null)
        {
            DbDataReader dr = null; // Declare the reader

            try
            {

                SqlCommand cmd = new SqlCommand();


                cmd.Connection = SQLConn;
                cmd.CommandText = procedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;



                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }


                if (SQLConn.State == ConnectionState.Closed)
                {
                    SQLConn.Open();
                }


                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error executing reader: " + ex.Message);
                throw;
            }


            return dr;
        }
    }


}

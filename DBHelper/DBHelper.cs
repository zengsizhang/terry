using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Reflection;
using System.Collections;

namespace DBHelper
{
    public class DBHelper
    {
        private static DBParaFactory dbParaFactory;

        public static DBParaFactory DbParaFactory
        {
            get
            {
                

                    DBiniClass iniclass = new DBiniClass(".\\system.ini");
                    _Connection = null;
                    string linkmodel = iniclass.IniReadValue("netlink", "linkmodel");
                    if (linkmodel == "sqlite")
                    {
                        return new SqlLiteDBParaFactory();
                    }
                    else if (linkmodel == "oracle")
                    {
                        return new OracleDBParaFactory();
                    }
                    else
                    {
                        return new SqlLiteDBParaFactory();
                    }
                    
                    return DBHelper.dbParaFactory;
               
             
            }
            set { DBHelper.dbParaFactory = value; }
        }

        private static DbConnection _Connection;
        /// <summary>
        /// 
        /// </summary>
        public static DbConnection Connection
        {
            get
            {
                try
                {
                    if (_Connection == null)
                    {
                        _Connection = DbParaFactory.GetDbConnection();
                        _Connection.Open();
                    }
                    else if (_Connection.State == ConnectionState.Closed)
                    {
                        _Connection.Open();
                    }
                    else if (_Connection.State == ConnectionState.Broken)
                    {
                        _Connection.Close();
                        _Connection.Open();
                    }
                    return _Connection;
                }
                catch (Exception er)
                {
                    throw new Exception(er.ToString());
                }
            }
        }
        public static bool Exists(string safeSql)
        {
            return (ExecuteCommand(safeSql) > 0);
        }
        public static bool Exists(string Sql, params DbParameter[] values)
        {
            return (ExecuteCommand(Sql, values) > 0);
        }
        /// <summary>
        /// 执行并返回影响行数
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public static int ExecuteCommand(string safeSql)
        {
            DbCommand cmd = cmd = DbParaFactory.GetDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = safeSql;

            int result = cmd.ExecuteNonQuery();
            return result;
        }
        /// <summary>
        /// 执行并返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int ExecuteCommand(string sql, params DbParameter[] values)
        {
            DbCommand cmd = DbParaFactory.GetDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            cmd.Parameters.AddRange(values);
            return cmd.ExecuteNonQuery();
        }

        public static string ExecuteCommand(ArrayList sqlal)
        {

            //DbCommand cmd = DbParaFactory.GetDbCommand();
            //cmd.Connection = Connection;
            //cmd.CommandType = CommandType.Text;


            //for (int i = 0; i < sqlal.Count; i++)
            //{
            //    try
            //    {
            //        cmd.CommandText = sqlal[i].ToString();
            //        cmd.ExecuteNonQuery();
            //    }
            //    catch (Exception er)
            //    { 
                
            //    }
            //}

            string result_str;
            DbCommand cmd = DbParaFactory.GetDbCommand();
            cmd.Connection = Connection;
            DbTransaction tx = Connection.BeginTransaction();
            cmd.Transaction = tx;
            string sql = "";
            string dd = System.DateTime.Now.ToString();
            try
            {
                for (int i = 0; i < sqlal.Count; i++)
                {
                    sql = sqlal[i].ToString();
                    cmd.CommandText = sqlal[i].ToString();
                    cmd.ExecuteNonQuery();
                }
                tx.Commit();
                result_str="导入成功";
            }
            catch (Exception er)
            {
                tx.Rollback();
                result_str = er.Message +";"+ sql;
             //   throw new Exception(er.Message);
            }
            string ddd1 = System.DateTime.Now.ToString();
            return result_str;
        }

        /// <summary>
        /// 执行并返回执行结果中的第一列
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public static object GetScalar(string safeSql)
        {
            DbCommand cmd = DbParaFactory.GetDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = safeSql;

            object result = cmd.ExecuteScalar();
            return result;
        }
        /// <summary>
        /// 执行并返回执行结果中的第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static object GetScalar(string sql, params DbParameter[] values)
        {
            DbCommand cmd = DbParaFactory.GetDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddRange(values);
            cmd.CommandText = sql;

            object result = cmd.ExecuteScalar();
            return result;
        }
        /// <summary>
        /// 根据sql语句获得datareader
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public static DbDataReader GetReader(string safeSql)
        {
            DbCommand cmd = DbParaFactory.GetDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = safeSql;

            DbDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        /// <summary>
        /// 根据sql语句获得datareader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static DbDataReader GetReader(string sql, params DbParameter[] values)
        {
            DbCommand cmd = DbParaFactory.GetDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(values);

            DbDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        /// <summary>
        /// 根据sql语句获得DataTable
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string safeSql)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand cmd = DbParaFactory.GetDbCommand();

                cmd.Connection = Connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = safeSql;

                DbDataAdapter da = DbParaFactory.GetDbDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);
                return ds;
            }
            catch (Exception er)
            {
                //DBiniClass iniclass = new DBiniClass(".\\system.ini");

                //iniclass.IniWriteValue("erromessate", "message", er.Message.ToString());
                throw new Exception(er.Message.ToString());

            }
        }
        /// <summary>
        /// 根据sql语句获得DataTable  
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, params DbParameter[] values)
        {
            DataSet ds = new DataSet();
            DbCommand cmd = DbParaFactory.GetDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(values);

            DbDataAdapter da = DbParaFactory.GetDbDataAdapter();
            da.SelectCommand = cmd;

            da.Fill(ds);
            return ds;
        }
        public static DataAdapter GetDataAdapter(string safeSql)
        {
            DbCommand cmd = DbParaFactory.GetDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = safeSql;

            DbDataAdapter da = DbParaFactory.GetDbDataAdapter();
            da.SelectCommand = cmd;

            return da;
        }
        /// <summary>
        /// 根据sql语句获得DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static DataAdapter GetDataAdapter(string sql, params DbParameter[] values)
        {

            DbCommand cmd = DbParaFactory.GetDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(values);

            DbDataAdapter da = DbParaFactory.GetDbDataAdapter();
            da.SelectCommand = cmd;

            return da;
        }
        /// <summary> 
        /// DataSet装换为泛型集合 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="ds">DataSet</param> 
        /// <param name="tableIndex">待转换数据表索引</param> 
        /// <returns></returns> 
        public static IList<T> DataSetToIList<T>(DataSet ds, int tableIndex)
        {
            if (ds == null || ds.Tables.Count < 0)
                return null;
            if (tableIndex > ds.Tables.Count - 1)
                return null;
            if (tableIndex < 0)
                tableIndex = 0;

            DataTable dt = ds.Tables[tableIndex];
            // 返回值初始化 
            IList<T> result = new List<T>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        if (pi.Name.Equals(dt.Columns[i].ColumnName))
                        {
                            // 数据库NULL值单独处理 
                            if (dt.Rows[j][i] != DBNull.Value)
                                pi.SetValue(_t, dt.Rows[j][i], null);
                            else
                                pi.SetValue(_t, null, null);
                            break;
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }

        /// <summary> 
        /// DataSet装换为泛型集合 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="ds">DataSet</param> 
        /// <param name="tableName">待转换数据表名称</param> 
        /// <returns></returns> 
        /// 2008-08-01 22:47 HPDV2806 
        public static IList<T> DataSetToIList<T>(DataSet ds, string tableName)
        {
            int _TableIndex = 0;
            if (ds == null || ds.Tables.Count < 0)
                return null;
            if (string.IsNullOrEmpty(tableName))
                return null;
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                // 获取Table名称在Tables集合中的索引值 
                if (ds.Tables[i].TableName.Equals(tableName))
                {
                    _TableIndex = i;
                    break;
                }
            }
            return DataSetToIList<T>(ds, _TableIndex);
        }

    }
}

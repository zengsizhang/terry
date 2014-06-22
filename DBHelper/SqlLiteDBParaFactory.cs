using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using System.IO;

namespace DBHelper
{
    class SqlLiteDBParaFactory : DBParaFactory
	{
        private static DbCommand _GetDbCommand()
        {
            return new SQLiteCommand();
        }
        private static DbConnection _GetDbConnection()
        {
            return new SQLiteConnection(ConnString);
        }
        private static DbDataAdapter _GetDbDataAdapter()
        {
            return new SQLiteDataAdapter();
        }
        private static DbParameter _GetDbParameter()
        {
            return new SQLiteParameter();
        }

        public override DbCommand GetDbCommand()
        {
            return _GetDbCommand();
        }

        public override DbConnection GetDbConnection()
        {
            GetDbConnectionString();
            return _GetDbConnection();
        }

        public override DbDataAdapter GetDbDataAdapter()
        {
            return _GetDbDataAdapter();
        }

        public override DbParameter GetDbParameter()
        {
            return _GetDbParameter();
        }

        public override string GetDbConnectionString()
        {
            try
            {
                //Data Source=timeseries.s3db;Pooling=true;FailIfMissing=false 
              //  string dir = Path.GetDirectoryName(Application.ExecutablePath);
                string str = this.GetType().Assembly.Location;
                
                //AppDomain.CurrentDomain.SetupInformation.ApplicationBase
                ConnString = "Data Source=.\\lostminer.s3db";
              
             //   ConnString = "Data Source=.//timeseries.s3db";
                return ConnString;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
	}
}

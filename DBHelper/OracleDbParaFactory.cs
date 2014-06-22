using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data.Common;
using System.IO;

namespace DBHelper
{
    class OracleDBParaFactory : DBParaFactory
    {
        private static DbCommand _GetDbCommand()
        {
            return new OracleCommand();
        }
        private static DbConnection _GetDbConnection()
        {
            return new OracleConnection(ConnString);
        }
        private static DbDataAdapter _GetDbDataAdapter()
        {
            return new OracleDataAdapter();
        }
        private static DbParameter _GetDbParameter()
        {
            return new OracleParameter();
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
                DBiniClass iniclass = new DBiniClass(".\\system.ini");

               ConnString= iniclass.IniReadValue("oraclelink", "tnsname");
               
              //   ConnString = "Provider=MSDAORA.1;User ID=IFSAPP;Password=IFSAPP;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 127.0.0.1)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = RACE)))";
              //  OleDbConnection cnn = new OleDbConnection(connString);
                //AppDomain.CurrentDomain.SetupInformation.ApplicationBase
               // ConnString = "Data Source=.\\lostminer.s3db";

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

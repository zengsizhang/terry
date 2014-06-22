using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.Common;
using System.Xml;

namespace DBHelper
{
    public abstract class DBParaFactory
    {
        private static string connString = "";

        public static string ConnString
        {
            get { return DBParaFactory.connString; }
            set { DBParaFactory.connString = value; }
        }

        public abstract string GetDbConnectionString();

        public abstract DbCommand GetDbCommand();

        public abstract DbConnection GetDbConnection();

        public abstract DbDataAdapter GetDbDataAdapter();

        public abstract DbParameter GetDbParameter();
    }
}
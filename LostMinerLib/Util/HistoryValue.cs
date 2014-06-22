
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LostMinerLib.Util
{
     [Serializable]
   public class HistoryValue
    {
        public string pointName;
        public string validFlag;
        public string datatime;
        public string datavalue;
        public string username;

    }
     [Serializable]
    public class RecordValue
    {
        public RecordValue() { }
        public string dataValue;
        public string dataTime;
    }
    [Serializable]
    public class HistoryValueResponse
    {
        public HistoryValue[] hv;
        public HistoryValueResponse() { }
    }
}

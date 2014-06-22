namespace TimeSearcher.Attribute
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class AttrType : IComparer
    {
        private readonly HorizontalAlignment _horAlignment;
        public static readonly AttrType DATETIME = new AttrType(HorizontalAlignment.Right);
        public static readonly AttrType DAYOFWEEK = new AttrType(HorizontalAlignment.Left);
        public static readonly AttrType DOUBLE = new AttrType(HorizontalAlignment.Right);
        public static readonly AttrType INTEGER = new AttrType(HorizontalAlignment.Right);
        public static readonly AttrType STRING = new AttrType(HorizontalAlignment.Left);

        protected AttrType(HorizontalAlignment horAlignment)
        {
            this._horAlignment = horAlignment;
        }

        public int Compare(object x, object y)
        {
            AttrValue value2 = (AttrValue) x;
            AttrValue value3 = (AttrValue) y;
            return value2.Compare(value3);
        }

        public static AttrValue ParseAttrValue(AttrType attrType, string strValue)
        {
            AttrValue value2;
            try
            {
                if (strValue.Trim() == "")
                {
                    return AttrValue.MISSING;
                }
                if (attrType == DATETIME)
                {
                    return new AttrDateTime(strValue);
                }
                if (attrType == DAYOFWEEK)
                {
                    return new AttrDayOfWeek(strValue);
                }
                if (attrType == DOUBLE)
                {
                    return new AttrDouble(strValue);
                }
                if (attrType == INTEGER)
                {
                    return new AttrInteger(strValue);
                }
                if (attrType == STRING)
                {
                    return new AttrString(strValue);
                }
                value2 = null;
            }
            catch (Exception exception)
            {
                throw new Exception(strValue + " incorrect.", exception);
            }
            return value2;
        }

        public static AttrValue[] ParseAttrValues(AttrType[] attrTypes, string[] strValues)
        {
            AttrValue[] valueArray = new AttrValue[strValues.Length];
            for (int i = 0; i < attrTypes.Length; i++)
            {
                valueArray[i] = ParseAttrValue(attrTypes[i], strValues[i]);
            }
            return valueArray;
        }

        public static AttrType[] ParseTypeArray(string[] strTypes)
        {
            AttrType[] typeArray = new AttrType[strTypes.Length];
            for (int i = 0; i < typeArray.Length; i++)
            {
                AttrType sTRING;
                switch (strTypes[i])
                {
                    case "STRING":
                        sTRING = STRING;
                        break;

                    case "INTEGER":
                        sTRING = INTEGER;
                        break;

                    case "DOUBLE":
                        sTRING = DOUBLE;
                        break;

                    case "DATETIME":
                        sTRING = DATETIME;
                        break;

                    case "DAYOFWEEK":
                        sTRING = DAYOFWEEK;
                        break;

                    default:
                        sTRING = STRING;
                        break;
                }
                typeArray[i] = sTRING;
            }
            return typeArray;
        }

        public HorizontalAlignment HorAlignment
        {
            get
            {
                return HorizontalAlignment.Left;
            }
        }
    }
}


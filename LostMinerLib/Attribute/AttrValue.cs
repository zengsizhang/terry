namespace TimeSearcher.Attribute
{
    using System;
    using System.Collections;

    public abstract class AttrValue : IComparer
    {
        protected readonly string _strValue;
        public static AttrValue MISSING = new AttrMissing();

        protected AttrValue(string strValue)
        {
            this._strValue = strValue;
        }

        public abstract int Compare(AttrValue val2);
        public int Compare(object x, object y)
        {
            AttrValue value2 = (AttrValue) x;
            AttrValue value3 = (AttrValue) y;
            return value2.Compare(value3);
        }

        public static string[] GetStrValues(AttrValue[] attrValues)
        {
            string[] strArray = new string[attrValues.Length];
            for (int i = 0; i < attrValues.Length; i++)
            {
                strArray[i] = attrValues[i].StrValue;
            }
            return strArray;
        }

        public string StrValue
        {
            get
            {
                return this._strValue;
            }
        }
    }
}


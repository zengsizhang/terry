namespace TimeSearcher.Attribute
{
    using System;
    using System.Collections;

    public class AttrDayOfWeek : AttrValue
    {
        private readonly int _attrValue;
        private Hashtable _htDayOfWeek;

        public AttrDayOfWeek(string strValue) : base(strValue)
        {
            this._htDayOfWeek = new Hashtable();
            string[] strArray = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            for (int i = 0; i < strArray.Length; i++)
            {
                this._htDayOfWeek.Add(strArray[i], i);
            }
            this._attrValue = this.ordinal(strValue);
        }

        public override int Compare(AttrValue val2)
        {
            if (val2 is AttrMissing)
            {
                return (-1 * val2.Compare(this));
            }
            AttrDayOfWeek week = (AttrDayOfWeek) val2;
            return (this._attrValue - week._attrValue);
        }

        private int ordinal(string strDayOfWeek)
        {
            return (int) this._htDayOfWeek[strDayOfWeek];
        }
    }
}


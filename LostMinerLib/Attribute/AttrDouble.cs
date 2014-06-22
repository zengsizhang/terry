namespace TimeSearcher.Attribute
{
    using System;

    public class AttrDouble : AttrNumber
    {
        private readonly double _attrValue;

        public AttrDouble(double dblValue) : base(Convert.ToString(dblValue))
        {
            this._attrValue = dblValue;
        }

        public AttrDouble(string strValue) : base(strValue)
        {
            this._attrValue = Convert.ToDouble(strValue);
        }

        public override double DblValue
        {
            get
            {
                return this._attrValue;
            }
        }
    }
}


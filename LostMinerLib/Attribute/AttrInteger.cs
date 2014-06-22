namespace TimeSearcher.Attribute
{
    using System;

    public class AttrInteger : AttrNumber
    {
        private readonly int _attrValue;

        public AttrInteger(string strValue) : base(strValue)
        {
            this._attrValue = Convert.ToInt32(strValue);
        }

        public override double DblValue
        {
            get
            {
                return (double) this._attrValue;
            }
        }
    }
}


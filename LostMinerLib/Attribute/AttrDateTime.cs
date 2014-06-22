namespace TimeSearcher.Attribute
{
    using System;

    public class AttrDateTime : AttrValue
    {
        private readonly DateTime _attrValue;

        public AttrDateTime(string strValue) : base(strValue)
        {
            this._attrValue = Convert.ToDateTime(strValue);
        }

        public override int Compare(AttrValue val2)
        {
            if (val2 is AttrMissing)
            {
                return (-1 * val2.Compare(this));
            }
            AttrDateTime time = (AttrDateTime) val2;
            return this._attrValue.CompareTo(time._attrValue);
        }
    }
}


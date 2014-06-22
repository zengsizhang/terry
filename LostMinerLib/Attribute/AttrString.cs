namespace TimeSearcher.Attribute
{
    using System;
    using System.Collections;

    public class AttrString : AttrValue
    {
        private readonly string _attrValue;
        private IComparer _ciComparer;

        public AttrString(string strValue) : base(strValue)
        {
            this._attrValue = strValue;
            this._ciComparer = new CaseInsensitiveComparer();
        }

        public override int Compare(AttrValue val2)
        {
            if (val2 is AttrMissing)
            {
                return (-1 * val2.Compare(this));
            }
            AttrString str = (AttrString) val2;
            return this._ciComparer.Compare(this._attrValue, str._attrValue);
        }
    }
}


namespace TimeSearcher.Attribute
{
    using System;

    public abstract class AttrNumber : AttrValue
    {
        public AttrNumber(string strValue) : base(strValue)
        {
        }

        public override int Compare(AttrValue val2)
        {
            if (val2 is AttrMissing)
            {
                return (-1 * val2.Compare(this));
            }
            AttrNumber number = (AttrNumber) val2;
            return this.DblValue.CompareTo(number.DblValue);
        }

        public abstract double DblValue { get; }
    }
}


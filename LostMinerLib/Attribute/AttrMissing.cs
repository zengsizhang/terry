namespace TimeSearcher.Attribute
{
    using System;

    public class AttrMissing : AttrValue
    {
        public AttrMissing() : base("")
        {
        }

        public override int Compare(AttrValue val2)
        {
            if (val2 is AttrMissing)
            {
                return 0;
            }
            return 1;
        }
    }
}


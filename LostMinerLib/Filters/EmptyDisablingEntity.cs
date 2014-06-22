namespace TimeSearcher.Filters
{
    using System;
    using TimeSearcher;

    public class EmptyDisablingEntity : DisablingEntity
    {
        public void Dematerialize()
        {
        }

        public bool Filters(DataItem di)
        {
            return false;
        }
    }
}


namespace TimeSearcher.Filters
{
    using System;
    using TimeSearcher;

    public interface DisablingEntity
    {
        void Dematerialize();
        bool Filters(DataItem di);
    }
}


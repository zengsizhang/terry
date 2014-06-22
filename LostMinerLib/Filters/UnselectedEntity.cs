namespace TimeSearcher.Filters
{
    using System;
    using TimeSearcher;

    public class UnselectedEntity : ExcludingEntity
    {
        public UnselectedEntity(int[] excludedItemIdx, DisablingManager disablingManager) : base(excludedItemIdx, disablingManager)
        {
        }
    }
}


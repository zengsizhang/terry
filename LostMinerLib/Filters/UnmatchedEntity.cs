namespace TimeSearcher.Filters
{
    using System;
    using TimeSearcher;

    public class UnmatchedEntity : ExcludingEntity
    {
        public UnmatchedEntity(int[] excludedItemIdx, DisablingManager disablingManager) : base(excludedItemIdx, disablingManager)
        {
        }
    }
}


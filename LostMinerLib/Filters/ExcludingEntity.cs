namespace TimeSearcher.Filters
{
    using System;
    using System.Collections;
    using TimeSearcher;

    public class ExcludingEntity : DisablingEntity
    {
        private readonly DisablingManager _disablingManager;
        private Hashtable _htExcludedItemIdx;

        public ExcludingEntity(int[] excludedItemIdx, DisablingManager disablingManager)
        {
            this._disablingManager = disablingManager;
            this._htExcludedItemIdx = new Hashtable();
            for (int i = 0; i < excludedItemIdx.Length; i++)
            {
                this._htExcludedItemIdx.Add(excludedItemIdx[i], "");
            }
        }

        public void Dematerialize()
        {
            this._disablingManager.RemoveEntity(this);
        }

        public bool Filters(DataItem di)
        {
            return this._htExcludedItemIdx.Contains(di.ItemIdx);
        }
    }
}


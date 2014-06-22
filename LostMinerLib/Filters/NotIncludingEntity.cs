namespace TimeSearcher.Filters
{
    using System;
    using System.Collections;
    using TimeSearcher;

    public class NotIncludingEntity : DisablingEntity
    {
        private readonly DisablingManager _disablingManager;
        private Hashtable _htIncludedItemIdx;

        public NotIncludingEntity(int[] includedItemIdx, DisablingManager disablingManager)
        {
            this._disablingManager = disablingManager;
            this._htIncludedItemIdx = new Hashtable();
            for (int i = 0; i < includedItemIdx.Length; i++)
            {
                this._htIncludedItemIdx.Add(includedItemIdx[i], "");
            }
        }

        public void Dematerialize()
        {
            this._disablingManager.RemoveEntity(this);
        }

        public bool Filters(DataItem di)
        {
            return !this._htIncludedItemIdx.Contains(di.ItemIdx);
        }
    }
}


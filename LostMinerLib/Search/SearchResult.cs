namespace TimeSearcher.Search
{
    using System;
    using System.Collections;

    public class SearchResult
    {
        private readonly int _patLen;
        private readonly int[] _timePtIndices;
        public static readonly SearchResult Empty = new SearchResult(new ArrayList(), 1);

        public SearchResult(ArrayList alTimePtIndices, int patLen)
        {
            this._timePtIndices = (int[]) alTimePtIndices.ToArray(typeof(int));
            this._patLen = patLen;
        }

        public bool IsSameAs(SearchResult sr2)
        {
            if (this._patLen != sr2._patLen)
            {
                return false;
            }
            if (this._timePtIndices.Length != sr2._timePtIndices.Length)
            {
                return false;
            }
            for (int i = 0; i < this._timePtIndices.Length; i++)
            {
                if (this._timePtIndices[i] != sr2._timePtIndices[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool HasResults
        {
            get
            {
                return (this._timePtIndices.Length > 0);
            }
        }

        public int PatLen
        {
            get
            {
                return this._patLen;
            }
        }

        public int[] TimePtIndices
        {
            get
            {
                return this._timePtIndices;
            }
        }
    }
}


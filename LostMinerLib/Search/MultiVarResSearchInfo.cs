namespace TimeSearcher.Search
{
    using System;

    public class MultiVarResSearchInfo
    {
        private int _hitCount;
        private int _patEndIndex;
        private int _patStartIndex;
        private TimeSearcher.Search.SearchOptions _so;
        private int _tolerance;
        private int[] _variables;

        public MultiVarResSearchInfo(int patStartIndex, int patEndIndex, int hitCount, int tolerance, TimeSearcher.Search.SearchOptions so, int[] variables)
        {
            this._patStartIndex = patStartIndex;
            this._patEndIndex = patEndIndex;
            this._hitCount = hitCount;
            this._tolerance = tolerance;
            this._so = so;
            this._variables = variables;
        }

        public int HitCount
        {
            get
            {
                return this._hitCount;
            }
        }

        public int PatEndIndex
        {
            get
            {
                return this._patEndIndex;
            }
        }

        public int PatStartIndex
        {
            get
            {
                return this._patStartIndex;
            }
        }

        public TimeSearcher.Search.SearchOptions SearchOptions
        {
            get
            {
                return this._so;
            }
            set
            {
                this._so = value;
            }
        }

        public int Tolerance
        {
            get
            {
                return this._tolerance;
            }
        }

        public int[] Variables
        {
            get
            {
                return this._variables;
            }
        }
    }
}


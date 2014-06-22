namespace TimeSearcher.Search
{
    using System;

    public class SearchOptions
    {
        protected SearchAlgo.Algo _algo;
        protected bool _amplitudeScaling;
        protected bool _autoSearch;
        protected bool _linearTrend;
        protected bool _noiseReduction;
        protected bool _offsetTranslation;
        private readonly SearchOptionsView _searchOptionsView;

        public SearchOptions()
        {
            this.setLT(false).setOT(true).setAS(true).setNR(false).setAlgo(SearchAlgo.Algo.Envelope).setAutoSearch(true);
            this._searchOptionsView = new SearchOptionsView(this);
        }

        public SearchAlgo.Algo getAlgo()
        {
            return this._algo;
        }

        public SearchOptionsView getView()
        {
            return this._searchOptionsView;
        }

        public bool hasAS()
        {
            return this._amplitudeScaling;
        }

        public bool hasAutoSearch()
        {
            return this._autoSearch;
        }

        public bool hasLT()
        {
            return this._linearTrend;
        }

        public bool hasNR()
        {
            return this._noiseReduction;
        }

        public bool hasOT()
        {
            return this._offsetTranslation;
        }

        public SearchOptions setAlgo(SearchAlgo.Algo algo)
        {
            this._algo = algo;
            return this;
        }

        public SearchOptions setAS(bool cond)
        {
            this._amplitudeScaling = cond;
            return this;
        }

        public SearchOptions setAutoSearch(bool cond)
        {
            this._autoSearch = cond;
            return this;
        }

        public SearchOptions setLT(bool cond)
        {
            this._linearTrend = cond;
            return this;
        }

        public SearchOptions setNR(bool cond)
        {
            this._noiseReduction = cond;
            return this;
        }

        public SearchOptions setOT(bool cond)
        {
            this._offsetTranslation = cond;
            return this;
        }

        public bool[] ToBoolArray
        {
            get
            {
                bool[] flagArray = new bool[4];
                flagArray[2] = this._amplitudeScaling;
                flagArray[0] = this._linearTrend;
                flagArray[3] = this._noiseReduction;
                flagArray[1] = this._offsetTranslation;
                return flagArray;
            }
        }
    }
}


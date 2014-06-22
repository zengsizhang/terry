namespace TimeSearcher.Search
{
    using System;

    public class SearchOptionsView
    {
        private readonly SearchOptions _searchOpts;

        public SearchOptionsView(SearchOptions searchOptions)
        {
            this._searchOpts = searchOptions;
        }

        public SearchAlgo.Algo getAlgo()
        {
            return this._searchOpts.getAlgo();
        }

        public int getKey()
        {
            int num = 1;
            if (this.hasLinearT())
            {
                num *= 2;
            }
            if (this.hasOffsetT())
            {
                num *= 3;
            }
            if (this.hasAmplitudeS())
            {
                num *= 5;
            }
            if (this.hasNoiseR())
            {
                num *= 7;
            }
            return num;
        }

        public string getString()
        {
            string str = "";
            if (this.hasLinearT())
            {
                str = str + "LT ";
            }
            if (this.hasOffsetT())
            {
                str = str + "OT ";
            }
            if (this.hasAmplitudeS())
            {
                str = str + "AS ";
            }
            if (this.hasNoiseR())
            {
                str = str + "NR ";
            }
            return str;
        }

        public bool hasAmplitudeS()
        {
            return this._searchOpts.hasAS();
        }

        public bool hasAutoSearch()
        {
            return this._searchOpts.hasAutoSearch();
        }

        public bool hasLinearT()
        {
            return this._searchOpts.hasLT();
        }

        public bool hasNoiseR()
        {
            return this._searchOpts.hasNR();
        }

        public bool hasOffsetT()
        {
            return this._searchOpts.hasOT();
        }
    }
}


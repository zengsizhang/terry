namespace TimeSearcher.Search
{
    using System;
    using System.Collections;

    public abstract class SearchAlgo
    {
        protected readonly double[] _pattern;
        protected readonly SearchOptionsView _searchOptsView;
        protected double _toleranceValue;

        public SearchAlgo(double[] patternC, SearchOptionsView searchOptsView)
        {
            this._pattern = patternC;
            this._searchOptsView = searchOptsView;
        }

        public static SearchAlgo NewInstance(double[] patternC, SearchOptionsView searchOptsView)
        {
            switch (searchOptsView.getAlgo())
            {
                case Algo.Envelope:
                    return new EnvelopeSearch(patternC, searchOptsView);

                case Algo.Euclidean:
                    return new EuclideanSearch(patternC, searchOptsView);
            }
            return null;
        }

        public abstract ArrayList NormalizedSearch(double[] series);
        public abstract void SetTolerancePerc(int tolerancePerc);

        public enum Algo
        {
            Envelope,
            Euclidean,
            None
        }
    }
}


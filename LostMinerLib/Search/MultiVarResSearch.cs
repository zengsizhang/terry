namespace TimeSearcher.Search
{
    using System;
    using System.Collections;
    using TimeSearcher;

    public class MultiVarResSearch
    {
        private DataSet _dataSet;
        private int _endIdx;
        private int _lastHitCount;
        private double[][] _patterns;
        private SearchOptions _searchOpts;
        private int _startIdx;
        private int _tolerance;
        private int[] _varIdx;

        public MultiVarResSearch(DataSet dataSet, SearchOptions searchOpts, double[][] patterns, int patStartIdx, int patEndIdx)
        {
            this._dataSet = dataSet;
            this._patterns = patterns;
            this._startIdx = patStartIdx;
            this._endIdx = patEndIdx;
            this._searchOpts = searchOpts;
        }

        public static SearchOptions GetDefaultSearchOpts()
        {
            SearchOptions options = new SearchOptions();
            options.setAlgo(SearchAlgo.Algo.Euclidean);
            options.setAS(false);
            return options;
        }

        public static double[][] GetPatternsFromItemIdx(DataSet dataSet, int itemIdx)
        {
            double[][] numArray = new double[dataSet.NumDynVar][];
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = dataSet.GetItem(itemIdx).GetVar(i).getValues();
            }
            return numArray;
        }

        public int[] PerformSearch(int percTolerance, int[] varIdx)
        {
            ArrayList enabledItemIdxList = this._dataSet.GetEnabledItemIdxList();
            ArrayList list3 = new ArrayList();
            this._tolerance = percTolerance;
            this._varIdx = varIdx;
            for (int i = 0; i < varIdx.Length; i++)
            {
                double[] destinationArray = new double[this._endIdx - this._startIdx];
                Array.Copy(this._patterns[varIdx[i]], this._startIdx, destinationArray, 0, destinationArray.Length);
                for (int j = 0; j < enabledItemIdxList.Count; j++)
                {
                    SearchAlgo algo = SearchAlgo.NewInstance(destinationArray, this._searchOpts.getView());
                    algo.SetTolerancePerc(percTolerance);
                    if (algo.NormalizedSearch(this._dataSet.GetItem((int) enabledItemIdxList[j]).GetVar(varIdx[i]).getValues(this._startIdx, this._endIdx)).Count == 0)
                    {
                        list3.Add(enabledItemIdxList[j]);
                    }
                }
                foreach (object obj2 in list3)
                {
                    enabledItemIdxList.Remove(obj2);
                }
                list3.Clear();
            }
            this._lastHitCount = enabledItemIdxList.Count;
            return (int[]) enabledItemIdxList.ToArray(typeof(int));
        }

        public MultiVarResSearchInfo MVRSInfo
        {
            get
            {
                return new MultiVarResSearchInfo(this._startIdx, this._endIdx, this._lastHitCount, this._tolerance, this._searchOpts, this._varIdx);
            }
        }

        public SearchOptions SearchOpts
        {
            get
            {
                return this._searchOpts;
            }
        }
    }
}


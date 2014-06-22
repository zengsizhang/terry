namespace TimeSearcher.Search
{
    using System;
    using System.Collections;

    public class EnvelopeSearch : SearchAlgo
    {
        public EnvelopeSearch(double[] patternC, SearchOptionsView searchOptsView) : base(patternC, searchOptsView)
        {
        }

        private bool matches1by1(double[] series, int startIdx, double[] pattern)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                if (Math.Abs((double) (series[startIdx + i] - pattern[i])) > base._toleranceValue)
                {
                    return false;
                }
            }
            return true;
        }

        public override ArrayList NormalizedSearch(double[] series)
        {
            double[] pattern = new double[base._pattern.Length];
            pattern = Statistics.getFullNormalized(base._pattern, base._searchOptsView);
            ArrayList list = new ArrayList();
            for (int i = 0; i < ((series.Length - pattern.Length) + 1); i++)
            {
                double[] destinationArray = new double[pattern.Length];
                Array.Copy(series, i, destinationArray, 0, destinationArray.Length);
                destinationArray = Statistics.getFullNormalized(destinationArray, base._searchOptsView);
                if (this.matches1by1(destinationArray, 0, pattern))
                {
                    list.Add(i);
                }
            }
            return list;
        }

        public override void SetTolerancePerc(int tolerancePerc)
        {
            double num = Statistics.getRange(Statistics.getFullNormalized(base._pattern, base._searchOptsView));
            base._toleranceValue = (((double) tolerancePerc) / 100.0) * num;
        }
    }
}


namespace TimeSearcher.Search
{
    using System;
    using System.Collections;

    public class EuclideanSearch : SearchAlgo
    {
        public EuclideanSearch(double[] patternC, SearchOptionsView searchOptsView) : base(patternC, searchOptsView)
        {
        }

        private bool matches(double[] series, int startIdx, double[] pattern)
        {
            return (optimizedEuclideanDistance(series, startIdx, pattern) <= base._toleranceValue);
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
                if (this.matches(destinationArray, 0, pattern))
                {
                    list.Add(i);
                }
            }
            return list;
        }

        private static double optimizedEuclideanDistance(double[] Q, int startIndex, double[] C)
        {
            double num = 0.0;
            for (int i = 0; (i < (Q.Length - startIndex)) && (i < C.Length); i++)
            {
                num += Math.Pow(Q[i + startIndex] - C[i], 2.0);
            }
            return num;
        }

        public override void SetTolerancePerc(int tolerancePerc)
        {
            double[] values = Statistics.getFullNormalized(base._pattern, base._searchOptsView);
            double num = Statistics.getRange(values);
            base._toleranceValue = (((double) tolerancePerc) / 100.0) * num;
            base._toleranceValue *= base._toleranceValue;
            base._toleranceValue *= values.Length;
        }
    }
}


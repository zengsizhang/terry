namespace Util.Stat
{
    using System;

    public class StatAlgo
    {
        private StatAlgo()
        {
        }

        public static double GetMeanOfDoubles(double[] dblValues)
        {
            double num = 0.0;
            for (int i = 0; i < dblValues.Length; i++)
            {
                num += dblValues[i];
            }
            return (num / ((double) dblValues.Length));
        }

        public static double GetStdevOfDoubles(double[] dblValues, double mean)
        {
            double d = 0.0;
            for (int i = 0; i < dblValues.Length; i++)
            {
                d += Math.Pow(dblValues[i] - mean, 2.0);
            }
            d /= (double) (dblValues.Length - 1);
            return Math.Sqrt(d);
        }
    }
}


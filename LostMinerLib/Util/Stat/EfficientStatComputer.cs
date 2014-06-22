namespace Util.Stat
{
    using System;
    using System.Collections;
    using TimeSearcher.Attribute;

    public class EfficientStatComputer
    {
        private double[] _dblStats;
        private readonly StatType[] _includedStats;
        private const double MISSING_DblStat = double.NaN;

        public EfficientStatComputer(double[] dblValues, StatType[] includedStats)
        {
            this._dblStats = new double[StatType.MaxOrd];
            this._includedStats = includedStats;
            this.computeStats(dblValues);
        }

        public EfficientStatComputer(AttrValue[] statAtrs, StatType[] includedStats) : this(toDoubleArray(statAtrs), includedStats)
        {
        }

        private void computeNoStats()
        {
            for (int i = 0; i < StatType.MaxOrd; i++)
            {
                this._dblStats[i] = double.NaN;
            }
        }

        private void computeStats(double[] dblAtrValues)
        {
            if (dblAtrValues.Length == 0)
            {
                this.computeNoStats();
            }
            else
            {
                bool[] flagArray = this.getBoolInclStats();
                Array.Sort<double>(dblAtrValues);
                if (flagArray[StatType.MIN.Ord])
                {
                    this._dblStats[StatType.MIN.Ord] = dblAtrValues[0];
                }
                if (flagArray[StatType.Q1.Ord])
                {
                    this._dblStats[StatType.Q1.Ord] = dblAtrValues[dblAtrValues.Length / 4];
                }
                if (flagArray[StatType.MED.Ord])
                {
                    this._dblStats[StatType.MED.Ord] = dblAtrValues[(2 * dblAtrValues.Length) / 4];
                }
                if (flagArray[StatType.Q3.Ord])
                {
                    this._dblStats[StatType.Q3.Ord] = dblAtrValues[(3 * dblAtrValues.Length) / 4];
                }
                if (flagArray[StatType.MAX.Ord])
                {
                    this._dblStats[StatType.MAX.Ord] = dblAtrValues[dblAtrValues.Length - 1];
                }
                if (flagArray[StatType.MEAN.Ord] || flagArray[StatType.STDEV.Ord])
                {
                    double meanOfDoubles = StatAlgo.GetMeanOfDoubles(dblAtrValues);
                    this._dblStats[StatType.MEAN.Ord] = meanOfDoubles;
                    if (flagArray[StatType.STDEV.Ord])
                    {
                        double stdevOfDoubles = StatAlgo.GetStdevOfDoubles(dblAtrValues, meanOfDoubles);
                        this._dblStats[StatType.STDEV.Ord] = stdevOfDoubles;
                    }
                }
                for (int i = 0; i < StatType.MaxOrd; i++)
                {
                    this._dblStats[i] = Convert.ToDouble(Math.Round((double) (this._dblStats[i] * 100.0))) / 100.0;
                }
            }
        }

        private bool[] getBoolInclStats()
        {
            bool[] flagArray = new bool[StatType.MaxOrd];
            for (int i = 0; i < flagArray.Length; i++)
            {
                flagArray[i] = false;
            }
            for (int j = 0; j < this._includedStats.Length; j++)
            {
                flagArray[this._includedStats[j].Ord] = true;
            }
            return flagArray;
        }

        public double GetDoubleStat(StatType statType)
        {
            return this._dblStats[statType.Ord];
        }

        public double[] GetDoubleStats(StatType[] statTypes)
        {
            double[] numArray = new double[statTypes.Length];
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = this.GetDoubleStat(statTypes[i]);
            }
            return numArray;
        }

        public AttrValue GetStat(StatType statType)
        {
            return new AttrDouble(this._dblStats[statType.Ord]);
        }

        public AttrValue[] GetStats(StatType[] statTypes)
        {
            AttrValue[] valueArray = new AttrValue[statTypes.Length];
            for (int i = 0; i < valueArray.Length; i++)
            {
                valueArray[i] = this.GetStat(statTypes[i]);
            }
            return valueArray;
        }

        private static double[] toDoubleArray(AttrValue[] statAtrs)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < statAtrs.Length; i++)
            {
                if (statAtrs[i] is AttrNumber)
                {
                    list.Add(((AttrNumber) statAtrs[i]).DblValue);
                }
            }
            return (double[]) list.ToArray(typeof(double));
        }
    }
}


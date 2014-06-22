namespace Util.Stat
{
    using System;

    public class StatType
    {
        private static int _maxOrd = 0;
        private readonly string _name;
        private readonly int _ord;
        public static readonly StatType MAX = new StatType("MAX");
        public static readonly StatType MEAN = new StatType("MEAN");
        public static readonly StatType MED = new StatType("MED");
        public static readonly StatType MIN = new StatType("MIN");
        public static readonly StatType Q1 = new StatType("Q1");
        public static readonly StatType Q3 = new StatType("Q3");
        public static readonly StatType STDEV = new StatType("STDEV");

        protected StatType(string name)
        {
            this._name = name;
            this._ord = _maxOrd;
            _maxOrd++;
        }

        public static int MaxOrd
        {
            get
            {
                return _maxOrd;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public int Ord
        {
            get
            {
                return this._ord;
            }
        }
    }
}


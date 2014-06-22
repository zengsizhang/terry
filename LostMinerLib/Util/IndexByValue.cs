namespace Util
{
    using System;

    public class IndexByValue : IComparable
    {
        private readonly int _index;
        private readonly double _value;

        public IndexByValue(int index, double val)
        {
            this._index = index;
            this._value = val;
        }

        public int CompareTo(object obj)
        {
            IndexByValue value2 = (IndexByValue) obj;
            if (this._value < value2._value)
            {
                return -1;
            }
            if (this._value > value2._value)
            {
                return 1;
            }
            return 0;
        }

        public int Index
        {
            get
            {
                return this._index;
            }
        }

        public double Value
        {
            get
            {
                return this._value;
            }
        }
    }
}


namespace TimeSearcher.Search
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SearchKey
    {
        private readonly int _varIndex;
        private readonly int _itemIndex;
        private readonly int _patStartIndex;
        private readonly int _patLength;
        private readonly int _percToler;
        private readonly int _sovKey;
        public SearchKey(int varIdx, int itemIdx, int patStartIdx, int patLength, int percToler, int sovKey)
        {
            this._varIndex = varIdx;
            this._itemIndex = itemIdx;
            this._patStartIndex = patStartIdx;
            this._patLength = patLength;
            this._percToler = percToler;
            this._sovKey = sovKey;
        }

        public override int GetHashCode()
        {
            string str = "";
            return string.Concat(new object[] { str, this._varIndex, ":", this._itemIndex, ":", this._patStartIndex, ":", this._patLength, ":", this._percToler, ":", this._sovKey }).GetHashCode();
        }
    }
}


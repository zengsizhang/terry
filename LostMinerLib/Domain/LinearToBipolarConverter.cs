namespace TimeSearcher.Domain
{
    using System;

    public class LinearToBipolarConverter
    {
        private int _indexDiff;
        private int _middleGran;
        private int _pole1EndMIndex;
        private int _pole1EndPIndex;
        private int _pole2StartMIndex;
        private int _pole2StartPIndex;
        private int _poleGran;

        public LinearToBipolarConverter(int middleGran, int poleGran, int pole1EndVisibleUnits, int pole2StartVisibleUnits)
        {
            this._middleGran = middleGran;
            this._poleGran = poleGran;
            this._pole1EndPIndex = this.relativeUnitsToPIndex(pole1EndVisibleUnits);
            this._pole2StartPIndex = this.relativeUnitsToPIndex(pole2StartVisibleUnits);
            this._indexDiff = this.getIndexDiff(pole2StartVisibleUnits - pole1EndVisibleUnits);
            this._pole1EndMIndex = this.toBipolar(this._pole1EndPIndex);
            this._pole2StartMIndex = this.toBipolar(this._pole2StartPIndex);
        }

        private int getIndexDiff(int middleUnit)
        {
            return ((middleUnit / this._poleGran) - (middleUnit / this._middleGran));
        }

        public int pIndexToRelativeUnits(int pIndex)
        {
            return (pIndex * this._poleGran);
        }

        public int relativeUnitsToPIndex(int units)
        {
            return (units / this._poleGran);
        }

        public int toBipolar(int pIndex)
        {
            if (pIndex <= this._pole1EndPIndex)
            {
                return pIndex;
            }
            if (pIndex <= this._pole2StartPIndex)
            {
                int num = ((pIndex - this._pole1EndPIndex) * this._poleGran) / this._middleGran;
                return (this._pole1EndPIndex + num);
            }
            return (pIndex - this._indexDiff);
        }

        public int toPIndex(int bipolarIndex)
        {
            if (bipolarIndex <= this._pole1EndMIndex)
            {
                return bipolarIndex;
            }
            if (bipolarIndex <= this._pole2StartMIndex)
            {
                int num = ((bipolarIndex - this._pole1EndMIndex) * this._middleGran) / this._poleGran;
                return (this._pole1EndMIndex + num);
            }
            return (bipolarIndex + this._indexDiff);
        }
    }
}


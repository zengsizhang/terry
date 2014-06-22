namespace TimeSearcher.Domain
{
    using System;

    public class UniformBipolarDateTimeDomain : UniformDateTimeDomain
    {
        private LinearToBipolarConverter _indexConverter;
        private readonly int _poleIndexCount;

        public UniformBipolarDateTimeDomain(string[] strDomainValues, TimeSpan poleLength, TimeSpan poleGran, TimeSpan middleGran) : base(strDomainValues)
        {
            DateTime time = base._dtValues[0];
            DateTime time2 = base._dtValues[base._dtValues.Length - 1];
            Console.WriteLine("middleGran = " + middleGran.TotalMinutes + " minutes");
            Console.WriteLine("poleGran = " + poleGran.TotalSeconds + " seconds");
            Console.WriteLine("poleLength = " + poleLength.TotalSeconds + " seconds");
            Console.WriteLine("firstDt = " + time.ToString());
            Console.WriteLine("pole1End = " + time.Add(poleLength).ToString());
            Console.WriteLine("pole2Start = " + time2.Subtract(poleLength).ToString());
            Console.WriteLine("lastDt = " + time2.ToString());
            this._poleIndexCount = base.toUnits(poleLength) / base.toUnits(poleGran);
            this._indexConverter = new LinearToBipolarConverter(base.toUnits(middleGran), base.toUnits(poleGran), base.toRelativeUnits(time.Add(poleLength)), base.toRelativeUnits(time2.Subtract(poleLength)));
        }

        private UniformBipolarDateTimeDomain(LinearToBipolarConverter indexConverter, int poleIndexCount, DateTime[] dtValues, int startIndex, int endIndex, int startPixel, int endPixel) : base(dtValues, startIndex, endIndex, startPixel, endPixel)
        {
            this._poleIndexCount = poleIndexCount;
            this._indexConverter = indexConverter;
        }

        public override int getCoordinateFromIndex(int middleMIndex)
        {
            int pIndex = this._indexConverter.toPIndex(middleMIndex);
            int units = this._indexConverter.pIndexToRelativeUnits(pIndex) - this.getRelativeUnits(base._startIndex);
            return base.getCoordinateFromVisibleUnits(units);
        }

        public override int getFloorIndexFromCoordinate(int coord)
        {
            int units = base.getVisibleUnitsFlooredFromCoordinate(coord) + this.getRelativeUnits(base._startIndex);
            int pIndex = this._indexConverter.relativeUnitsToPIndex(units);
            return this._indexConverter.toBipolar(pIndex);
        }

        public override UniformDomain GetIndependentCopy()
        {
            return new UniformBipolarDateTimeDomain(this._indexConverter, this._poleIndexCount, base._dtValues, base._startIndex, base._endIndex, base._startPixel, base._endPixel);
        }

        public override int getIndexFromCoordinate(int coord)
        {
            int units = base.getVisibleUnitsRoundedFromCoordinate(coord) + this.getRelativeUnits(base._startIndex);
            int pIndex = this._indexConverter.relativeUnitsToPIndex(units);
            return this._indexConverter.toBipolar(pIndex);
        }

        public int PoleIndexCount
        {
            get
            {
                return this._poleIndexCount;
            }
        }
    }
}


namespace TimeSearcher.Domain
{
    using System;

    public abstract class UniformDateTimeDomain : UniformDomain
    {
        protected readonly DateTime[] _dtValues;

        public UniformDateTimeDomain(string[] strDomainValues) : this(Utils.strArr2DateTime(strDomainValues), 0, strDomainValues.Length - 1)
        {
        }

        private UniformDateTimeDomain(DateTime[] dtValues, int startIndex, int endIndex) : this(dtValues, startIndex, endIndex, startIndex, endIndex)
        {
        }

        protected UniformDateTimeDomain(DateTime[] dtValues, int startIndex, int endIndex, int startPixel, int endPixel)
        {
            this._dtValues = dtValues;
            base.init(startIndex, endIndex, startPixel, endPixel);
        }

        protected override int getRelativeUnits(int middleIndex)
        {
            return this.toRelativeUnits(this._dtValues[middleIndex]);
        }

        protected override int getVisibleUnits(int middleIndex)
        {
            return this.toVisibleUnits(this._dtValues[middleIndex]);
        }

        protected int toRelativeUnits(DateTime dTime)
        {
            return this.toUnits(dTime.Subtract(this._dtValues[0]));
        }

        protected int toUnits(TimeSpan tSpan)
        {
            return (int) tSpan.TotalSeconds;
        }

        protected int toVisibleUnits(DateTime dTime)
        {
            return this.toUnits(dTime.Subtract(this._dtValues[base._startIndex]));
        }
    }
}


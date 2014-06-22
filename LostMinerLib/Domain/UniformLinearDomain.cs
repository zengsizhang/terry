namespace TimeSearcher.Domain
{
    using System;

    public class UniformLinearDomain : UniformDomain
    {
        public UniformLinearDomain(int startIndex, int endIndex) : base(startIndex, endIndex)
        {
        }

        private UniformLinearDomain(int startIndex, int endIndex, int startPixel, int endPixel) : base(startIndex, endIndex, startPixel, endPixel)
        {
        }

        public override int getFloorIndexFromCoordinate(int coord)
        {
            return (base._startIndex + base.getVisibleUnitsFlooredFromCoordinate(coord));
        }

        public override UniformDomain GetIndependentCopy()
        {
            return new UniformLinearDomain(base._startIndex, base._endIndex, base._startPixel, base._endPixel);
        }

        public override int getIndexFromCoordinate(int coord)
        {
            return (base._startIndex + base.getVisibleUnitsRoundedFromCoordinate(coord));
        }

        protected override int getRelativeUnits(int middleIndex)
        {
            return middleIndex;
        }

        protected override int getVisibleUnits(int middleIndex)
        {
            return (middleIndex - base._startIndex);
        }
    }
}


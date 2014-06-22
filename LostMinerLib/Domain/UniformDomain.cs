namespace TimeSearcher.Domain
{
    using System;
    using System.Drawing;

    public abstract class UniformDomain
    {
        protected int _endIndex;
        protected int _endPixel;
        protected int _startIndex;
        protected int _startPixel;
        private float _unitPerPixel;

        protected UniformDomain()
        {
        }

        public UniformDomain(int startIndex, int endIndex) : this(startIndex, endIndex, startIndex, endIndex)
        {
        }

        public UniformDomain(int startIndex, int endIndex, int startPixel, int endPixel)
        {
            this.init(startIndex, endIndex, startPixel, endPixel);
        }

        public bool containsPixel(int middlePixel)
        {
            return ((this._startPixel < middlePixel) && (middlePixel < this._endPixel));
        }

        public bool coversValidRect(Rectangle rect)
        {
            return (((this._startPixel <= rect.Left) && (rect.Right <= this._endPixel)) && (rect.Left < rect.Right));
        }

        public virtual int getCoordinateFromIndex(int middleIndex)
        {
            return this.getCoordinateFromVisibleUnits(this.getVisibleUnits(middleIndex));
        }

        protected int getCoordinateFromVisibleUnits(int units)
        {
            return (this._startPixel + ((int) (((float) units) / this._unitPerPixel)));
        }

        public abstract int getFloorIndexFromCoordinate(int coord);
        public abstract UniformDomain GetIndependentCopy();
        public abstract int getIndexFromCoordinate(int coord);
        protected abstract int getRelativeUnits(int middleIndex);
        protected abstract int getVisibleUnits(int middleIndex);
        public virtual int getVisibleUnitsFlooredFromCoordinate(int coord)
        {
            return (int) Math.Floor(this.getVisibleUnitsFromCoordinate(coord));
        }

        private double getVisibleUnitsFromCoordinate(int coord)
        {
            return (double) ((coord - this._startPixel) * this._unitPerPixel);
        }

        public virtual int getVisibleUnitsRoundedFromCoordinate(int coord)
        {
            return (int) Math.Round(this.getVisibleUnitsFromCoordinate(coord));
        }

        protected void init(int startIndex, int endIndex, int startPixel, int endPixel)
        {
            this._startIndex = startIndex;
            this._endIndex = endIndex;
            this._startPixel = startPixel;
            this._endPixel = endPixel;
            this.setUnitPerPixel();
        }

        public void SetStartEndIndex(int startIndex, int endIndex)
        {
            this._startIndex = startIndex;
            this._endIndex = endIndex;
            this.setUnitPerPixel();
        }

        public void SetStartEndPixel(int startPixel, int endPixel)
        {
            this._startPixel = startPixel;
            this._endPixel = endPixel;
            this.setUnitPerPixel();
        }

        private void setUnitPerPixel()
        {
            float num = this.getVisibleUnits(this._endIndex);
            this._unitPerPixel = num / ((float) (this._endPixel - this._startPixel));
        }
    }
}


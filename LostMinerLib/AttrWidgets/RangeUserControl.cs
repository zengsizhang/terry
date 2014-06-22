namespace TimeSearcher.AttrWidgets
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using TimeSearcher.Attribute;

    public class RangeUserControl : UserControl
    {
        private bool _checked;
        private readonly AttrValue _maxLimit;
        private AttrValue _maxRange;
        private readonly AttrValue _minLimit;
        private AttrValue _minRange;
        private readonly int _rangeIndex;
        private Container components;

        public event StatusChanged CheckedChanged;

        public event RangeChanged MaxRangeChanged;

        public event RangeChanged MinMaxRangeChanged;

        public event RangeChanged MinRangeChanged;

        public RangeUserControl()
        {
        }

        public RangeUserControl(int rangeIndex, AttrValue minLimit, AttrValue maxLimit)
        {
            this.InitializeComponent();
            this._rangeIndex = rangeIndex;
            this._minLimit = minLimit;
            this._maxLimit = maxLimit;
            this._minRange = this._minLimit;
            this._maxRange = this._maxLimit;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.Name = "RangeUserControl";
        }

        protected virtual void OnCheckedChanged(RangeUserControl sourceRUC)
        {
            if (this.CheckedChanged != null)
            {
                this.CheckedChanged(sourceRUC);
            }
        }

        protected virtual void OnMaxRangeChanged(RangeUserControl sourceRUC)
        {
            if (this.MaxRangeChanged != null)
            {
                this.MaxRangeChanged(sourceRUC);
            }
        }

        protected virtual void OnMinMaxRangeChanged(RangeUserControl sourceRUC)
        {
            if (this.MinMaxRangeChanged != null)
            {
                this.MinMaxRangeChanged(sourceRUC);
            }
        }

        protected virtual void OnMinRangeChanged(RangeUserControl sourceRUC)
        {
            if (this.MinRangeChanged != null)
            {
                this.MinRangeChanged(sourceRUC);
            }
        }

        public void ResetMinMaxRange()
        {
            this.SetMinMaxRange(this.MinRange, this.MaxRange);
        }

        protected void setChecked(bool isChecked)
        {
            this._checked = isChecked;
            this.OnCheckedChanged(this);
        }

        protected void setMaxRange(AttrValue maxRange)
        {
            this._maxRange = maxRange;
            this.OnMaxRangeChanged(this);
        }

        protected void setMinMaxRange(AttrValue minRange, AttrValue maxRange)
        {
            this._minRange = minRange;
            this._maxRange = maxRange;
            this.OnMinMaxRangeChanged(this);
        }

        public void SetMinMaxRange(AttrValue minRange, AttrValue maxRange)
        {
            this._minRange = minRange;
            this._maxRange = maxRange;
            this.setMinMaxRangeOnGui();
            this.OnMinMaxRangeChanged(this);
        }

        protected virtual void setMinMaxRangeOnGui()
        {
        }

        protected void setMinRange(AttrValue minRange)
        {
            this._minRange = minRange;
            this.OnMinRangeChanged(this);
        }

        public bool Checked
        {
            get
            {
                return this._checked;
            }
        }

        public AttrValue MaxLimit
        {
            get
            {
                return this._maxLimit;
            }
        }

        public AttrValue MaxRange
        {
            get
            {
                return this._maxRange;
            }
        }

        public AttrValue MinLimit
        {
            get
            {
                return this._minLimit;
            }
        }

        public AttrValue MinRange
        {
            get
            {
                return this._minRange;
            }
        }

        public int RangeIndex
        {
            get
            {
                return this._rangeIndex;
            }
        }

        public delegate void RangeChanged(RangeUserControl sourceRUC);

        public delegate void StatusChanged(RangeUserControl sourceRUC);
    }
}


namespace TimeSearcher
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher.Attribute;
    using TimeSearcher.AttrWidgets;
    using TimeSearcher.Filters;

    public class AttrFilterForm : Form
    {
        private readonly DataSet _dataSet;
        private RangeUserControl[] _rangeChoosers;
        private DisablingEntity[] _rangeEntities;
        private readonly TimeSearcherForm _tsForm;
        private Container components;

        public AttrFilterForm(TimeSearcherForm tsForm)
        {
            this.InitializeComponent();
            this._tsForm = tsForm;
            this._dataSet = tsForm.DataSet;
            this.myInit();
        }

        private void AttrFilterForm_CheckedChanged(RangeUserControl sourceRUC)
        {
            this.updateChecked(sourceRUC);
            this._tsForm.UpdateVisibleTabsDisplay();
        }

        private void AttrFilterForm_Closing(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < this._rangeEntities.Length; i++)
            {
                this._rangeEntities[i].Dematerialize();
            }
            this._tsForm.UpdateVisibleTabsDisplay();
        }

        private void AttrFilterForm_RangeChanged(RangeUserControl sourceRUC)
        {
            this.updateEntity(sourceRUC);
            this._tsForm.UpdateVisibleTabsDisplay();
        }

        private void disableRangeEvents(RangeUserControl ruc)
        {
            ruc.MinRangeChanged -= new RangeUserControl.RangeChanged(this.AttrFilterForm_RangeChanged);
            ruc.MaxRangeChanged -= new RangeUserControl.RangeChanged(this.AttrFilterForm_RangeChanged);
            ruc.MinMaxRangeChanged -= new RangeUserControl.RangeChanged(this.AttrFilterForm_RangeChanged);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void enableRangeEvents(RangeUserControl ruc)
        {
            ruc.MinRangeChanged += new RangeUserControl.RangeChanged(this.AttrFilterForm_RangeChanged);
            ruc.MaxRangeChanged += new RangeUserControl.RangeChanged(this.AttrFilterForm_RangeChanged);
            ruc.MinMaxRangeChanged += new RangeUserControl.RangeChanged(this.AttrFilterForm_RangeChanged);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AttrFilterForm));
            base.SuspendLayout();
            this.AutoScaleBaseSize = new Size(5, 13);
            this.AutoScroll = true;
            base.ClientSize = new Size(0x268, 0x1b5);
            base.Name = "AttrFilterForm";
            this.Text = "Attribute Filter Form";
            base.Closing += new CancelEventHandler(this.AttrFilterForm_Closing);
            base.ResumeLayout(false);
        }

        private void initRangeEntities()
        {
            this._rangeEntities = new DisablingEntity[this._rangeChoosers.Length];
            for (int i = 0; i < this._rangeEntities.Length; i++)
            {
                this._rangeEntities[i] = new EmptyDisablingEntity();
            }
        }

        private void myInit()
        {
            this._rangeChoosers = new RangeUserControl[this._dataSet.AttrOracle.NumAttr];
            for (int i = 0; i < this._rangeChoosers.Length; i++)
            {
                AttrType attrType = this._dataSet.AttrOracle.GetAttrType(i);
                string attrName = this._dataSet.AttrOracle.GetAttrName(i);
                AttrValue attrMin = this._dataSet.AttrOracle.GetAttrMin(i);
                AttrValue attrMax = this._dataSet.AttrOracle.GetAttrMax(i);
                if (attrType == AttrType.DATETIME)
                {
                    this._rangeChoosers[i] = new DateTimeRangeChooser(i, attrName, attrMin, attrMax);
                }
                else if ((attrType == AttrType.DOUBLE) || (attrType == AttrType.INTEGER))
                {
                    this._rangeChoosers[i] = new DoubleRangeChooser(i, attrName, attrMin, attrMax);
                }
                else if (attrType == AttrType.DAYOFWEEK)
                {
                    this._rangeChoosers[i] = new DayOfWeekRangeChooser(i, attrName, attrMin, attrMax);
                }
                else if (attrType == AttrType.STRING)
                {
                    this._rangeChoosers[i] = new StringRangeChooser(i, attrName, this._dataSet.AttrOracle.GetValuesOf(i));
                }
            }
            this.initRangeEntities();
            for (int j = 0; j < this._rangeChoosers.Length; j++)
            {
                if (this._rangeChoosers[j].Checked)
                {
                    this._rangeChoosers[j].ResetMinMaxRange();
                }
                this._rangeChoosers[j].Dock = DockStyle.Top;
                base.Controls.Add(this._rangeChoosers[j]);
            }
            for (int k = 0; k < this._rangeChoosers.Length; k++)
            {
                this.enableRangeEvents(this._rangeChoosers[k]);
                this._rangeChoosers[k].CheckedChanged += new RangeUserControl.StatusChanged(this.AttrFilterForm_CheckedChanged);
            }
        }

        private void updateChecked(RangeUserControl ruc)
        {
            if (ruc.Checked)
            {
                this.updateEntity(ruc);
                this.enableRangeEvents(ruc);
            }
            else
            {
                this._rangeEntities[ruc.RangeIndex].Dematerialize();
                this._rangeEntities[ruc.RangeIndex] = new EmptyDisablingEntity();
                this.disableRangeEvents(ruc);
            }
        }

        private void updateEntity(RangeUserControl ruc)
        {
            this._rangeEntities[ruc.RangeIndex].Dematerialize();
            DisablingEntity dEntity = new NotIncludingEntity(this._dataSet.AttrOracle.GetIncludedItemIdx(ruc.RangeIndex, ruc.MinRange, ruc.MaxRange, true), this._dataSet.DisablingManager);
            this._rangeEntities[ruc.RangeIndex] = dEntity;
            this._dataSet.DisablingManager.AddEntity(dEntity);
        }
    }
}


namespace TimeSearcher.Panels
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using TimeSearcher;

    public class ItemPanel : VarIndexedPanel
    {
        private readonly DataSet _dataSet;
        private Graph _graphIP;
        private DataAxisH _hAxis;
        private readonly int _itemIndex;
        private const int _overviewMargin = 0x19;
        private readonly TimeSearcherForm _tsForm;
        private DataAxisV _vAxis;

        public ItemPanel(TimeSearcherForm frm, int varIndex, int itemIdx) : base(varIndex)
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.Selectable, true);
            this._tsForm = frm;
            this._itemIndex = itemIdx;
            this._dataSet = this._tsForm.DataSet;
            this.initAxes();
            this.initGraphs(base._varIndex, this._itemIndex);
            base.ResizeRedraw = true;
            base.Resize += new EventHandler(this.OnResize);
            base.Paint += new PaintEventHandler(this.OnPaint);
        }

        private void ALEXrenderGraphIP(Graphics grfx)
        {
            grfx.SmoothingMode = SmoothingMode.AntiAlias;
            grfx.FillRectangle(Configuration.appBackgroundBrush, base.ClientRectangle);
            this._graphIP.RenderGraphWithMatches(grfx, 0, true);
            this._hAxis.Paint(grfx);
            this._vAxis.Paint(grfx);
        }

        private void buildGraph()
        {
            if ((this._tsForm.WindowState != FormWindowState.Minimized) && this._tsForm.IsDataSetLoaded)
            {
                int s = 0;
                int e = this._dataSet.NumTimePoints - 1;
                this._hAxis.setSize(base.ClientRectangle);
                this._vAxis.setSize(base.ClientRectangle);
                this._hAxis.setScale(s, e);
                this._vAxis.setScale();
                this._graphIP.BuildGraph(s, e);
            }
        }

        private void initAxes()
        {
            base._hMargin = 0x19;
            base._vMargin = 0x19;
            this._hAxis = new DataAxisH(base.ClientRectangle, this._dataSet, base._hMargin, base._vMargin);
            this._vAxis = new DataAxisV(base.ClientRectangle, this._dataSet, base._varIndex, base._hMargin, base._vMargin);
        }

        private void initGraphs(int varIdx, int itemIdx)
        {
            DataVariable var = this._dataSet.GetItem(itemIdx).GetVar(varIdx);
            this._graphIP = new Graph(var, this._hAxis.Domain, this._vAxis);
        }

        private void OnPaint(object obj, PaintEventArgs pea)
        {
            if (this._tsForm.IsDataSetLoaded)
            {
                Graphics grfx = pea.Graphics;
                this.ALEXrenderGraphIP(grfx);
                grfx.DrawString(this._dataSet.GetItem(this._itemIndex).Name, this._tsForm.Font, Brushes.Black, (float) 10f, (float) 5f);
            }
        }

        private void OnResize(object obj, EventArgs ea)
        {
            if (this._tsForm.WindowState != FormWindowState.Minimized)
            {
                this.buildGraph();
                base.Invalidate();
            }
        }
    }
}


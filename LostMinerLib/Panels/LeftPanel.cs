namespace TimeSearcher.Panels
{
    using System;
    using System.Windows.Forms;

    public class LeftPanel : Panel
    {
        public LeftPanel()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.Selectable, true);
        }
    }
}


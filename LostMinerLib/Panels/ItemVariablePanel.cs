namespace TimeSearcher.Panels
{
    using System;
    using System.Windows.Forms;
    using TimeSearcher;

    public class ItemVariablePanel : Panel
    {
        private ItemPanel _itemPanel;

        public ItemVariablePanel(TimeSearcherForm frm, ItemPanel itemPanel)
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            this._itemPanel = itemPanel;
            itemPanel.Parent = this;
            base.Resize += new EventHandler(this.OnResize);
        }

        private void OnResize(object obj, EventArgs ea)
        {
            if (this._itemPanel != null)
            {
                this.ResizeItemPanel();
            }
        }

        private void ResizeItemPanel()
        {
            this._itemPanel.SetBounds(base.ClientRectangle.Left, base.ClientRectangle.Top, base.ClientRectangle.Width, base.ClientRectangle.Height);
        }
    }
}


namespace TimeSearcher
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher.Widgets;

    public class SearchOptsForm : Form, AutoSearcher
    {
        private TabControl _searchOptsTabControl;
        private TabPage _searchOptsTabPage;
        private SearchOptsUserControl _searchOptsUC;
        private readonly TimeSearcherForm _tsForm;
        private Container components;

        public SearchOptsForm(TimeSearcherForm tsForm)
        {
            this.InitializeComponent();
            this._tsForm = tsForm;
            this.myInit();
        }

        public void autoSearch(bool isAutoSearchChecked)
        {
            if (isAutoSearchChecked)
            {
                SearchBox box = this._tsForm.DataSet.TimeBoxManager.getFirstSelectedSearchBox();
                if (box != null)
                {
                    box.PerformSearch();
                }
            }
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
            this._searchOptsTabControl = new TabControl();
            this._searchOptsTabPage = new TabPage();
            this._searchOptsTabControl.SuspendLayout();
            base.SuspendLayout();
            this._searchOptsTabControl.Controls.Add(this._searchOptsTabPage);
            this._searchOptsTabControl.Dock = DockStyle.Fill;
            this._searchOptsTabControl.Location = new Point(0, 0);
            this._searchOptsTabControl.Name = "_searchOptsTabControl";
            this._searchOptsTabControl.SelectedIndex = 0;
            this._searchOptsTabControl.Size = new Size(0x120, 0x135);
            this._searchOptsTabControl.TabIndex = 0;
            this._searchOptsTabPage.Location = new Point(4, 0x16);
            this._searchOptsTabPage.Name = "_searchOptsTabPage";
            this._searchOptsTabPage.Size = new Size(280, 0x11b);
            this._searchOptsTabPage.TabIndex = 0;
            this._searchOptsTabPage.Text = "Search Options";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x120, 0x135);
            base.Controls.Add(this._searchOptsTabControl);
            base.Name = "SearchOptsForm";
            this.Text = "Search Options Form";
            this._searchOptsTabControl.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void myInit()
        {
            this._searchOptsUC = new SearchOptsUserControl(SharedData.searchOptions, this);
            this._searchOptsUC.Dock = DockStyle.Fill;
            this._searchOptsTabPage.Controls.Add(this._searchOptsUC);
        }
    }
}


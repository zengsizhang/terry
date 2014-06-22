namespace TimeSearcher.Panels
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher;

    public class VariablePanel : Panel
    {
        protected ComboBox _cbList;
        private Panel _choicePanel;
        private const int _comboHeight = 0x17;
        private const int _comboWidth = 200;
        protected VarIndexedPanel _enclosedPanel;
        private Label _lblBeforeCombo;
        private Label _lblSelectedVariable;
        protected TimeSearcherForm _tsForm;
        protected int _vpIndex;

        public VariablePanel()
        {
        }

        public VariablePanel(TimeSearcherForm frm, int vpIdx, VarIndexedPanel enclosedPanel, string[] dynVarNames)
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            this._choicePanel = new Panel();
            this._choicePanel.Dock = DockStyle.Top;
            this._choicePanel.Parent = this;
            this._choicePanel.Height = 0x17;
            this._cbList = new ComboBox();
            this._cbList.DropDownStyle = ComboBoxStyle.DropDownList;
            this._cbList.Dock = DockStyle.Left;
            this._cbList.Height = 0x17;
            this._cbList.Width = 200;
            this._cbList.Parent = this._choicePanel;
            this._cbList.SelectedIndexChanged += new EventHandler(this.variableChanged);
            this._lblBeforeCombo = new Label();
            if (vpIdx == 0)
            {
                this._lblBeforeCombo.Text = "线损率";
            }
            if (vpIdx == 1)
            {
                this._lblBeforeCombo.Text = "线损电量";
            }
            if (vpIdx == 2)
            {
                this._lblBeforeCombo.Text = "用户用电量";
            }
            this._lblBeforeCombo.AutoSize = true;
            this._lblBeforeCombo.TextAlign = ContentAlignment.MiddleCenter;
            this._lblBeforeCombo.Dock = DockStyle.Left;
            this._lblSelectedVariable = new Label();
            this._lblSelectedVariable.AutoSize = true;
            this._lblSelectedVariable.TextAlign = ContentAlignment.BottomCenter;
            this._lblSelectedVariable.Dock = DockStyle.Left;
            this._lblSelectedVariable.Visible = false;
            this._cbList.Visible = false;
            this._choicePanel.Controls.Add(this._lblBeforeCombo);
            this._tsForm = frm;
            this._vpIndex = vpIdx;
            this._enclosedPanel = enclosedPanel;
            this._enclosedPanel.Parent = this;
            this.populateList(dynVarNames);
            base.Resize += new EventHandler(this.OnResize);
        }

        protected void disableVariableChangedHandler()
        {
            this._cbList.SelectedIndexChanged -= new EventHandler(this.variableChanged);
        }

        protected void enableVariableChangedHandler()
        {
            this._cbList.SelectedIndexChanged += new EventHandler(this.variableChanged);
        }

        private void OnResize(object obj, EventArgs ea)
        {
            this.setQueryPanelSize();
        }

        private void populateList(string[] varNames)
        {
            this.disableVariableChangedHandler();
            for (int i = 0; i < varNames.Length; i++)
            {
                this._cbList.Items.Add(varNames[i]);
            }
            this._cbList.SelectedIndex = this._enclosedPanel.VarIndex;
            this.enableVariableChangedHandler();
        }

        private void setEnclosedPanel(VarIndexedPanel qp)
        {
            if (this._enclosedPanel.Parent == this)
            {
                this._enclosedPanel.Parent = null;
            }
            this._enclosedPanel = qp;
            this._enclosedPanel.Parent = this;
            this.setQueryPanelSize();
            this._cbList.SelectedIndex = this._enclosedPanel.VarIndex;
        }

        private void setQueryPanelSize()
        {
            if (this._enclosedPanel != null)
            {
                int width = base.ClientRectangle.Width;
                this._enclosedPanel.SetBounds(base.ClientRectangle.Left, base.ClientRectangle.Top + 5, width, base.ClientRectangle.Height - 5);
            }
        }

        public void showSelectionBox(bool shouldShow)
        {
            if (shouldShow)
            {
                this._lblSelectedVariable.Visible = false;
                this._cbList.Visible = true;
                this._lblBeforeCombo.Visible = true;
                this._choicePanel.BackColor = Color.LightGray;
            }
            else
            {
                this._cbList.Visible = false;
                this._lblBeforeCombo.Visible = false;
                this._lblSelectedVariable.Text = (string) this._cbList.SelectedItem;
                this._lblSelectedVariable.Visible = true;
                this._choicePanel.BackColor = Configuration.appBackgroundColor;
            }
        }

        public void swapEnclosedPanelWith(VariablePanel vPanelB)
        {
            VarIndexedPanel qp = vPanelB._enclosedPanel;
            vPanelB.setEnclosedPanel(this._enclosedPanel);
            this.setEnclosedPanel(qp);
        }

        protected virtual void variableChanged(object sender, EventArgs ea)
        {
            this.disableVariableChangedHandler();
            this._tsForm.VarView.swapQueryPanels(this._cbList.SelectedIndex, this._vpIndex, this._enclosedPanel.VarIndex);
            this.enableVariableChangedHandler();
        }
    }
}


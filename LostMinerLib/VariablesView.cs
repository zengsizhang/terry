namespace TimeSearcher
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher.Panels;

    public class VariablesView
    {
        private readonly Panel _leftPanel;
        private QueryPanel[] _queryPanels;
        private readonly TimeSearcherForm _tsForm;
        private VariablePanel[] _variablePanels;
        private int[] _varIndexToVpIndex;

        public VariablesView(TimeSearcherForm tsForm, Panel leftPanel)
        {
            this._tsForm = tsForm;
            this._leftPanel = leftPanel;
        }

        public void createQueryPanels()
        {
            int num = 1;
            if (this._queryPanels != null)
            {
                foreach (QueryPanel panel in this._queryPanels)
                {
                    panel.Dispose();
                }
            }
            if (this._variablePanels != null)
            {
                foreach (VariablePanel panel2 in this._variablePanels)
                {
                    panel2.Dispose();
                }
            }
            this._queryPanels = new QueryPanel[this._tsForm.DataSet.NumDynVar];
            this._variablePanels = new VariablePanel[this._tsForm.DataSet.NumDynVar];
            if (this._tsForm.IsDataSetLoaded)
            {
                int num2 = this._leftPanel.ClientRectangle.Height / this._tsForm.NumDisplayedVars;
                for (int j = 0; j < this._tsForm.DataSet.NumDynVar; j++)
                {
                    QueryPanel enclosedPanel;
                    if (j == 0)
                    { 
                     enclosedPanel = new QueryPanel(this._tsForm, this._tsForm.DataSet, 1);
                    }
                    else if (j == 1)
                    {
                        enclosedPanel = new QueryPanel(this._tsForm, this._tsForm.DataSet, 0);
                    }
                    else
                    {
                        enclosedPanel = new QueryPanel(this._tsForm, this._tsForm.DataSet, j);
                    }
                  //  QueryPanel enclosedPanel = new QueryPanel(this._tsForm, this._tsForm.DataSet, j);
                    VariablePanel panel4 = new VariablePanel(this._tsForm, j, enclosedPanel, this._tsForm.DataSet.getDynVarNames(this._tsForm.LogMenuItemChecked));
                    panel4.SetBounds(this._leftPanel.Left, (this._leftPanel.Top + (j * num2)) + num, this._leftPanel.Width, num2 - num);
                    panel4.Size = new Size(this._leftPanel.Width, num2 - num);
                    this._queryPanels[j] = enclosedPanel;
                    this._variablePanels[j] = panel4;
                }
            }
            this._leftPanel.Controls.Clear();
            for (int i = 0; i < this._tsForm.NumDisplayedVars; i++)
            {
                this._leftPanel.Controls.Add(this._variablePanels[i]);
            }
        }

        public QueryPanel getQP(int varIdx)
        {
            return this._queryPanels[varIdx];
        }

        public void initVarIndexToVpIndex(int numDynamicVariables)
        {
            this._varIndexToVpIndex = new int[numDynamicVariables];
            for (int i = 0; i < numDynamicVariables; i++)
            {
                this._varIndexToVpIndex[i] = i;
            }
        }

        public void InvalidateVisibleQPs(int numDisplayedVariables)
        {
            for (int i = 0; i < numDisplayedVariables; i++)
            {
                this._queryPanels[this._varIndexToVpIndex[i]].Invalidate();
            }
        }

        public void layoutQueryPanels()
        {
            int num = 1;
            int numDisplayedVars = this._tsForm.NumDisplayedVars;
            if (this._tsForm.IsDataSetLoaded)
            {
                int num3 = this._leftPanel.ClientRectangle.Height / numDisplayedVars;
                int[] numArray = new int[numDisplayedVars];
                int[] numArray2 = new int[numDisplayedVars];
                int num5 = 0;
                for (int j = 0; j < numDisplayedVars; j++)
                {
                    if (this._tsForm.DataSet.dataType[j] == DataSet.DataType.BINARY)
                    {
                        num5++;
                    }
                }
                int num7 = numDisplayedVars - num5;
                int num8 = Math.Min(num3, 0x57);
                int num9 = this._leftPanel.ClientRectangle.Height - (num5 * num8);
                num9 /= num7;
                int num4 = this._leftPanel.Top + num;
                for (int k = 0; k < numArray.Length; k++)
                {
                    if (this._tsForm.DataSet.dataType[k] == DataSet.DataType.BINARY)
                    {
                        numArray[k] = num8;
                    }
                    else
                    {
                        numArray[k] = num9;
                    }
                    numArray2[k] = num4;
                    num4 += numArray[k];
                }
                int width = this._leftPanel.Width;
                for (int m = 0; m < this._tsForm.NumDisplayedVars; m++)
                {
                    this._variablePanels[m].SetBounds(this._leftPanel.Left, numArray2[m], this._leftPanel.Width, numArray[m]);
                }
            }
            this._leftPanel.Controls.Clear();
            for (int i = 0; i < this._tsForm.NumDisplayedVars; i++)
            {
                this._leftPanel.Controls.Add(this._variablePanels[i]);
            }
        }

        public void swapQueryPanels(int varIndex2, int vpIndex1, int varIndex1)
        {
            int index = this._varIndexToVpIndex[varIndex2];
            this._variablePanels[vpIndex1].swapEnclosedPanelWith(this._variablePanels[index]);
            this._varIndexToVpIndex[varIndex1] = index;
            this._varIndexToVpIndex[varIndex2] = vpIndex1;
        }

        public void UpdateIncItem(DataItem incItem)
        {
            foreach (QueryPanel panel in this._queryPanels)
            {
                panel.UpdateIncItem(incItem);
            }
        }

        public QueryPanel[] QPs
        {
            get
            {
                return this._queryPanels;
            }
        }

        public QueryPanel[] QueryPanels
        {
            get
            {
                return this._queryPanels;
            }
        }

        public VariablePanel[] VarPanels
        {
            get
            {
                return this._variablePanels;
            }
        }
    }
}


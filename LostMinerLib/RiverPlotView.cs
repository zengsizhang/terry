namespace TimeSearcher
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher.Panels;

    public class RiverPlotView
    {
        private int[] _enabledItemIdx;
        private readonly Panel _leftPanel;
        private RiverPanel[] _riverPanels;
        private RiverVariablePanel[] _riverVariablePanels;
        private DataSet _statDataSet;
        private readonly TimeSearcherForm _tsForm;
        private int[] _varIndexToVpIndex;

        public RiverPlotView(TimeSearcherForm tsForm, LeftPanel leftRiverPanel)
        {
            this._tsForm = tsForm;
            this._leftPanel = leftRiverPanel;
        }

        public void createRiverPanels()
        {
            int num = 1;
            this._statDataSet = this._tsForm.DataSet.GetStatDataSet();
            this._enabledItemIdx = this._tsForm.DataSet.GetEnabledItemIdx();
            this.initVarIndexToVpIndex(this._statDataSet.NumDynVar);
            if (this._riverPanels != null)
            {
                foreach (RiverPanel panel in this._riverPanels)
                {
                    panel.Dispose();
                }
            }
            if (this._riverVariablePanels != null)
            {
                RiverVariablePanel[] panelArray2 = this._riverVariablePanels;
                for (int j = 0; j < panelArray2.Length; j++)
                {
                    panelArray2[j].Dispose();
                }
            }
            this._riverPanels = new RiverPanel[this._statDataSet.NumDynVar];
            this._riverVariablePanels = new RiverVariablePanel[this._statDataSet.NumDynVar];
            if (this._tsForm.IsDataSetLoaded)
            {
                int num2 = this._leftPanel.ClientRectangle.Height / this._tsForm.NumDisplayedVars;
                for (int k = 0; k < this._statDataSet.NumDynVar; k++)
                {
                    RiverPanel enclosedPanel = new RiverPanel(this._tsForm, this._statDataSet, k);
                    RiverVariablePanel panel4 = new RiverVariablePanel(this._tsForm, k, enclosedPanel, this._statDataSet.getDynVarNames(this._tsForm.LogMenuItemChecked));
                    panel4.SetBounds(this._leftPanel.Left, (this._leftPanel.Top + (k * num2)) + num, this._leftPanel.Width, num2 - num);
                    panel4.Size = new Size(this._leftPanel.Width, num2 - num);
                    this._riverPanels[k] = enclosedPanel;
                    this._riverVariablePanels[k] = panel4;
                }
            }
            this._leftPanel.Controls.Clear();
            for (int i = 0; i < this._tsForm.NumDisplayedVars; i++)
            {
                this._leftPanel.Controls.Add(this._riverVariablePanels[i]);
            }
        }

        public RiverPanel GetRP(int varIdx)
        {
            return this._riverPanels[varIdx];
        }

        public void initVarIndexToVpIndex(int numDynamicVariables)
        {
            this._varIndexToVpIndex = new int[numDynamicVariables];
            for (int i = 0; i < numDynamicVariables; i++)
            {
                this._varIndexToVpIndex[i] = i;
            }
        }

        public void Invalidate()
        {
            foreach (RiverPanel panel in this._riverPanels)
            {
                panel.Invalidate();
            }
        }

        public void layoutRiverPanels()
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
                    if (this._statDataSet.dataType[j] == DataSet.DataType.BINARY)
                    {
                        num5++;
                    }
                }
                int num7 = numDisplayedVars - num5;
                int num8 = Math.Min(num3, 0x57);
                int num9 = this._leftPanel.ClientRectangle.Height - (num5 * num8);
                num9 /= num7;
                int num4 = num;
                for (int k = 0; k < numArray.Length; k++)
                {
                    if (this._statDataSet.dataType[k] == DataSet.DataType.BINARY)
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
                for (int m = 0; m < numDisplayedVars; m++)
                {
                    this._riverVariablePanels[m].SetBounds(0, numArray2[m], this._leftPanel.Width, numArray[m]);
                }
            }
            this._leftPanel.Controls.Clear();
            for (int i = 0; i < numDisplayedVars; i++)
            {
                this._leftPanel.Controls.Add(this._riverVariablePanels[i]);
            }
        }

        public void PrepareToDisplay(DataSet dataSet)
        {
            if (!Utils.EqualArrays(dataSet.GetEnabledItemIdx(), this._enabledItemIdx))
            {
                this.updateStatDataSet();
                this._enabledItemIdx = dataSet.GetEnabledItemIdx();
            }
        }

        public void SwapRiverPanels(int varIndex2, int vpIndex1, int varIndex1)
        {
            int index = this._varIndexToVpIndex[varIndex2];
            this._riverVariablePanels[vpIndex1].swapEnclosedPanelWith(this._riverVariablePanels[index]);
            this._varIndexToVpIndex[varIndex1] = index;
            this._varIndexToVpIndex[varIndex2] = vpIndex1;
        }

        public void UpdateIncItem(DataItem incItem)
        {
            foreach (RiverPanel panel in this._riverPanels)
            {
                panel.UpdateIncItem(incItem);
            }
        }

        private void updateStatDataSet()
        {
            this._statDataSet = this._tsForm.DataSet.GetStatDataSet();
            foreach (RiverPanel panel in this._riverPanels)
            {
                panel.UpdateStatDataSet(this._statDataSet);
            }
        }

        public RiverPanel[] RiverPanels
        {
            get
            {
                return this._riverPanels;
            }
        }

        public RiverVariablePanel[] RiverVariablePanels
        {
            get
            {
                return this._riverVariablePanels;
            }
        }

        public RiverPanel[] RPs
        {
            get
            {
                return this._riverPanels;
            }
        }
    }
}


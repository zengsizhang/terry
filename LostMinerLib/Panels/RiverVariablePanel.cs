namespace TimeSearcher.Panels
{
    using System;
    using TimeSearcher;

    public class RiverVariablePanel : VariablePanel
    {
        public RiverVariablePanel(TimeSearcherForm tsForm, int vpIdx, VarIndexedPanel enclosedPanel, string[] dynVarNames) : base(tsForm, vpIdx, enclosedPanel, dynVarNames)
        {
        }

        protected override void variableChanged(object sender, EventArgs ea)
        {
            base.disableVariableChangedHandler();
            base._tsForm.RiverView.SwapRiverPanels(base._cbList.SelectedIndex, base._vpIndex, base._enclosedPanel.VarIndex);
            base.enableVariableChangedHandler();
        }
    }
}


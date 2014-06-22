namespace TimeSearcher.Panels
{
    using System;
    using System.Windows.Forms;

    public class VarIndexedPanel : Panel
    {
        protected int _hMargin;
        protected readonly int _varIndex;
        protected int _vMargin;

        public VarIndexedPanel()
        {
        }

        public VarIndexedPanel(int varIndex)
        {
            this._varIndex = varIndex;
        }

        public int VarIndex
        {
            get
            {
                return this._varIndex;
            }
        }
    }
}


namespace TimeSearcher.AttrStat
{
    using System;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Attribute;
    using Util.Stat;

    public class CalcAttrListManager
    {
        private AttrStatCalculator _attrStatCalc;
        private readonly ListView _calcAttrList;
        private readonly DataSet _dataSet;
        private readonly StatType[] _includedStats = new StatType[] { StatType.MIN, StatType.Q1, StatType.STDEV, StatType.MEAN, StatType.MED, StatType.Q3, StatType.MAX };

        public CalcAttrListManager(ListView calcAttrList, DataSet dataSet)
        {
            this._calcAttrList = calcAttrList;
            this._dataSet = dataSet;
            this.Reset();
        }

        public void Refill()
        {
            int[] enabledItemIdx = this._dataSet.GetEnabledItemIdx();
            AttrValue[][] valueArray2 = new AttrValue[enabledItemIdx.Length][];
            for (int i = 0; i < enabledItemIdx.Length; i++)
            {
                AttrValue[] attributes = this._dataSet.GetItem(enabledItemIdx[i]).Attributes;
                valueArray2[i] = this._attrStatCalc.GetStatAtrArray(attributes);
            }
            this._attrStatCalc.InitCalcAttrListHeader(this._calcAttrList, this._includedStats);
            this._calcAttrList.Items.Clear();
            AttrValue[] statAtrs = new AttrValue[enabledItemIdx.Length];
            for (int j = 0; j < this._attrStatCalc.Arity; j++)
            {
                for (int k = 0; k < statAtrs.Length; k++)
                {
                    statAtrs[k] = valueArray2[k][j];
                }
                ListViewItem item = this._attrStatCalc.GetStatRow(j, statAtrs, this._includedStats);
                this._calcAttrList.Items.Add(item);
            }
        }

        public void Reset()
        {
            this._attrStatCalc = new AttrStatCalculator(this._dataSet.AttrOracle);
        }
    }
}


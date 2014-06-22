namespace TimeSearcher.AttrStat
{
    using System;
    using System.Collections;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Attribute;
    using Util.Stat;

    public class AttrStatCalculator
    {
        private readonly AttributeOracle _attrOracle;
        private readonly int[] _statAtrIndToAtrInd;

        public AttrStatCalculator(AttributeOracle attrOracle)
        {
            this._attrOracle = attrOracle;
            this._statAtrIndToAtrInd = this.getStatAtrIndToAtrInd();
        }

        public AttrValue[] GetStatAtrArray(AttrValue[] atrArray)
        {
            AttrValue[] valueArray = new AttrValue[this.Arity];
            for (int i = 0; i < valueArray.Length; i++)
            {
                valueArray[i] = atrArray[this._statAtrIndToAtrInd[i]];
            }
            return valueArray;
        }

        private int[] getStatAtrIndToAtrInd()
        {
            ArrayList list = new ArrayList();
            for (int i = 1; i < this._attrOracle.NumAttr; i++)
            {
                AttrType attrType = this._attrOracle.GetAttrType(i);
                if ((attrType == AttrType.INTEGER) || (attrType == AttrType.DOUBLE))
                {
                    list.Add(i);
                }
            }
            return (int[]) list.ToArray(typeof(int));
        }

        public ListViewItem GetStatRow(int statAtrIdx, AttrValue[] statAtrs, StatType[] statTypes)
        {
            int length = statAtrs.Length;
            ArrayList list = new ArrayList();
            list.Add(this._attrOracle.GetAttrName(this._statAtrIndToAtrInd[statAtrIdx]));
            this._attrOracle.GetAttrType(this._statAtrIndToAtrInd[statAtrIdx]);
            EfficientStatComputer computer = new EfficientStatComputer(statAtrs, statTypes);
            for (int i = 0; i < statTypes.Length; i++)
            {
                list.Add(computer.GetStat(statTypes[i]).StrValue);
            }
            return new ListViewItem((string[]) list.ToArray(typeof(string)));
        }

        public void InitCalcAttrListHeader(ListView calcAttrList, StatType[] attrStatTypes)
        {
            calcAttrList.Columns.Clear();
            calcAttrList.Columns.Add("Attributes", 60, AttrType.STRING.HorAlignment);
            for (int i = 0; i < attrStatTypes.Length; i++)
            {
                calcAttrList.Columns.Add(attrStatTypes[i].Name, 60, AttrType.DOUBLE.HorAlignment);
            }
        }

        public int Arity
        {
            get
            {
                return this._statAtrIndToAtrInd.Length;
            }
        }
    }
}


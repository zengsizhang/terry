namespace TimeSearcher
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher.Attribute;

    public class AttributeOracle
    {
        private AttrValue[] _attrMaxs;
        private AttrValue[] _attrMins;
        private readonly string _attrName0;
        private readonly string[] _attrNames;
        private readonly AttrType[] _attrTypes;
        private readonly DataSet _dataSet;

        public AttributeOracle(DataSet dataSet, string[] attrNames, AttrType[] attrTypes)
        {
            this._dataSet = dataSet;
            this._attrNames = attrNames;
            this._attrName0 = this._attrNames[0];
            this._attrTypes = attrTypes;
        }

        public AttributeOracle(DataSet dataSet, string attrName, AttrType attrType) : this(dataSet, new string[] { attrName }, new AttrType[] { attrType })
        {
        }

        private void calcMinMaxOfAttr()
        {
            this._attrMins = new AttrValue[this.NumAttr];
            this._attrMaxs = new AttrValue[this.NumAttr];
            for (int i = 0; i < this.NumAttr; i++)
            {
                this._attrMins[i] = this._dataSet.GetItem(0).GetAttr(i);
                this._attrMaxs[i] = this._attrMins[i];
                for (int j = 0; j < this._dataSet.NumItems; j++)
                {
                    AttrValue attr = this._dataSet.GetItem(j).GetAttr(i);
                    if (!(attr is AttrMissing))
                    {
                        if (attr.Compare(this._attrMins[i]) < 0)
                        {
                            this._attrMins[i] = attr;
                        }
                        if (this._attrMaxs[i].Compare(attr) < 0)
                        {
                            this._attrMaxs[i] = attr;
                        }
                    }
                }
            }
        }

        public int[] CalcPreferredAttrWidths(ListView lv)
        {
            Graphics graphics = lv.CreateGraphics();
            int[] numArray = new int[this.NumAttr];
            for (int i = 0; i < this.NumAttr; i++)
            {
                numArray[i] = Utils.GetCeilingIntWidthForLvColHdr(graphics.MeasureString(this._attrNames[i], lv.Font));
            }
            for (int j = 0; j < this._dataSet.NumItems; j++)
            {
                AttrValue[] attributes = this._dataSet.GetItem(j).Attributes;
                for (int k = 0; k < this.NumAttr; k++)
                {
                    int ceilingIntWidthForLvCol = Utils.GetCeilingIntWidthForLvCol(graphics.MeasureString(attributes[k].StrValue, lv.Font));
                    if (ceilingIntWidthForLvCol > numArray[k])
                    {
                        numArray[k] = ceilingIntWidthForLvCol;
                    }
                }
            }
            return numArray;
        }

        public AttrValue GetAttrMax(int attrIndex)
        {
            if (this._attrMaxs == null)
            {
                this.calcMinMaxOfAttr();
            }
            return this._attrMaxs[attrIndex];
        }

        public AttrValue GetAttrMin(int attrIndex)
        {
            if (this._attrMins == null)
            {
                this.calcMinMaxOfAttr();
            }
            return this._attrMins[attrIndex];
        }

        public string GetAttrName(int attrIdx)
        {
            return this._attrNames[attrIdx];
        }

        public AttrType GetAttrType(int attrIdx)
        {
            return this._attrTypes[attrIdx];
        }

        public int[] GetIncludedItemIdx(int attrIdx, AttrValue minAttrVal, AttrValue maxAttrVal, bool shdIncludeMissing)
        {
            ArrayList list = new ArrayList();
            AttrType type1 = this._attrTypes[attrIdx];
            for (int i = 0; i < this._dataSet.NumItems; i++)
            {
                AttrValue attr = this._dataSet.GetItem(i).GetAttr(attrIdx);
                if (attr is AttrMissing)
                {
                    if (shdIncludeMissing)
                    {
                        list.Add(i);
                    }
                }
                else if ((minAttrVal.Compare(attr) <= 0) && (attr.Compare(maxAttrVal) <= 0))
                {
                    list.Add(i);
                }
            }
            return (int[]) list.ToArray(typeof(int));
        }

        public AttrValue[] GetValuesOf(int attrIndex)
        {
            AttrValue[] valueArray = new AttrValue[this._dataSet.NumItems];
            for (int i = 0; i < this._dataSet.NumItems; i++)
            {
                valueArray[i] = this._dataSet.GetItem(i).GetAttr(attrIndex);
            }
            return valueArray;
        }

        public string AttrName0WithCount
        {
            get
            {
                return string.Concat(new object[] { this._attrName0, " (", this._dataSet.calcEnabledNumItems(), "/", this._dataSet.NumItems, ")" });
            }
        }

        public string[] AttrNames
        {
            get
            {
                return this._attrNames;
            }
        }

        public bool HaveAttr
        {
            get
            {
                return (this.NumAttr > 1);
            }
        }

        public int NumAttr
        {
            get
            {
                return this._attrTypes.Length;
            }
        }
    }
}


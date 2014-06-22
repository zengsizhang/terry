namespace TimeSearcher
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using TimeSearcher.AttrStat;
    using Util;

    public class ItemsListManager
    {
        private readonly CalcAttrListManager _calm;
        private readonly DataSet _dataSet;
        private readonly ListView _itemsList;
        private int[] _itemToLvIndex;
        private ListViewColumnSorter _lvColumnSorter;
        private int[] _lvPrefWidth;
        private int[] _lvToItemIndex;
        private ArrayList _prevSelectedItemIdx;
        private readonly TimeSearcherForm _tsForm;

        public ItemsListManager(ListView itemsList, DataSet dataSet, CalcAttrListManager calm, TimeSearcherForm tsForm)
        {
            this._itemsList = itemsList;
            this._dataSet = dataSet;
            this._calm = calm;
            this._tsForm = tsForm;
            this._lvColumnSorter = new ListViewColumnSorter(dataSet);
            this._itemsList.ListViewItemSorter = this._lvColumnSorter;
            this.Reset(false);
            this.installEventHandlers();
        }

        private void _itemsList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == this._lvColumnSorter.SortColumnIndex)
            {
                if (this._lvColumnSorter.Order == SortOrder.Ascending)
                {
                    this._lvColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    this._lvColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                this._lvColumnSorter.SortColumnIndex = e.Column;
                this._lvColumnSorter.Order = SortOrder.Ascending;
            }
            this._itemsList.Sort();
            this.updateLvAndItemIndex();
        }

        private void _itemsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._tsForm.bUpdating)
            {
                int[] numArray;
                int[] numArray2;
                this.computeToSelectAndUnselect(out numArray, out numArray2);
                for (int i = 0; i < numArray2.Length; i++)
                {
                    this._dataSet.GetItem(numArray2[i]).unSelectEnabled();
                    this.colorAsUnselected(numArray2[i]);
                }
                for (int j = 0; j < numArray.Length; j++)
                {
                    this._dataSet.GetItem(numArray[j]).selectEnabled();
                    this.colorAsSelected(numArray[j]);
                }
                this.updatePrevSelectedItemIdx();
                if (numArray.Length > 0)
                {
                    this._itemsList.EnsureVisible(this._itemToLvIndex[this.ItemIdxOfSelectedAtBottommost]);
                }
                if (this._tsForm.IsDetailsListSynchronized)
                {
                    this._tsForm.RefillDetailsList("", false);
                }
                this._tsForm.invalidateVisiblePanels();
            }
        }

        private void colorAsSelected(int itemIdx)
        {
            int num = this._itemToLvIndex[itemIdx];
            this._itemsList.Items[num].BackColor = Color.Blue;
            this._itemsList.Items[num].ForeColor = Color.White;
        }

        private void colorAsUnselected(int itemIdx)
        {
            int num = this._itemToLvIndex[itemIdx];
            this._itemsList.Items[num].BackColor = Color.White;
            this._itemsList.Items[num].ForeColor = Color.Black;
        }

        private void computeToSelectAndUnselect(out int[] toSelect, out int[] toUnselect)
        {
            int num;
            ArrayList list = new ArrayList();
            for (int i = 0; i < this._itemsList.SelectedIndices.Count; i++)
            {
                int index = this._itemsList.SelectedIndices[i];
                num = this._lvToItemIndex[index];
                if (!this._prevSelectedItemIdx.Contains(num))
                {
                    list.Add(num);
                }
                else
                {
                    this._prevSelectedItemIdx.Remove(num);
                }
            }
            ArrayList list2 = new ArrayList();
            for (int j = 0; j < this._prevSelectedItemIdx.Count; j++)
            {
                num = (int) this._prevSelectedItemIdx[j];
                if (this._dataSet.GetItem(num).IsEnabled())
                {
                    list2.Add(num);
                }
            }
            toSelect = (int[]) list.ToArray(typeof(int));
            toUnselect = (int[]) list2.ToArray(typeof(int));
        }

        public void fillItemsList()
        {
            this._itemsList.Items.Clear();
            this.initItemsListHeader(this._itemsList);
            int[] selectedAndEnabledItemIdx = this._dataSet.GetSelectedAndEnabledItemIdx();
            this.uninstallEventHandlers();
            int[] enabledItemIdx = this._dataSet.GetEnabledItemIdx();
            ListViewItem[] items = new ListViewItem[enabledItemIdx.Length];
            for (int i = 0; i < enabledItemIdx.Length; i++)
            {
                items[i] = this._dataSet.GetItem(enabledItemIdx[i]).ListViewItem;
            }
            this._itemsList.Items.AddRange(items);
            this.installEventHandlers();
            this._itemsList.Sort();
            this.updateLvAndItemIndex();
            for (int j = 0; j < selectedAndEnabledItemIdx.Length; j++)
            {
                int num2 = selectedAndEnabledItemIdx[j];
                if (this._dataSet.GetItem(num2).IsEnabled())
                {
                    this._itemsList.Items[this._itemToLvIndex[num2]].Selected = true;
                    this.colorAsSelected(num2);
                }
            }
            if (this._dataSet.AttrOracle.HaveAttr)
            {
                this._calm.Refill();
            }
        }

        private int[] getEnabledItemIdxFromItemsList()
        {
            int[] array = new int[this._itemsList.Items.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (int) this._itemsList.Items[i].Tag;
            }
            Array.Sort<int>(array);
            return array;
        }

        private ArrayList getSelectedItemIdx()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < this._itemsList.SelectedIndices.Count; i++)
            {
                int index = this._itemsList.SelectedIndices[i];
                list.Add(this._lvToItemIndex[index]);
            }
            return list;
        }

        private void initItemsListHeader(ListView itemList)
        {
            string[] attrNames = this._dataSet.AttrOracle.AttrNames;
            attrNames[0] = this._dataSet.AttrOracle.AttrName0WithCount;
            this.updateLvPrefWidth();
            itemList.Columns.Clear();
            for (int i = 0; i < attrNames.Length; i++)
            {
                itemList.Columns.Add(attrNames[i], this._lvPrefWidth[i], this._dataSet.AttrOracle.GetAttrType(i).HorAlignment);
            }
        }

        private void installEventHandlers()
        {
            this._itemsList.SelectedIndexChanged += new EventHandler(this._itemsList_SelectedIndexChanged);
            this._itemsList.ColumnClick += new ColumnClickEventHandler(this._itemsList_ColumnClick);
        }

        public bool ItemsUpToDate()
        {
            return Utils.EqualArrays(this._dataSet.GetEnabledItemIdx(), this.getEnabledItemIdxFromItemsList());
        }

        public void LoadAtrFromFile(string path)
        {
            this._lvColumnSorter.ColumnSortComparers = this._dataSet.ReadAttributes(path);
            this._calm.Reset();
            this._tsForm.RefillItemsList();
        }

        public void Reset(bool shdKeepSettings)
        {
            this._prevSelectedItemIdx = new ArrayList();
            if (shdKeepSettings)
            {
                if (this._dataSet.LastAttrPath != null)
                {
                    this.LoadAtrFromFile(this._dataSet.LastAttrPath);
                }
            }
            else
            {
                this._lvColumnSorter.SortColumnIndex = 0;
            }
        }

        public void SelectDataItem(int itemIdx)
        {
            int num = this._itemToLvIndex[itemIdx];
            bool selected = this._itemsList.Items[num].Selected;
            if (this._tsForm.IsControlKeyDown)
            {
                this._itemsList.Items[num].Selected = !selected;
            }
            else if (!selected || (this._itemsList.SelectedItems.Count != 1))
            {
                foreach (ListViewItem item in this._itemsList.SelectedItems)
                {
                    item.Selected = false;
                }
                this._itemsList.Items[num].Selected = true;
            }
        }

        public void SelectTheseDataItemsOnly(int[] toSelect)
        {
            foreach (int num in this._dataSet.GetSelectedAndEnabledItemIdx())
            {
                this._itemsList.Items[this._itemToLvIndex[num]].Selected = false;
            }
            for (int i = 0; i < toSelect.Length; i++)
            {

                int num;
                num = toSelect[i];
                this._itemsList.Items[this._itemToLvIndex[num]].Selected = true;
            }
        }

        private void uninstallEventHandlers()
        {
            this._itemsList.SelectedIndexChanged -= new EventHandler(this._itemsList_SelectedIndexChanged);
            this._itemsList.ColumnClick -= new ColumnClickEventHandler(this._itemsList_ColumnClick);
        }

        private void updateLvAndItemIndex()
        {
            this._lvToItemIndex = new int[this._itemsList.Items.Count];
            for (int i = 0; i < this._lvToItemIndex.Length; i++)
            {
                this._lvToItemIndex[i] = (int) this._itemsList.Items[i].Tag;
            }
            this._itemToLvIndex = new int[this._dataSet.NumItems];
            for (int j = 0; j < this._lvToItemIndex.Length; j++)
            {
                this._itemToLvIndex[this._lvToItemIndex[j]] = j;
            }
        }

        private void updateLvPrefWidth()
        {
            this._lvPrefWidth = this._dataSet.AttrOracle.CalcPreferredAttrWidths(this._itemsList);
        }

        private void updatePrevSelectedItemIdx()
        {
            this._prevSelectedItemIdx = new ArrayList();
            for (int i = 0; i < this._itemsList.SelectedIndices.Count; i++)
            {
                int index = this._itemsList.SelectedIndices[i];
                this._prevSelectedItemIdx.Add(this._lvToItemIndex[index]);
            }
        }

        public int ItemIdxOfSelected
        {
            get
            {
                return this.ItemIdxOfSelectedAtTopmost;
            }
        }

        private int ItemIdxOfSelectedAtBottommost
        {
            get
            {
                if (this._itemsList.SelectedIndices.Count == 0)
                {
                    return -1;
                }
                int index = this._itemsList.SelectedIndices[this._itemsList.SelectedIndices.Count - 1];
                return this._lvToItemIndex[index];
            }
        }

        private int ItemIdxOfSelectedAtTopmost
        {
            get
            {
                if (this._itemsList.SelectedIndices.Count == 0)
                {
                    return -1;
                }
                int index = this._itemsList.SelectedIndices[0];
                return this._lvToItemIndex[index];
            }
        }
    }
}


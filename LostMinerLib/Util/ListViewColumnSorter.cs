namespace Util
{
    using System;
    using System.Collections;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Attribute;

    public class ListViewColumnSorter : IComparer
    {
        private IComparer _ciComparer;
        private int _columnIndexToSort;
        private IComparer[] _columnSortComparers;
        private readonly DataSet _dataSet;
        private SortOrder _orderOfSort;

        public ListViewColumnSorter(DataSet dataSet)
        {
            this._dataSet = dataSet;
            this._columnSortComparers = new IComparer[] { new CaseInsensitiveComparer() };
            this._columnIndexToSort = 0;
            this._orderOfSort = SortOrder.None;
            this._ciComparer = this._columnSortComparers[0];
        }

        public int Compare(object x, object y)
        {
            ListViewItem item = (ListViewItem) x;
            ListViewItem item2 = (ListViewItem) y;
            DataItem item3 = this._dataSet.GetItem((int) item.Tag);
            DataItem item4 = this._dataSet.GetItem((int) item2.Tag);
            AttrValue attr = item3.GetAttr(this._columnIndexToSort);
            AttrValue value3 = item4.GetAttr(this._columnIndexToSort);
            int num = attr.Compare(value3);
            if (this._orderOfSort == SortOrder.Ascending)
            {
                return num;
            }
            if (this._orderOfSort == SortOrder.Descending)
            {
                return -num;
            }
            return 0;
        }

        public IComparer[] ColumnSortComparers
        {
            set
            {
                this._columnSortComparers = value;
            }
        }

        public SortOrder Order
        {
            get
            {
                return this._orderOfSort;
            }
            set
            {
                this._orderOfSort = value;
            }
        }

        public int SortColumnIndex
        {
            get
            {
                return this._columnIndexToSort;
            }
            set
            {
                this._columnIndexToSort = value;
                if (this._columnIndexToSort < this._columnSortComparers.Length)
                {
                    this._ciComparer = this._columnSortComparers[this._columnIndexToSort];
                }
            }
        }
    }
}


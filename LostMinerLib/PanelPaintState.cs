namespace TimeSearcher
{
    using System;
    using System.Collections;
    using TimeSearcher.Search;

    public class PanelPaintState
    {
        private readonly GraphCollection _graphs;
        private readonly int[] _itemStates;
        private readonly SearchResult[] _sResults;
        private const int BLANK = 0;
        private const int DISABLED = 2;
        private const int ENABLED_ONLY = 1;
        private const int HIGHLIGHTED = 4;
        public const int NONE_TO_HIGHLIGHT = -1;
        private const int SELECTED_AND_ENABLED = 3;

        public PanelPaintState(DataSet dataSet, GraphCollection graphs)
        {
            this._graphs = graphs;
            this._itemStates = new int[dataSet.NumItems];
            this._sResults = new SearchResult[dataSet.NumItems];
            for (int i = 0; i < dataSet.NumItems; i++)
            {
                DataItem item = dataSet.GetItem(i);
                if (item.IsEnabledOnly())
                {
                    this._itemStates[i] = 1;
                }
                else if (item.IsSelectedAndEnabled())
                {
                    this._itemStates[i] = 3;
                }
                else if (item.IsHighlighted())
                {
                    this._itemStates[i] = 4;
                }
                else
                {
                    this._itemStates[i] = 2;
                }
                this._sResults[i] = graphs.getGraph(i).SearchResult;
            }
        }

        public PanelPaintState(GraphCollection graphs, int numItems)
        {
            this._graphs = graphs;
            this._itemStates = new int[numItems];
            for (int i = 0; i < numItems; i++)
            {
                this._itemStates[i] = 0;
            }
        }

        public PanelPaintStateDiff GetDiff(PanelPaintState pps2)
        {
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            ArrayList list3 = new ArrayList();
            int itemToHighlight = -1;
            ArrayList list4 = new ArrayList();
            for (int i = 0; i < this._itemStates.Length; i++)
            {
                if (this._itemStates[i] != pps2._itemStates[i])
                {
                    switch (pps2._itemStates[i])
                    {
                        case 1:
                            list.Add(i);
                            break;

                        case 2:
                            list2.Add(i);
                            break;

                        case 3:
                            list3.Add(i);
                            break;

                        case 4:
                            itemToHighlight = i;
                            break;
                    }
                }
                else if (!this._sResults[i].IsSameAs(pps2._sResults[i]))
                {
                    list4.Add(i);
                }
            }
            int[] itemsToUndisable = (int[]) list.ToArray(typeof(int));
            int[] itemsToDisable = (int[]) list2.ToArray(typeof(int));
            return new PanelPaintStateDiff(itemsToUndisable, itemsToDisable, (int[]) list3.ToArray(typeof(int)), itemToHighlight, (int[]) list4.ToArray(typeof(int)));
        }
    }
}


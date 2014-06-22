namespace TimeSearcher
{
   
    using System;
    using System.Collections;
    using System.Drawing;
    using TimeSearcher.Domain;
    using TimeSearcher.Search;

    public class GraphCollection
    {
        private readonly DataSet _dataSet;
        private Graph[] _graphs;
        private int _varIdx;

        public GraphCollection(DataSet dataSet, int varIdx, UniformDomain staticVarDomain, DataAxisV vAxis)
        {
            this._dataSet = dataSet;
            this._graphs = new Graph[dataSet.NumItems];
            if (varIdx == 1)
            {
                this._graphs = new Graph[dataSet.NumItems + 1];
            }
            if (varIdx == 0)
            {
                this._graphs = new Graph[dataSet.NumItems + 1];
            }
            this._varIdx = varIdx;
            for (int i = 0; i < this._graphs.Length; i++)
            {
                if ((varIdx == 1) || (varIdx == 0))
                {
                    if (i == (this._graphs.Length - 1))
                    {
                        this._graphs[i] = new Graph(this.d_item(this._dataSet).GetVar(0), staticVarDomain, vAxis);
                    }
                    else
                    {
                        this._graphs[i] = new Graph(dataSet.GetItem(i).GetVar(varIdx), staticVarDomain, vAxis);
                    }
                }
                else
                {
                    this._graphs[i] = new Graph(dataSet.GetItem(i).GetVar(varIdx), staticVarDomain, vAxis);
                }
            }
        }

        public void BuildVarGraphs(int s, int e)
        {
            foreach (Graph graph in this._graphs)
            {
                graph.BuildGraph(s, e);
            }
        }

        public void clearResults()
        {
            foreach (Graph graph in this._graphs)
            {
                graph.clearResults();
            }
        }

        private DataItem d_item(DataSet ds)
        {
            double warn_d = 0.0;
            if (this._varIdx == 0)
            {
                warn_d =DBHelper.MathAdd.Ave(ds.GetItem(0).GetVar(0).get_values());
            }
            else
            {
                warn_d = 5.0;
            }
            double[][] d = new double[][] { new double[ds.get_timePointNames().Length] };
            for (int i = 0; i < ds.get_timePointNames().Length; i++)
            {
                d[0][i] = warn_d;
            }
            return new DataItem("arate_1", 0, d, new string[] { "arate_1" });
        }

        public Graph getGraph(int itemIndex)
        {
            return this._graphs[itemIndex];
        }

        public int[] GetItemIdxHavingResults()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < this._graphs.Length; i++)
            {
                if (this._graphs[i].SearchResult.HasResults)
                {
                    list.Add(i);
                }
            }
            return (int[]) list.ToArray(typeof(int));
        }

        public void renderGraphs(Graphics grfx, int global_s)
        {
            int i = 0;
            for (int j = 0; j < this._graphs.Length; j++)
            {
                string dd;
                if (this._varIdx == 0)
                {
                    if ((j < this._dataSet.DataItems.Length) && (this._graphs[j].DataVariable.get_strName == this._dataSet.GetItem(j).Name))
                    {
                        dd = this._dataSet.GetItem(j).Name;
                        this._graphs[j].RenderGraphWithMatches(grfx, global_s, true, 0);
                    }
                    if (this._graphs[j].DataVariable.get_strName == "arate_1")
                    {
                        this._graphs[j].RenderGraphWithMatches(grfx, global_s, true, -1);
                    }
                }
                else if (this._varIdx == 1)
                {
                    if ((j < this._dataSet.DataItems.Length) && (this._graphs[j].DataVariable.get_strName == this._dataSet.GetItem(j).Name))
                    {
                        dd = this._dataSet.GetItem(j).Name;
                        this._graphs[j].RenderGraphWithMatches(grfx, global_s, true, 0);
                    }
                    if (this._graphs[j].DataVariable.get_strName == "arate_1")
                    {
                        this._graphs[j].RenderGraphWithMatches(grfx, global_s, true, -1);
                    }
                }
                else
                {
                    dd = this._dataSet.getDynVarName(0);
                    string dd2 = this._dataSet.getDynVarName(1);
                    string dd1 = this._dataSet.GetItem(j).Name;
                    bool bb = this._dataSet._tsform.get_g_userid(this._dataSet.GetItem(j).Name);
                    if (this._dataSet._tsform.get_g_userid(this._dataSet.GetItem(j).Name))
                    {
                        this._graphs[j].RenderGraphWithMatches(grfx, global_s, true, j);
                        i++;
                    }
                }
            }
            int tt = i;
        }

        public void renderGraphs(Graphics grfx, PanelPaintStateDiff ppsDiff, int global_s)
        {
            int[] itemsToDisable = ppsDiff.ItemsToDisable;
            this.renderGraphs(grfx, itemsToDisable, global_s);
            itemsToDisable = ppsDiff.ItemsToUndisable;
            this.renderGraphs(grfx, itemsToDisable, global_s);
            if (ppsDiff.ItemToHighlight != -1)
            {
                this._graphs[ppsDiff.ItemToHighlight].RenderGraphWithMatches(grfx, global_s, false);
            }
            itemsToDisable = ppsDiff.ItemsToSelectFromEnabled;
            this.renderGraphs(grfx, itemsToDisable, global_s);
            itemsToDisable = ppsDiff.AddtlItemsToRedraw;
            this.renderGraphs(grfx, itemsToDisable, global_s);
        }

        private void renderGraphs(Graphics grfx, int[] itemIndices, int global_s)
        {
            for (int i = 0; i < itemIndices.Length; i++)
            {
                this._graphs[itemIndices[i]].RenderGraphWithMatches(grfx, global_s, false);
            }
        }

        public void setResults(SearchResults searchResults)
        {
            for (int i = 0; i < this._graphs.Length; i++)
            {
                this._graphs[i].SearchResult = searchResults.GetResult(i);
            }
        }

        public int Count
        {
            get
            {
                return this._graphs.Length;
            }
        }
    }
}


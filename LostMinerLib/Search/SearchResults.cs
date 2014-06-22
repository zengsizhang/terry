namespace TimeSearcher.Search
{
    using System;
    using System.Collections;

    public class SearchResults
    {
        private readonly int _patternLength;
        private readonly int _patternStartIndex;
        private ArrayList[] _resultIndices;
        private readonly SearchKey _searchKey;
        private readonly int _varIndex;

        public SearchResults(SearchKey sKey, int varIdx, int numItems, int patStartIndex, int patLength)
        {
            this._searchKey = sKey;
            this._varIndex = varIdx;
            this._resultIndices = new ArrayList[numItems];
            for (int i = 0; i < this._resultIndices.Length; i++)
            {
                this._resultIndices[i] = new ArrayList();
            }
            this._patternStartIndex = patStartIndex;
            this._patternLength = patLength;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SearchResults))
            {
                return false;
            }
            SearchResults results = (SearchResults) obj;
            if (!results._searchKey.Equals(this._searchKey))
            {
                return false;
            }
            return true;
        }

        public ArrayList getAllResults()
        {
            ArrayList list = new ArrayList();
            foreach (ArrayList list2 in this._resultIndices)
            {
                list.AddRange(list2);
            }
            return list;
        }

        public override int GetHashCode()
        {
            return this._searchKey.GetHashCode();
        }

        public int getPatLen()
        {
            return this._patternLength;
        }

        public int getPatStartIndex()
        {
            return this._patternStartIndex;
        }

        public SearchResult GetResult(int itemIndex)
        {
            return new SearchResult(this._resultIndices[itemIndex], this._patternLength);
        }

        public int getVarIndex()
        {
            return this._varIndex;
        }

        public bool isSameSearch(SearchResults searchResults)
        {
            return searchResults._searchKey.Equals(this._searchKey);
        }

        public void setResults(int itemIndex, ArrayList results)
        {
            this._resultIndices[itemIndex] = results;
        }
    }
}


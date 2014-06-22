namespace TimeSearcher
{
    using System;

    public class PanelPaintStateDiff
    {
        private readonly int[] _addtlItemsToRedraw;
        private readonly int[] _itemsToDisable;
        private readonly int[] _itemsToSelectFromEnabled;
        private readonly int[] _itemsToUndisable;
        private readonly int _itemToHighlight;

        public PanelPaintStateDiff(int[] itemsToUndisable, int[] itemsToDisable, int[] itemsToSelectFromEnabled, int itemToHighlight, int[] addtlItemsToRedraw)
        {
            this._itemsToUndisable = itemsToUndisable;
            this._itemsToDisable = itemsToDisable;
            this._itemsToSelectFromEnabled = itemsToSelectFromEnabled;
            this._itemToHighlight = itemToHighlight;
            this._addtlItemsToRedraw = addtlItemsToRedraw;
        }

        public int[] AddtlItemsToRedraw
        {
            get
            {
                return this._addtlItemsToRedraw;
            }
        }

        public int[] ItemsToDisable
        {
            get
            {
                return this._itemsToDisable;
            }
        }

        public int[] ItemsToSelectFromEnabled
        {
            get
            {
                return this._itemsToSelectFromEnabled;
            }
        }

        public int[] ItemsToUndisable
        {
            get
            {
                return this._itemsToUndisable;
            }
        }

        public int ItemToHighlight
        {
            get
            {
                return this._itemToHighlight;
            }
        }
    }
}


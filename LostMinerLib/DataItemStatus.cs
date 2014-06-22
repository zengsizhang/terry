namespace TimeSearcher
{
    using System;

    public class DataItemStatus
    {
        private bool _enabled;
        private bool _highlighted;
        private bool _selected;

        public DataItemStatus(bool enabled, bool selected, bool highlighted)
        {
            this._enabled = enabled;
            this._selected = selected;
            this._highlighted = highlighted;
        }

        public bool Enabled
        {
            get
            {
                return this._enabled;
            }
            set
            {
                this._enabled = value;
            }
        }

        public bool Highlighted
        {
            get
            {
                return this._highlighted;
            }
            set
            {
                this._highlighted = value;
            }
        }

        public static DataItemStatus NewDefaultStatus
        {
            get
            {
                return new DataItemStatus(true, false, false);
            }
        }

        public bool Selected
        {
            get
            {
                return this._selected;
            }
            set
            {
                this._selected = value;
            }
        }
    }
}


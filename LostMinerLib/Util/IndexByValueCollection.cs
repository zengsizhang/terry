namespace Util
{
    using System;
    using System.Collections;

    public class IndexByValueCollection
    {
        private readonly ArrayList _alIbvs = new ArrayList();

        public void Add(int index, double value)
        {
            this._alIbvs.Add(new IndexByValue(index, value));
        }

        private int findIndex(int index)
        {
            for (int i = 0; i < this._alIbvs.Count; i++)
            {
                if (((IndexByValue) this._alIbvs[i]).Index == index)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetNextIndex(int index)
        {
            int num = this.findIndex(index);
            int num2 = (num == (this._alIbvs.Count - 1)) ? 0 : (num + 1);
            return ((IndexByValue) this._alIbvs[num2]).Index;
        }

        public int GetPrevIndex(int index)
        {
            int num = this.findIndex(index);
            int num2 = (num == 0) ? (this._alIbvs.Count - 1) : (num - 1);
            return ((IndexByValue) this._alIbvs[num2]).Index;
        }

        public void Sort(bool ascending)
        {
            this._alIbvs.Sort();
            if (!ascending)
            {
                this._alIbvs.Reverse();
            }
        }
    }
}


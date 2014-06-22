namespace TimeSearcher.Wizard
{
    using System;
    using System.Runtime.InteropServices;
    using TimeSearcher.Search;

    [StructLayout(LayoutKind.Sequential)]
    public struct PreviewState
    {
        private MultiVarResSearchInfo mvrsInfo;
        private int startMin;
        private int startMax;
        private int endMin;
        private int endMax;
        public MultiVarResSearchInfo MVRSInfo
        {
            get
            {
                return this.mvrsInfo;
            }
        }
        public int StartMin
        {
            get
            {
                return this.startMin;
            }
        }
        public int StartMax
        {
            get
            {
                return this.startMax;
            }
        }
        public int EndMin
        {
            get
            {
                return this.endMin;
            }
        }
        public int EndMax
        {
            get
            {
                return this.endMax;
            }
        }
        public PreviewState(MultiVarResSearchInfo mvrsInfo, int startMin, int startMax, int endMin, int endMax)
        {
            this.mvrsInfo = mvrsInfo;
            this.startMin = startMin;
            this.startMax = startMax;
            this.endMin = endMin;
            this.endMax = endMax;
        }
    }
}


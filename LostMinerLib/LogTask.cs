namespace TimeSearcher
{
    using System;

    public class LogTask
    {
        private DateTime _beginTime;
        private DateTime _endTime;
        private string _name;

        public LogTask(string name)
        {
            this._name = name;
            this._beginTime = DateTime.Now;
        }

        public string Finish()
        {
            this._endTime = DateTime.Now;
            TimeSpan span = this._endTime.Subtract(this._beginTime);
            return (this._name + " " + span.TotalMilliseconds);
        }
    }
}


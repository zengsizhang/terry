namespace lostminer
{
    using System;
    using System.Windows.Forms;
    using TimeSearcher;
    internal class TimeSearcherStart
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
           Application.Run(new LostMinerLib.Wizard());
          //Application.Run(new TimeSearcher.TimeSearcherForm());
        }
    }
}


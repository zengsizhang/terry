namespace TimeSearcher.Properties
{
    using System;
    using System.Configuration;
    using System.Drawing;

    public class LineColor
    {
        public static Pen getlinecolor(int linenum)
        {
            try
            {
                string[] colorlist = ConfigurationManager.AppSettings["Color" + linenum].Split(new char[] { ',' });
                return new Pen(Color.FromArgb(int.Parse(colorlist[0]), int.Parse(colorlist[1]), int.Parse(colorlist[2])), 2f);
            }
            catch (Exception)
            {
                return new Pen(Brushes.Red, 1f);
            }
        }
    }
}


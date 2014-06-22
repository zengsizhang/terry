namespace TimeSearcher
{
    using System;
    using System.Drawing;

    public abstract class DataAxis
    {
        protected DataAxis()
        {
        }

        public abstract bool contains(Point point);
        public abstract void drawGrid(Graphics g);
        public abstract object getValueFromCoordinate(int coord);
        public abstract void Paint(Graphics g);
        public abstract void setSize(Rectangle rect);
    }
}


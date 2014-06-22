namespace LostMinerLib
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(MyOpaqueLayer))]
    public class MyOpaqueLayer : Control
    {
        private int _alpha;
        private bool _transparentBG;
        private Container components;

        public MyOpaqueLayer() : this(0x7d, true)
        {
        }

        public MyOpaqueLayer(int Alpha, bool IsShowLoadingImage)
        {
            this._transparentBG = false;
          //  this._alpha = 0x7d;
            this._alpha =1;
            this.components = new Container();
            base.SetStyle(ControlStyles.Opaque, true);
            base.CreateControl();
            this._alpha = Alpha;
            if (IsShowLoadingImage)
            {
                PictureBox pictureBox_Loading = new PictureBox();
                pictureBox_Loading.BackColor = Color.Red;
                pictureBox_Loading.Image = Image.FromFile("tsDirs/images/loading.gif");
                pictureBox_Loading.Name = "pictureBox_Loading";
                pictureBox_Loading.Size = new Size(1025, 768);
                pictureBox_Loading.SizeMode = PictureBoxSizeMode.AutoSize;
                Point Location = new Point(base.Location.X + ((base.Width - pictureBox_Loading.Width) / 2), base.Location.Y + ((base.Height - pictureBox_Loading.Height) / 2));
                pictureBox_Loading.Location = Location;
                pictureBox_Loading.Anchor = AnchorStyles.None;
                base.Controls.Add(pictureBox_Loading);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen labelBorderPen;
            SolidBrush labelBackColorBrush;
            if (this._transparentBG)
            {
                Color drawColor = Color.Red;
               // Color drawColor = Color.FromArgb(this._alpha, this.BackColor);
                labelBorderPen = new Pen(drawColor, 0f);
                labelBackColorBrush = new SolidBrush(drawColor);
            }
            else
            {
                Color drawColor = Color.Red;
                labelBorderPen = new Pen(drawColor, 0f);
             //   labelBorderPen = new Pen(this.BackColor, 0f);
                labelBackColorBrush = new SolidBrush(this.BackColor);
            }
            base.OnPaint(e);
            float vlblControlWidth = base.Size.Width;
            float vlblControlHeight = base.Size.Height;
            e.Graphics.DrawRectangle(labelBorderPen, 0f, 0f, vlblControlWidth, vlblControlHeight);
            e.Graphics.FillRectangle(labelBackColorBrush, 0f, 0f, vlblControlWidth, vlblControlHeight);
        }

        [Description("设置透明度"), Category("MyOpaqueLayer")]
        public int Alpha
        {
            get
            {
                return this._alpha;
            }
            set
            {
                this._alpha = value;
                base.Invalidate();
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        [Category("MyOpaqueLayer"), Description("是否使用透明,默认为True")]
        public bool TransparentBG
        {
            get
            {
                return this._transparentBG;
            }
            set
            {
                this._transparentBG = value;
                base.Invalidate();
            }
        }
    }
}


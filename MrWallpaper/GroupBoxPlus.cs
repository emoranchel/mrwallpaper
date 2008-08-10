using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MrWallpaper {
    class GroupBoxPlus : GroupBox{

        private Color topBgGradient = Color.LightBlue;
        private Color botBgGradient = Color.Blue;
        private Color topLbGradient = Color.LightBlue;
        private Color botLbGradient = Color.White;

        public Color TopBgGradient {
            get { return topBgGradient; }
            set { topBgGradient = value; }
        }
        public Color BotBgGradient {
            get { return botBgGradient; }
            set { botBgGradient = value; }
        }
        public Color TopLbGradient {
            get { return topLbGradient; }
            set { topLbGradient = value; }
        }
        public Color BotLbGradient {
            get { return botLbGradient; }
            set { botLbGradient = value; }
        }

        public override Rectangle DisplayRectangle {
            get {
                return new Rectangle(5, 25, this.Width - 10, this.Height - 30);
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            if (Width == 0 || Height == 0) {
                return;
            }
            Graphics g = e.Graphics;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Far;
            format.LineAlignment = StringAlignment.Center;
            SizeF sSize = g.MeasureString(this.Text, this.Font, this.Width, format);
            Point p1 = new Point(this.Width-10-(int)sSize.Width-(int)sSize.Height,0);
            Point p2 = new Point(this.Width,0);
            Point p3 = new Point(this.Width, 22);
            Point p4 = new Point(this.Width - 10 - (int)sSize.Width, 22);
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(new Point[]{p1, p2, p3, p4});
            LinearGradientBrush b = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), topBgGradient, botBgGradient);
            g.FillRectangle(b, new Rectangle(0, 0, this.Width, this.Height));
            b = new LinearGradientBrush(new Point(0, 0), new Point(0, 22), topLbGradient, botLbGradient);
            g.FillPath(b, path);
            g.DrawString(this.Text, this.Font, Brushes.Black, new RectangleF(7, 0, this.Width - 14, 22), format);
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            g.DrawPath(Pens.Black, path);
        }
    }
}

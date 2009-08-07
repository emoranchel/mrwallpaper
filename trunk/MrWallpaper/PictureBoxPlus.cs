using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ImageBrowser2 {
  public class PictureBoxPlus : PictureBox {
    private enum Corner { topLeft, topRight, bottomLeft, bottomRight };
    private Rectangle? imageBorders = null;
    private Rectangle? cropRectangle = null;

    private Image original;
    private Image cropedImage;

    private double aspectRatio;

    public double AspectRatio {
      get { return aspectRatio; }
      set { this.aspectRatio = value; }
    }
    public Image Original {
      get { return original; }
      set {
        this.original = value;
        cropRectangle = null;
        imageBorders = null;
        mouseRect = new Rectangle(0, 0, 0, 0);
        lastRect = new Rectangle(0, 0, 0, 0);
        rebuildPreview();
      }
    }
    public Image CropedImage {
      get {
        if (cropedImage == null) {
          if (CropRectangle.Width == 0 || CropRectangle.Height == 0 || original == null) {
            cropedImage = original;
          } else {
            System.Drawing.Image img = new Bitmap(CropRectangle.Width, CropRectangle.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(img);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(original, new Rectangle(0, 0, img.Width, img.Height), CropRectangle, GraphicsUnit.Pixel);
            g.Dispose();
            cropedImage = img;
          }
        }
        return cropedImage;
      }
    }
    public Rectangle ImageBorders {
      get {
        if (!imageBorders.HasValue) {
          Rectangle r = new Rectangle();
          if (original != null) {
            double ar = (double)original.Width / original.Height;
            r = MakeAspectRect(new Rectangle(0, 0, this.Width, this.Height), new Size(original.Width, original.Height), iMouse, eMouse);
            r.Y = (this.Height - r.Height) / 2;
            r.X = (this.Width - r.Width) / 2; ;
          }
          imageBorders = r;
        }
        return imageBorders.Value;
      }
    }
    public Rectangle CropRectangle {
      get {
        if (!cropRectangle.HasValue) {
          Rectangle r = new Rectangle();
          if (mouseRect.Width != 0 && mouseRect.Height != 0 && original != null) {
            double prop = (double)original.Width / ImageBorders.Width;
            r.X = (int)(((double)(mouseRect.X - ImageBorders.X)) * prop);
            r.Y = (int)(((double)(mouseRect.Y - ImageBorders.Y)) * prop);
            r.Width = (int)((double)mouseRect.Width * prop);
            r.Height = (int)((double)mouseRect.Height * prop);
          }
          cropRectangle = r;
        }
        return cropRectangle.Value;
      }
    }


    #region constructors
    public PictureBoxPlus() { this.SizeMode = PictureBoxSizeMode.CenterImage; }
    public PictureBoxPlus(Image original) : this() { this.Original = original; }
    #endregion

    #region MouseEvents
    private Point iMouse;
    private Point eMouse;
    private bool mouseDown;
    private bool moveRect;
    private bool resizeRect;
    private Rectangle mouseRect;
    private Rectangle lastRect;
    protected override void OnMouseDown(MouseEventArgs e) {
      if (e.Button == MouseButtons.Left) {
        mouseDown = true;
        iMouse = getPointBeetween(e.X, e.Y, ImageBorders);
        Rectangle previewCropRect = lastRect;
        Rectangle moveRectangle = new Rectangle(previewCropRect.X + 2, previewCropRect.Y + 2, previewCropRect.Width - 4, previewCropRect.Height - 4);
        moveRect = moveRectangle.IntersectsWith(new Rectangle(e.X, e.Y, 0, 0));
        resizeRect = !moveRect && new Rectangle(previewCropRect.X - 2, previewCropRect.Y - 2, previewCropRect.Width + 4, previewCropRect.Height + 4).IntersectsWith(new Rectangle(e.X, e.Y, 0, 0));
      }
      base.OnMouseDown(e);
    }
    protected override void OnMouseUp(MouseEventArgs e) {
      OnMouseMove(e);
      if (mouseDown || moveRect) {
        mouseDown = false;
        moveRect = false;
        lastRect = mouseRect;
        cropedImage = null;
        cropRectangle = null;
        rebuildPreview();
      }
      base.OnMouseUp(e);
    }
    protected override void OnMouseMove(MouseEventArgs e) {
      if (mouseDown) {
        eMouse = getPointBeetween(e.X, e.Y, ImageBorders);
        if (moveRect) {
          mouseRect.X = lastRect.X + (eMouse.X - iMouse.X);
          mouseRect.Y = lastRect.Y + (eMouse.Y - iMouse.Y);
          mouseRect = realignRect(mouseRect, ImageBorders);
        } else {
          mouseRect = getRect(iMouse, eMouse);
          if (Math.Abs(mouseRect.X - ImageBorders.X) < 7) {
            mouseRect.X = ImageBorders.X;
            if (Math.Abs(mouseRect.Width - ImageBorders.Width) < 15) mouseRect.Width = ImageBorders.Width;
          }
          if (Math.Abs(mouseRect.Y - ImageBorders.Y) < 7) {
            mouseRect.Y = ImageBorders.Y;
            if (Math.Abs(mouseRect.Height - ImageBorders.Height) < 15) mouseRect.Height = ImageBorders.Height;
          }
          if (Math.Abs(mouseRect.X + mouseRect.Width - (ImageBorders.Width + ImageBorders.X)) < 7) mouseRect.Width = ImageBorders.Width - mouseRect.X + ImageBorders.X;
          if (Math.Abs(mouseRect.Y + mouseRect.Height - (ImageBorders.Height + ImageBorders.Y)) < 7) mouseRect.Height = ImageBorders.Height - mouseRect.Y + ImageBorders.Y;
          if (aspectRatio != 0) {
            mouseRect = MakeAspectRect(mouseRect, aspectRatio, iMouse, eMouse);
          }
        }
        this.Invalidate();
      }
      base.OnMouseMove(e);
    }
    protected override void OnPaint(PaintEventArgs pe) {
      base.OnPaint(pe);
      pe.Graphics.DrawRectangle(Pens.Black, mouseRect);
    }
    protected override void OnResize(EventArgs e) {
      base.OnResize(e);
      imageBorders = null;
      rebuildPreview();
    }
    #endregion

    private void rebuildPreview() {
      if (ImageBorders.Width == 0 || ImageBorders.Height == 0 || original == null) {
        this.Image = null;
        return;
      }
      System.Drawing.Image img = new Bitmap(ImageBorders.Width, ImageBorders.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
      Graphics g = Graphics.FromImage(img);
      g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      g.DrawImage(original, new Rectangle(0, 0, img.Width, img.Height), new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
      if (mouseRect.Width != 0 && mouseRect.Height != 0) {
        System.Drawing.Region region = new Region(new Rectangle(0, 0, img.Width, img.Height));
        region.Exclude(new Rectangle(mouseRect.X - ImageBorders.X, mouseRect.Y - ImageBorders.Y, mouseRect.Width, mouseRect.Height));
        g = Graphics.FromImage(img);
        g.FillRegion(new SolidBrush(Color.FromArgb(100, Color.Black)), region);
      }
      g.Dispose();
      this.Image = img;
    }

    #region utilities
    private Point getPointBeetween(int x, int y, Rectangle bounds) {
      Point p = new Point();
      p.X = x < bounds.X ? bounds.X : x > (bounds.X + bounds.Width) ? bounds.X + bounds.Width : x;
      p.Y = y < bounds.Y ? bounds.Y : y > (bounds.Y + bounds.Height) ? bounds.Y + bounds.Height : y;
      return p;
    }
    private Rectangle getRect(Point p1, Point p2) {
      return getRect(p1.X, p1.Y, p2.X, p2.Y);
    }
    private Rectangle getRect(int iX, int iY, int eX, int eY) {
      Rectangle r = new Rectangle();
      r.X = iX < eX ? iX : eX;
      r.Y = iY < eY ? iY : eY;
      r.Width = Math.Abs(eX - iX);
      r.Height = Math.Abs(eY - iY);
      return r;
    }
    private Rectangle MakeAspectRect(Rectangle mouseRect, double ar, Point startPoint, Point endPoint) {
      Rectangle r = new Rectangle();
      if (ar > 1.0) {
        r.Width = mouseRect.Width;
        r.Height = (int)((double)r.Width / ar);
        if (r.Height > mouseRect.Height) {
          r.Height = mouseRect.Height;
          r.Width = (int)((double)r.Height * ar);
        }
      } else {
        r.Height = mouseRect.Height;
        r.Width = (int)((double)r.Height * ar);
        if (r.Width > mouseRect.Width) {
          r.Width = mouseRect.Width;
          r.Height = (int)((double)r.Width / ar);
        }
      }
      switch (getCorner(startPoint, endPoint)) {
        case Corner.topLeft:
          r.X = mouseRect.X;
          r.Y = mouseRect.Y;
          break;
        case Corner.topRight:
          if (mouseRect.Width > r.Width) {
            r.X = mouseRect.X + (mouseRect.Width - r.Width);
          } else {
            r.X = mouseRect.X;
          }
          r.Y = mouseRect.Y;
          break;
        case Corner.bottomLeft:
          r.X = mouseRect.X;
          if (mouseRect.Height > r.Height) {
            r.Y = mouseRect.Y + (mouseRect.Height - r.Height);
          } else {
            r.Y = mouseRect.Y;
          }
          break;
        case Corner.bottomRight:
          if (mouseRect.Width > r.Width) {
            r.X = mouseRect.X + (mouseRect.Width - r.Width);
          } else {
            r.X = mouseRect.X;
          }
          if (mouseRect.Height > r.Height) {
            r.Y = mouseRect.Y + (mouseRect.Height - r.Height);
          } else {
            r.Y = mouseRect.Y;
          }
          break;
      }
      return r;
    }
    private Rectangle MakeAspectRect(Rectangle mouseRect, Size original, Point startPoint, Point endPoint) {
      return MakeAspectRect(mouseRect, (double)original.Width / original.Height, startPoint, endPoint);
    }
    private Rectangle realignRect(Rectangle rectangle, Rectangle bounds) {
      Rectangle rec = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
      if (rec.X < bounds.X) rec.X += (bounds.X - rec.X);
      if (rec.Y < bounds.Y) rec.Y += (bounds.Y - rec.Y);
      if (rec.Width > bounds.Width) rec.Width = bounds.Width;
      if (rec.Height > bounds.Height) rec.Height = bounds.Height;
      if ((rec.X + rec.Width) > (bounds.X + bounds.Width))
        rec.X = (bounds.X + bounds.Width) - rec.Width;
      if ((rec.Y + rec.Height) > (bounds.Y + bounds.Height))
        rec.Y = (bounds.Y + bounds.Height) - rec.Height;
      return rec;
    }

    private Corner getCorner(Point initialPoint, Point finalPoint) {
      if (initialPoint.X > finalPoint.X) {
        if (initialPoint.Y > finalPoint.Y) {
          return Corner.bottomRight;
        } else {
          return Corner.topRight;
        }
      }
      if (initialPoint.Y > finalPoint.Y) {
        return Corner.bottomLeft;
      }
      return Corner.topLeft;
    }

    #endregion
  }
}

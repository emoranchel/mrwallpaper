using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ImageSearch.Entity;
using System.Drawing;

namespace ImageSearch {
    public class ImageFinder {

        public event EventHandler<ProgressEventArgs> ProgressChanged;
        public event EventHandler FindStarted;
        public event EventHandler FindFinished;
        public event EventHandler<ImageDataCompleteEventArgs> DataReady;
        public event EventHandler<ImageFoundEventArgs> imageFound;

        protected void onProgressChanged(double partial, double total, string filename) {
            if (ProgressChanged != null) {
                ProgressChanged(this, new ProgressEventArgs(partial, total, filename));
            }
        }

        protected void onFindStarted() {
            if (FindStarted != null) {
                FindStarted(this, EventArgs.Empty);
            }
        }

        protected void onFindFinished() {
            if (FindFinished != null) {
                FindFinished(this, EventArgs.Empty);
            }
        }

        protected void onDataReady(List<ImageData> data) {
            if (DataReady != null) {
                DataReady(this, new ImageDataCompleteEventArgs(data));
            }
        }
        protected void onImageFound(ImageData image) {
          if (imageFound != null) {
            imageFound(this, new ImageFoundEventArgs(image));
          }
        }
        private bool recursive;
        private int? minimumWidth = null;
        private int? minimumHeight = null;

        private int? maximumWidth = null;
        private int? maximumHeight = null;

        private double? minimumAspectRatio = null;
        private double? maximumAspectRatio = null;

        private int? total = null;

        private List<string> paths = new List<string>();

        public bool Recursive { get { return recursive; } set { recursive = value; } }

        public int? MinimumWidth { get { return minimumWidth; } set { minimumWidth = value; } }
        public int? MinimumHeight { get { return minimumHeight; } set { minimumHeight = value; } }

        public int? MaximumWidth { get { return maximumWidth; } set { maximumWidth = value; } }
        public int? MaximumHeight { get { return maximumHeight; } set { maximumHeight = value; } }

        public double? MinimumAspectRatio { get { return minimumAspectRatio; } set { minimumAspectRatio = value; } }
        public double? MaximumAspectRatio { get { return maximumAspectRatio; } set { maximumAspectRatio = value; } }

        public int Total {
            get {
                if (!total.HasValue) {
                    total = getTotal();
                }
                return total.Value;
            }
        }

        private int getTotal() {
            int i = 0;
            foreach (string s in paths) {
                DirectoryInfo dir = new DirectoryInfo(s);
                i += dir.GetFiles().Length;
            }
            return i;
        }
        
        private int current = 0;
        public void findImages() {
            current = 0;
            List<ImageData> dat = new List<ImageData>();
            onFindStarted();
            foreach (string path in paths) {
                DirectoryInfo dir = new DirectoryInfo(path);
                dat.AddRange(findImages(dir));
            }
            onFindFinished();
            onDataReady(dat);
        }

        public bool addPathsRecursive(string path) {
            bool ret = false;
            DirectoryInfo dirs = new DirectoryInfo(path);
            if ((dirs.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden || (dirs.Attributes & FileAttributes.System) == FileAttributes.System) {
                return false;
            }
            DirectoryInfo[] subdirs = null;
            try {
                subdirs = dirs.GetDirectories();
                addPath(path);
                foreach (DirectoryInfo d in subdirs) {
                    ret = addPathsRecursive(d.FullName) || ret;
                }
                total = null;
            } catch (System.Exception) { return false; }
            return true;
        }

        public bool addPath(string path) {
            paths.Add(path);
            total = null;
            return true;
        }

        private List<ImageData> findImages(DirectoryInfo dir) {
            List<ImageData> images = new List<ImageData>();
            FileInfo[] files = dir.GetFiles();
            bool hasWork = minimumWidth.HasValue || minimumHeight.HasValue || minimumAspectRatio.HasValue 
                || maximumWidth.HasValue || maximumHeight.HasValue || maximumAspectRatio.HasValue;
            foreach (FileInfo file in files) {
                current++;
                onProgressChanged(current, Total, file.FullName);
                try {
                    if (hasWork) {
                        Image img = new Bitmap(file.FullName);
                        ImageData data = new ImageData();
                        data.Width = img.Width;
                        data.Height = img.Height;
                        img.Dispose();
                        if (minimumHeight.HasValue && minimumHeight.Value >= data.Height) { continue; }
                        if (minimumWidth.HasValue && minimumWidth.Value >= data.Width) { continue; }
                        if (maximumHeight.HasValue && maximumHeight.Value <= data.Height) { continue; }
                        if (maximumWidth.HasValue && maximumWidth.Value <= data.Width) { continue; }
                        if (minimumAspectRatio.HasValue && minimumAspectRatio.Value >= data.AspectRatio) { continue; }
                        if (maximumAspectRatio.HasValue && maximumAspectRatio.Value <= data.AspectRatio) { continue; }
                        data.Filename = file.FullName;
                        data.File = file;
                        images.Add(data);
                        onImageFound(data);
                    } else {
                        if (file.Extension == ".jpg" || file.Extension == ".bmp" || file.Extension == ".png") {
                            ImageData data = new ImageData();
                            data.Width = 1;
                            data.Height = 1;
                            data.Filename = file.FullName;
                            data.File = file;
                            images.Add(data);
                        }
                    }
                } catch (System.Exception) { }
            }
            return images;
        }
    }
    public class ProgressEventArgs : EventArgs {
        private int progress;
        private string filename;
        private double partial;
        private double total;

        public int Progress {
            get { return progress; }
            set { progress = value < 0 ? 0 : (value > 100 ? 100 : value); }
        }
        public string Filename {
            get { return filename; }
            set { this.filename = value; }
        }
        public double Partial { get { return partial; } set { partial = value; } }
        public double Total { get { return total; } set { total = value; } }
        public ProgressEventArgs(double partial, double total, string filename) {
            Progress = (int)((partial / total) * 100);
            this.partial = partial;
            this.total = total;
            this.filename = filename;
        }
    }
    public class ImageDataComparator : IComparer<ImageData> {

        private double aspectRatio;

        public ImageDataComparator(double aspectRatio) {
            this.aspectRatio = aspectRatio;
        }

        #region IComparer<ImageData> Members

        public int Compare(ImageData x, ImageData y) {
            double a = Math.Abs(y.AspectRatio - this.aspectRatio);
            double b = Math.Abs(x.AspectRatio - this.aspectRatio);
            if (a < b) {
                return 1;
            }
            if (a > b) {
                return -1;
            }
            return 0;
        }

        #endregion
    }
    public class ImageDataCompleteEventArgs : EventArgs {
        private List<ImageData> data;
        public ImageDataCompleteEventArgs(List<ImageData> data) {
            this.data = data;
        }
        public List<ImageData> Data { get { return data; } }
    }
  public class ImageFoundEventArgs:EventArgs{
    private ImageData imageData;
    public ImageFoundEventArgs(ImageData imageData) {
      this.imageData = imageData;
    }
    public ImageData img {
      get { return imageData; }
    }
  }
}

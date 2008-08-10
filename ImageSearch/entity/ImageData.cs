using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ImageSearch.Entity {
    public class ImageData {
        private string filename;
        private int width;
        private int height;
        private FileInfo file;
        public string Filename {
            get { return filename; }
            set { this.filename = value; }
        }
        public int Width {
            get { return width; }
            set { this.width = value; }
        }
        public int Height {
            get { return height; }
            set { this.height = value; }
        }
        public FileInfo File {
            get { return file; }
            set { this.file = value; }
        }
        public double AspectRatio {
            get { return (double)width / height; }
        }
    }
}

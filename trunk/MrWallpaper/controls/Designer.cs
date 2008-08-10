using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace MrWallpaper.controls {
    public partial class Designer : UserControl {
        public event EventHandler ImageListEmpty;
        protected void onImageListEmpty() {
            if (ImageListEmpty != null) {
                ImageListEmpty(this, EventArgs.Empty);
            }
        }

        public Designer() {
            InitializeComponent();
        }
        public void addFile(string filename) {
            listBox1.Items.Add(new FileDat(filename));
            if (listBox1.SelectedIndex < 0 && listBox1.Items.Count > 0) {
                listBox1.SelectedIndex = 0;
            }
        }

        public void addFileRange(List<string> filenames) {
            foreach (string filename in filenames) {
                listBox1.Items.Add(new FileDat(filename));
            }
            if (listBox1.SelectedIndex < 0 && listBox1.Items.Count > 0) {
                listBox1.SelectedIndex = 0;
            }
        }
        private int lastSize = 0;
        private int lastIndex = 0;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (listBox1.Items.Count != lastSize || listBox1.SelectedIndex != lastIndex) {
                if (listBox1.SelectedIndex >= 0) {
                    FileDat dat = (FileDat)listBox1.SelectedItem;
                    string filename = dat.FileName;
                    try {
                        Image img = Bitmap.FromFile(filename);
                        pictureBoxPlus1.Original = img;
                    } catch (System.Exception ex) {
                        MessageBox.Show("Unable to open image:\n" + filename + "\nError:" + ex.Message);
                        button1_Click(this, EventArgs.Empty);
                    }
                } else {
                    pictureBoxPlus1.Original = null;
                    if (listBox1.Items.Count == 0) {
                        onImageListEmpty();
                    }
                }
                lastSize = listBox1.Items.Count;
                lastIndex = listBox1.SelectedIndex;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType) {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j) {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (listBox1.Items.Count > 0) {
                int index = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(index);
                if (index >= listBox1.Items.Count) {
                    index = listBox1.Items.Count - 1;
                }
                if (index >= 0) {
                    listBox1.SelectedIndex = index;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK) {
                List<string> filenames = new List<string>();
                foreach (string file in openFileDialog1.FileNames) {
                    addFile(file);
                }
            }
        }

        private void Designer_Load(object sender, EventArgs e) {
            scrWidth.Value = Screen.PrimaryScreen.Bounds.Width;
            scrHeight.Value = Screen.PrimaryScreen.Bounds.Height;
        }

        private void screenSizeChanged(object sender, EventArgs e) {
            if (scrHeight.Value != 0) {
                pictureBoxPlus1.AspectRatio = (double)(scrWidth.Value / scrHeight.Value);
            }
        }

        private void button5_Click(object sender, EventArgs e) {
            DialogResult res = saveFileDialog1.ShowDialog();
            if (res == DialogResult.OK) {
                string filename = saveFileDialog1.FileName;
                string ext = saveFileDialog1.Filter.Split('|')[(saveFileDialog1.FilterIndex * 2) - 1];
                // remove the * from the *.jpg
                ext = ext.Substring(1);
                if (ext == ".*")
                {
                    ext = ".jpg";
                }
                if (!filename.EndsWith(ext))
                {
                    filename += ext;
                }
                if (ext == ".jpg")
                {

                    Image wallpaper = new Bitmap((int)scrWidth.Value, (int)scrHeight.Value, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    Graphics g = Graphics.FromImage(wallpaper);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(pictureBoxPlus1.CropedImage, new Rectangle(0, 0, wallpaper.Width, wallpaper.Height));
                    g.Dispose();

                    ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    myEncoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

                    wallpaper.Save(saveFileDialog1.FileName, myImageCodecInfo, myEncoderParameters);
                }
            }
        }
    }
    class FileDat {
        private string filename;
        private string name;
        public string FileName { get { return filename; } }
        public FileDat(string filename) {
            FileInfo info = new FileInfo(filename);
            this.filename = filename;
            this.name = info.Name;
        }
        public override string ToString() {
            return name;
        }
    }
}

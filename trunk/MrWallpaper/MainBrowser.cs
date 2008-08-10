using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ImageSearch;
using System.Threading;
using System.IO;
using ImageSearch.Entity;

namespace ImageBrowser2 {
    public partial class MainBrowser : Form {
        public MainBrowser() {
            InitializeComponent();
            browser1.MakeWallpaper += new EventHandler<MrWallpaper.controls.WallpaperEventArgs>(browser1_MakeWallpaper);
            browser1.MakeWallpaperRange += new EventHandler<MrWallpaper.controls.WallpaperRangeEventArgs>(browser1_MakeWallpaper);
            designer1.ImageListEmpty += new EventHandler(designer1_ImageListEmpty);
        }

        void designer1_ImageListEmpty(object sender, EventArgs e) {
            DialogResult res = MessageBox.Show(this, "Your image list is empty.\nDo you want to go back to the browser? \nYou can still open images using the open button.", "Image list is empty", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes) {
                flatTabControl1.SelectedTab = tabPage3;
            }
        }

        void browser1_MakeWallpaper(object sender, MrWallpaper.controls.WallpaperEventArgs e) {
            flatTabControl1.SelectedTab = tabPage4;
            designer1.addFile(e.FileName);
        }

        void browser1_MakeWallpaper(object sender, MrWallpaper.controls.WallpaperRangeEventArgs e) {
            flatTabControl1.SelectedTab = tabPage4;
            designer1.addFileRange(e.FileNames);
        }
    }
}

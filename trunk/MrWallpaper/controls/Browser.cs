using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ImageSearch;
using ImageSearch.Entity;
using System.IO;
using System.Threading;

namespace MrWallpaper.controls {
    public partial class Browser : UserControl {

        public event EventHandler<WallpaperEventArgs> MakeWallpaper;
        public event EventHandler<WallpaperRangeEventArgs> MakeWallpaperRange;

        protected void onMakeWallpaper(string filename) {
            if (MakeWallpaper != null) {
                MakeWallpaper(this, new WallpaperEventArgs(filename));
            }
        }

        protected void onMakeWallpaperRange(List<string> filenames) {
            if (MakeWallpaperRange != null) {
                MakeWallpaperRange(this, new WallpaperRangeEventArgs(filenames));
            }
        }

        public Browser() {
            InitializeComponent();
        }

        private void control_load(object sender, EventArgs e) {
            scrWidth.Value = Screen.PrimaryScreen.Bounds.Width;
            scrHeight.Value = Screen.PrimaryScreen.Bounds.Height;
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives) {
                TreeNode node = new TreeNode(drive.Name);
                TreeNodeData dat = new TreeNodeData(drive.Name);
                node.Tag = dat;
                treeView1.Nodes.Add(node);
                addNodes(node);
            }
            try {
                doThaPathRecursiveThing(new DirectoryInfo(MrWallpaper.Properties.Settings.Default.LastPath));
            } catch (System.Exception) { }
            init = true;
            setAspectRatioValues(this, EventArgs.Empty);
            setResolutionValues(this, EventArgs.Empty);
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            ImageData data = (ImageData)e.Item.Tag;
            imgPreview.Image = new Bitmap(data.Filename);
        }

        private ImageFinder SeekFiles(bool doSearch) {
            ImageFinder imageFinder = new ImageFinder();
            imageFinder.Recursive = chkRecursive.Checked;
            bool hasParam = false;
            if (doSearch) {
                if (chkMinRes.Checked) {
                    imageFinder.MinimumHeight = (int)minHeight.Value;
                    imageFinder.MinimumWidth = (int)minWidth.Value;
                    hasParam = true;
                }
                if (chkMaxRes.Checked) {
                    imageFinder.MaximumHeight = (int)maxHeight.Value;
                    imageFinder.MaximumWidth = (int)maxWidth.Value;
                    hasParam = true;
                }
                if (chkMinAR.Checked) {
                    imageFinder.MinimumAspectRatio = (double)minAR.Value;
                    hasParam = true;
                }
                if (chkMaxAR.Checked) {
                    imageFinder.MaximumAspectRatio = (double)maxAR.Value;
                    hasParam = true;
                }
            }
            bool added = false;
            foreach (TreeNode node in treeView1.Nodes) {
                added = checkAndAdd(node, imageFinder, false) || added;
            }
            if (!added) {
                if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null) {
                    MessageBox.Show(this, "You must select at least one folder", "Error", MessageBoxButtons.OK);
                    return null;
                }
                if (chkRecursive.Checked) {
                    imageFinder.addPathsRecursive(((TreeNodeData)treeView1.SelectedNode.Tag).FullPath);
                } else {
                    imageFinder.addPath(((TreeNodeData)treeView1.SelectedNode.Tag).FullPath);
                }
            }
            if (imageFinder.Total > 2500 && hasParam) {
                DialogResult res = MessageBox.Show(this, "You are about to process " + imageFinder.Total + " files...\nAre you sure?", "Confirm", MessageBoxButtons.YesNo);
                if (res == DialogResult.No)
                    return null;
                if (imageFinder.Total > 7000) {
                    res = MessageBox.Show(this, "Really they are a lot.\n" + imageFinder.Total + " Files may take too long to process...\nDon't you think?", "Confirm again", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                        return null;
                }
            }
            imageFinder.ProgressChanged += new EventHandler<ProgressEventArgs>(imageFinder_ProgressChanged);
            imageFinder.DataReady += new EventHandler<ImageDataCompleteEventArgs>(imageFinder_DataReady);
            return imageFinder;
        }

        private bool checkAndAdd(TreeNode node, ImageFinder imageFinder, bool adding) {
            bool added = false;
            TreeNodeData dat = (TreeNodeData)node.Tag;
            if (imageFinder.Recursive) {
                if (node.Checked) {
                    adding = !adding;
                }
                if (adding) {
                    if (node.Nodes.Count == 0) {
                        added = imageFinder.addPathsRecursive(dat.FullPath) || added;
                    } else {
                        imageFinder.addPath(dat.FullPath);
                        added = true;
                    }
                }
                foreach (TreeNode n in node.Nodes) {
                    added = checkAndAdd(n, imageFinder, adding) || added;
                }
            } else {
                if (node.Checked) {
                    added = imageFinder.addPath(dat.FullPath);
                }
                foreach (TreeNode n in node.Nodes) {
                    added = checkAndAdd(n, imageFinder, false) || added;
                }
            }
            return added;
        }

        void imageFinder_DataReady(object sender, ImageDataCompleteEventArgs e) {
            if (this.InvokeRequired) {
                this.Invoke(new EventHandler<ImageDataCompleteEventArgs>(imageFinder_DataReady), sender, e);
            } else {
                images = e.Data;
                double width = (double)scrWidth.Value;
                double height = (double)scrHeight.Value;
                images.Sort(new ImageDataComparator(width / height));
                progressBar1.Value = 100;
                lblProgress.Text = "";
                reloadList();
            }
        }

        void imageFinder_ProgressChanged(object sender, ProgressEventArgs e) {
            if (this.InvokeRequired) {
                this.Invoke(new EventHandler<ProgressEventArgs>(imageFinder_ProgressChanged), sender, e);
            } else {
                progressBar1.Value = e.Progress;
                lblProgress.Text = e.Filename + string.Format("({0:N0}/{1:N0})", e.Partial, e.Total);
            }
        }

        private List<ImageData> images = null;
        private void reloadList() {
            listView1.Items.Clear();
            foreach (ImageData img in images) {
                ListViewItem item = new ListViewItem();
                item.Text = img.File.FullName;
                item.Tag = img;
                item.SubItems.Add(string.Format("{0:N}", img.AspectRatio));
                listView1.Items.Add(item);
            }
        }
        private bool init = false;
        private TreeNode doThaPathRecursiveThing(DirectoryInfo dir) {
            TreeNodeCollection nodes = null;
            if (dir.Parent == null) {
                nodes = treeView1.Nodes;
            } else {
                TreeNode node = doThaPathRecursiveThing(dir.Parent);
                if (node != null) {
                    addNodes(node);
                    treeView1.SelectedNode = node;
                    nodes = node.Nodes;
                }
            }
            if (nodes == null)
                return null;
            foreach (TreeNode node in nodes) {
                if (dir.FullName == ((TreeNodeData)node.Tag).FullPath) {
                    treeView1.SelectedNode = node;
                    return node;
                }
            }
            return null;
        }
        private void buttonSort_click(object sender, EventArgs e) {
            int width = (int)scrWidth.Value;
            int height = (int)scrHeight.Value;
            images.Sort(new ImageDataComparator((double)width / height));
            reloadList();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e) {
            addNodes(e.Node);
            if (init) {
                MrWallpaper.Properties.Settings.Default.LastPath = ((TreeNodeData)e.Node.Tag).FullPath;
                MrWallpaper.Properties.Settings.Default.Save();
            }
        }
        private void addNodes(TreeNode root) {
            TreeNodeData dat = (TreeNodeData)root.Tag;
            if (dat.Explored)
                return;
            try {
                DirectoryInfo inf = new DirectoryInfo(dat.FullPath);
                foreach (DirectoryInfo dir in inf.GetDirectories()) {
                    if (((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) || ((dir.Attributes & FileAttributes.System) == FileAttributes.System)) {
                        continue;
                    }
                    TreeNode node = new TreeNode(dir.Name);
                    TreeNodeData data = new TreeNodeData(dir.FullName);
                    node.Tag = data;
                    root.Nodes.Add(node);
                }
                dat.Explored = true;
            } catch (IOException) { } catch (UnauthorizedAccessException) { }
        }

        private void treeView_AfterExpand(object sender, TreeViewEventArgs e) {
            foreach (TreeNode node in e.Node.Nodes) {
                addNodes(node);
            }
            if (init) {
                MrWallpaper.Properties.Settings.Default.LastPath = ((TreeNodeData)e.Node.Tag).FullPath;
                MrWallpaper.Properties.Settings.Default.Save();
            }
        }

        private void treeView_AfterCollapse(object sender, TreeViewEventArgs e) {
            foreach (TreeNode node in e.Node.Nodes) {
                node.Nodes.Clear();
                ((TreeNodeData)node.Tag).Explored = false;
            }
            if (init) {
                MrWallpaper.Properties.Settings.Default.LastPath = ((TreeNodeData)e.Node.Tag).FullPath;
                MrWallpaper.Properties.Settings.Default.Save();
            }
        }

        private void buttonOpen_click(object sender, EventArgs e) {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = listView1.SelectedItems[0].Text;
            proc.Start();
        }

        private void buttonOpenDirectory_Click(object sender, EventArgs e) {
            FileInfo fi = new FileInfo(listView1.SelectedItems[0].Text);
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "explorer.exe";
            proc.StartInfo.Arguments = fi.Directory.FullName;
            proc.Start();
        }

        private void buttonMakeWallpaper_Click(object sender, EventArgs e) {
            if (listView1.SelectedItems.Count > 0) {
                if (listView1.SelectedItems.Count == 1) {
                    onMakeWallpaper(listView1.SelectedItems[0].Text);
                } else {
                    List<string> files = new List<string>();
                    foreach (ListViewItem item in listView1.SelectedItems) {
                        files.Add(item.Text);
                    }
                    onMakeWallpaperRange(files);
                }
            } else {
                MessageBox.Show(this, "Must choose at least one file");
            }
        }

        private void treeView_AfterCheck(object sender, TreeViewEventArgs e) {
            if (init) {
                MrWallpaper.Properties.Settings.Default.LastPath = ((TreeNodeData)e.Node.Tag).FullPath;
                MrWallpaper.Properties.Settings.Default.Save();
            }
        }

        private void setResolutionValues(object sender, EventArgs e) {
            decimal width = scrWidth.Value;
            decimal height = scrHeight.Value;
            decimal pre = (100.0m - resolutionPrecision.Value) / 100.0m;
            if (width == 0 || height == 0) return;
            scrAspectRatio.Value = width / height;
            if (pre < 1) {
                decimal deltaHeight = height * pre;
                decimal deltaWidth = width * pre;
                minHeight.Value = height - deltaHeight;
                minWidth.Value = width - deltaWidth;
                maxHeight.Value = height + deltaHeight;
                maxWidth.Value = width + deltaWidth;
                chkMinRes.Checked = true;
            } else {
                chkMinRes.Checked = false;
                chkMaxRes.Checked = false;
            }
        }

        private void setAspectRatioValues(object sender, EventArgs e) {
            decimal ar = scrAspectRatio.Value;
            decimal pre = (100.0m - aspectRatioPrecision.Value) / 100.0m;
            if (pre < 1) {
                decimal delta = ar * pre;
                minAR.Value = ar - delta;
                maxAR.Value = ar + delta;
                chkMinAR.Checked = true;
                chkMaxAR.Checked = true;
            } else {
                chkMinAR.Checked = false;
                chkMaxAR.Checked = false;
            }
        }

        private void buttonMoreOptions_Click(object sender, EventArgs e) {
            moOptions.Visible = !moOptions.Visible;
            if (moOptions.Visible) {
                button3.Text = "Less Options";
            } else {
                button3.Text = "More Options";
            }
        }

        private void buttonShowFiles_Click(object sender, EventArgs e) {
            buttonShow.Enabled = false;
            ImageFinder imageFinder = SeekFiles(false);
            if (imageFinder != null) {
                imageFinder.FindFinished += new EventHandler(showFinished);
                Thread t = new Thread(new ThreadStart(imageFinder.findImages));
                t.IsBackground = true;
                t.Name = "Finder Thread";
                t.Start();
            } else {
                buttonSearch.Enabled = true;
            }
        }

        private void showFinished(object sender, EventArgs e) {
            if (buttonShow.InvokeRequired) {
                buttonShow.Invoke(new EventHandler(showFinished));
            } else {
                buttonShow.Enabled = true;
            }
        }

        private void buttonSearchImages_Click(object sender, EventArgs e) {
            buttonSearch.Enabled = false;
            ImageFinder imageFinder = SeekFiles(true);
            if (imageFinder != null) {
                imageFinder.FindFinished += new EventHandler(searchFinished);
                Thread t = new Thread(new ThreadStart(imageFinder.findImages));
                t.IsBackground = true;
                t.Name = "Finder Thread";
                t.Start();
            } else {
                buttonSearch.Enabled = true;
            }
        }

        void searchFinished(object sender, EventArgs e) {
            if (buttonShow.InvokeRequired) {
                buttonShow.Invoke(new EventHandler(searchFinished));
            } else {
                buttonSearch.Enabled = true;
            }
        }

        private void listView1_MouseEnter(object sender, EventArgs e) {
          listView1.Focus();
        }

    }

    public class WallpaperEventArgs : EventArgs {
        private string filename;
        public string FileName { get { return filename; } set { filename = value; } }
        public WallpaperEventArgs(string filename) {
            this.filename = filename;
        }
            
    }

    public class WallpaperRangeEventArgs : EventArgs {
        private List<string> filenames;
        public List<string> FileNames { get { return filenames; } set { filenames = value; } }
        public WallpaperRangeEventArgs(List<string> filenames) {
            this.filenames = filenames;
        }

    }

    public class TreeNodeData {
        private bool explored;
        private string fullPath;
        public TreeNodeData(string fullPath) {
            this.fullPath = fullPath;
        }
        public bool Explored { get { return explored; } set { this.explored = value; } }
        public string FullPath { get { return fullPath; } set { this.fullPath = value; } }
    }
}

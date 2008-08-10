namespace MrWallpaper.controls {
    partial class Designer {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBoxPlus1 = new MrWallpaper.GroupBoxPlus();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxPlus4 = new MrWallpaper.GroupBoxPlus();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBoxPlus3 = new MrWallpaper.GroupBoxPlus();
            this.button5 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scrHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.scrWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxPlus2 = new MrWallpaper.GroupBoxPlus();
            this.pictureBoxPlus1 = new ImageBrowser2.PictureBoxPlus();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel4.SuspendLayout();
            this.groupBoxPlus1.SuspendLayout();
            this.groupBoxPlus4.SuspendLayout();
            this.groupBoxPlus3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrHeight)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrWidth)).BeginInit();
            this.groupBoxPlus2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlus1)).BeginInit();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "JPG|*.jpg";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBoxPlus1);
            this.panel4.Controls.Add(this.groupBoxPlus4);
            this.panel4.Controls.Add(this.groupBoxPlus3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(691, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(168, 517);
            this.panel4.TabIndex = 2;
            // 
            // groupBoxPlus1
            // 
            this.groupBoxPlus1.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxPlus1.BotBgGradient = System.Drawing.Color.MediumSeaGreen;
            this.groupBoxPlus1.BotLbGradient = System.Drawing.Color.White;
            this.groupBoxPlus1.Controls.Add(this.listBox1);
            this.groupBoxPlus1.Controls.Add(this.button2);
            this.groupBoxPlus1.Controls.Add(this.button1);
            this.groupBoxPlus1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPlus1.Location = new System.Drawing.Point(0, 336);
            this.groupBoxPlus1.Name = "groupBoxPlus1";
            this.groupBoxPlus1.Size = new System.Drawing.Size(168, 181);
            this.groupBoxPlus1.TabIndex = 0;
            this.groupBoxPlus1.TabStop = false;
            this.groupBoxPlus1.Text = "Files";
            this.groupBoxPlus1.TopBgGradient = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(200)))), ((int)(((byte)(164)))));
            this.groupBoxPlus1.TopLbGradient = System.Drawing.Color.LightGreen;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(5, 48);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(158, 95);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button2.Location = new System.Drawing.Point(5, 153);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(158, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Open";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(5, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBoxPlus4
            // 
            this.groupBoxPlus4.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxPlus4.BotBgGradient = System.Drawing.Color.MediumSeaGreen;
            this.groupBoxPlus4.BotLbGradient = System.Drawing.Color.White;
            this.groupBoxPlus4.Controls.Add(this.button4);
            this.groupBoxPlus4.Controls.Add(this.button3);
            this.groupBoxPlus4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPlus4.Location = new System.Drawing.Point(0, 231);
            this.groupBoxPlus4.Name = "groupBoxPlus4";
            this.groupBoxPlus4.Size = new System.Drawing.Size(168, 105);
            this.groupBoxPlus4.TabIndex = 2;
            this.groupBoxPlus4.TabStop = false;
            this.groupBoxPlus4.Text = "Selection Options";
            this.groupBoxPlus4.TopBgGradient = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(200)))), ((int)(((byte)(164)))));
            this.groupBoxPlus4.TopLbGradient = System.Drawing.Color.LightGreen;
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Top;
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(5, 48);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(158, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Select None";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(5, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(158, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Select Maximum";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // groupBoxPlus3
            // 
            this.groupBoxPlus3.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxPlus3.BotBgGradient = System.Drawing.Color.MediumSeaGreen;
            this.groupBoxPlus3.BotLbGradient = System.Drawing.Color.White;
            this.groupBoxPlus3.Controls.Add(this.button5);
            this.groupBoxPlus3.Controls.Add(this.panel1);
            this.groupBoxPlus3.Controls.Add(this.panel2);
            this.groupBoxPlus3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPlus3.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPlus3.Name = "groupBoxPlus3";
            this.groupBoxPlus3.Size = new System.Drawing.Size(168, 231);
            this.groupBoxPlus3.TabIndex = 1;
            this.groupBoxPlus3.TabStop = false;
            this.groupBoxPlus3.Text = "Image settings";
            this.groupBoxPlus3.TopBgGradient = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(200)))), ((int)(((byte)(164)))));
            this.groupBoxPlus3.TopLbGradient = System.Drawing.Color.LightGreen;
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Top;
            this.button5.Location = new System.Drawing.Point(5, 65);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(158, 23);
            this.button5.TabIndex = 16;
            this.button5.Text = "Save Image";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.scrHeight);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 20);
            this.panel1.TabIndex = 14;
            // 
            // scrHeight
            // 
            this.scrHeight.Dock = System.Windows.Forms.DockStyle.Left;
            this.scrHeight.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.scrHeight.Location = new System.Drawing.Point(50, 0);
            this.scrHeight.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.scrHeight.Name = "scrHeight";
            this.scrHeight.Size = new System.Drawing.Size(62, 20);
            this.scrHeight.TabIndex = 11;
            this.scrHeight.ValueChanged += new System.EventHandler(this.screenSizeChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Height:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.scrWidth);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 20);
            this.panel2.TabIndex = 15;
            // 
            // scrWidth
            // 
            this.scrWidth.Dock = System.Windows.Forms.DockStyle.Left;
            this.scrWidth.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.scrWidth.Location = new System.Drawing.Point(50, 0);
            this.scrWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.scrWidth.Name = "scrWidth";
            this.scrWidth.Size = new System.Drawing.Size(62, 20);
            this.scrWidth.TabIndex = 9;
            this.scrWidth.ValueChanged += new System.EventHandler(this.screenSizeChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Width:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBoxPlus2
            // 
            this.groupBoxPlus2.BotBgGradient = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.groupBoxPlus2.BotLbGradient = System.Drawing.Color.White;
            this.groupBoxPlus2.Controls.Add(this.pictureBoxPlus1);
            this.groupBoxPlus2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPlus2.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPlus2.Name = "groupBoxPlus2";
            this.groupBoxPlus2.Size = new System.Drawing.Size(691, 517);
            this.groupBoxPlus2.TabIndex = 1;
            this.groupBoxPlus2.TabStop = false;
            this.groupBoxPlus2.Text = "Wallpaper Preview";
            this.groupBoxPlus2.TopBgGradient = System.Drawing.Color.PaleGreen;
            this.groupBoxPlus2.TopLbGradient = System.Drawing.Color.LightGreen;
            // 
            // pictureBoxPlus1
            // 
            this.pictureBoxPlus1.AspectRatio = 0;
            this.pictureBoxPlus1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPlus1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPlus1.Location = new System.Drawing.Point(5, 25);
            this.pictureBoxPlus1.Name = "pictureBoxPlus1";
            this.pictureBoxPlus1.Original = null;
            this.pictureBoxPlus1.Size = new System.Drawing.Size(681, 487);
            this.pictureBoxPlus1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxPlus1.TabIndex = 0;
            this.pictureBoxPlus1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Designer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGreen;
            this.Controls.Add(this.groupBoxPlus2);
            this.Controls.Add(this.panel4);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Designer";
            this.Size = new System.Drawing.Size(859, 517);
            this.Load += new System.EventHandler(this.Designer_Load);
            this.panel4.ResumeLayout(false);
            this.groupBoxPlus1.ResumeLayout(false);
            this.groupBoxPlus4.ResumeLayout(false);
            this.groupBoxPlus3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scrHeight)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scrWidth)).EndInit();
            this.groupBoxPlus2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlus1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBoxPlus groupBoxPlus1;
        private GroupBoxPlus groupBoxPlus2;
        private ImageBrowser2.PictureBoxPlus pictureBoxPlus1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel4;
        private GroupBoxPlus groupBoxPlus3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown scrHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown scrWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private GroupBoxPlus groupBoxPlus4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
    }
}

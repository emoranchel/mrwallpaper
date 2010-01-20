namespace ImageBrowser2 {
    partial class MainBrowser {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainBrowser));
          this.flatTabControl1 = new FlatTabControl.FlatTabControl();
          this.tabPage3 = new System.Windows.Forms.TabPage();
          this.browser1 = new MrWallpaper.controls.Browser();
          this.tabPage4 = new System.Windows.Forms.TabPage();
          this.designer1 = new MrWallpaper.controls.Designer();
          this.flatTabControl1.SuspendLayout();
          this.tabPage3.SuspendLayout();
          this.tabPage4.SuspendLayout();
          this.SuspendLayout();
          // 
          // flatTabControl1
          // 
          this.flatTabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
          this.flatTabControl1.Controls.Add(this.tabPage3);
          this.flatTabControl1.Controls.Add(this.tabPage4);
          this.flatTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.flatTabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.flatTabControl1.Location = new System.Drawing.Point(0, 0);
          this.flatTabControl1.myBackColor = System.Drawing.Color.SlateGray;
          this.flatTabControl1.Name = "flatTabControl1";
          this.flatTabControl1.Padding = new System.Drawing.Point(20, 0);
          this.flatTabControl1.SelectedIndex = 0;
          this.flatTabControl1.Size = new System.Drawing.Size(863, 512);
          this.flatTabControl1.TabIndex = 0;
          // 
          // tabPage3
          // 
          this.tabPage3.BackColor = System.Drawing.Color.LightSteelBlue;
          this.tabPage3.Controls.Add(this.browser1);
          this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.tabPage3.Location = new System.Drawing.Point(4, 4);
          this.tabPage3.Name = "tabPage3";
          this.tabPage3.Padding = new System.Windows.Forms.Padding(5);
          this.tabPage3.Size = new System.Drawing.Size(855, 485);
          this.tabPage3.TabIndex = 0;
          this.tabPage3.Text = "Browser";
          // 
          // browser1
          // 
          this.browser1.BackColor = System.Drawing.Color.LightSteelBlue;
          this.browser1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.browser1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.browser1.ForeColor = System.Drawing.Color.Black;
          this.browser1.Location = new System.Drawing.Point(5, 5);
          this.browser1.Name = "browser1";
          this.browser1.Size = new System.Drawing.Size(845, 475);
          this.browser1.TabIndex = 0;
          // 
          // tabPage4
          // 
          this.tabPage4.BackColor = System.Drawing.Color.PaleGreen;
          this.tabPage4.Controls.Add(this.designer1);
          this.tabPage4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.tabPage4.Location = new System.Drawing.Point(4, 4);
          this.tabPage4.Name = "tabPage4";
          this.tabPage4.Padding = new System.Windows.Forms.Padding(6);
          this.tabPage4.Size = new System.Drawing.Size(855, 485);
          this.tabPage4.TabIndex = 1;
          this.tabPage4.Text = "Wallpaper Designer";
          // 
          // designer1
          // 
          this.designer1.BackColor = System.Drawing.Color.PaleGreen;
          this.designer1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.designer1.ForeColor = System.Drawing.Color.Black;
          this.designer1.Location = new System.Drawing.Point(6, 6);
          this.designer1.Name = "designer1";
          this.designer1.Size = new System.Drawing.Size(180, 61);
          this.designer1.TabIndex = 0;
          // 
          // MainBrowser
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.SlateGray;
          this.ClientSize = new System.Drawing.Size(863, 512);
          this.Controls.Add(this.flatTabControl1);
          this.ForeColor = System.Drawing.Color.Black;
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.Name = "MainBrowser";
          this.Text = "Mr. Wallpaper";
          this.flatTabControl1.ResumeLayout(false);
          this.tabPage3.ResumeLayout(false);
          this.tabPage4.ResumeLayout(false);
          this.ResumeLayout(false);

        }

        #endregion

        private FlatTabControl.FlatTabControl flatTabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private MrWallpaper.controls.Browser browser1;
        private MrWallpaper.controls.Designer designer1;
    }
}


namespace blkEditor
{
    partial class frmSplash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplash));
            this.tlpSplash = new System.Windows.Forms.TableLayoutPanel();
            this.tlpSplashCell1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSplashName = new System.Windows.Forms.Label();
            this.lblSplashVersion = new System.Windows.Forms.Label();
            this.picSplashIcon = new System.Windows.Forms.PictureBox();
            this.lblSplashInfo = new System.Windows.Forms.Label();
            this.tlpSplash.SuspendLayout();
            this.tlpSplashCell1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSplashIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpSplash
            // 
            this.tlpSplash.BackColor = System.Drawing.Color.Transparent;
            this.tlpSplash.BackgroundImage = global::blkEditor.Properties.Resources.splash;
            this.tlpSplash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlpSplash.ColumnCount = 1;
            this.tlpSplash.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSplash.Controls.Add(this.tlpSplashCell1, 0, 0);
            this.tlpSplash.Controls.Add(this.lblSplashInfo, 0, 1);
            this.tlpSplash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSplash.Location = new System.Drawing.Point(0, 0);
            this.tlpSplash.Name = "tlpSplash";
            this.tlpSplash.RowCount = 2;
            this.tlpSplash.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSplash.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpSplash.Size = new System.Drawing.Size(460, 330);
            this.tlpSplash.TabIndex = 0;
            // 
            // tlpSplashCell1
            // 
            this.tlpSplashCell1.ColumnCount = 4;
            this.tlpSplashCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSplashCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tlpSplashCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSplashCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSplashCell1.Controls.Add(this.lblSplashName, 2, 2);
            this.tlpSplashCell1.Controls.Add(this.lblSplashVersion, 2, 4);
            this.tlpSplashCell1.Controls.Add(this.picSplashIcon, 1, 1);
            this.tlpSplashCell1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSplashCell1.Location = new System.Drawing.Point(3, 3);
            this.tlpSplashCell1.Name = "tlpSplashCell1";
            this.tlpSplashCell1.RowCount = 8;
            this.tlpSplashCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSplashCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpSplashCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpSplashCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tlpSplashCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpSplashCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpSplashCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSplashCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSplashCell1.Size = new System.Drawing.Size(454, 300);
            this.tlpSplashCell1.TabIndex = 0;
            // 
            // lblSplashName
            // 
            this.tlpSplashCell1.SetColumnSpan(this.lblSplashName, 2);
            this.lblSplashName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSplashName.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSplashName.ForeColor = System.Drawing.Color.Yellow;
            this.lblSplashName.Location = new System.Drawing.Point(119, 30);
            this.lblSplashName.Name = "lblSplashName";
            this.lblSplashName.Size = new System.Drawing.Size(332, 40);
            this.lblSplashName.TabIndex = 0;
            this.lblSplashName.Text = "BanLinhKien Editor";
            this.lblSplashName.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblSplashVersion
            // 
            this.tlpSplashCell1.SetColumnSpan(this.lblSplashVersion, 2);
            this.lblSplashVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSplashVersion.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSplashVersion.ForeColor = System.Drawing.Color.Gold;
            this.lblSplashVersion.Location = new System.Drawing.Point(119, 82);
            this.lblSplashVersion.Name = "lblSplashVersion";
            this.lblSplashVersion.Size = new System.Drawing.Size(332, 30);
            this.lblSplashVersion.TabIndex = 1;
            this.lblSplashVersion.Text = "Phiên bản";
            this.lblSplashVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picSplashIcon
            // 
            this.picSplashIcon.BackgroundImage = global::blkEditor.Properties.Resources.splash_icon;
            this.picSplashIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSplashIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSplashIcon.Location = new System.Drawing.Point(23, 23);
            this.picSplashIcon.Name = "picSplashIcon";
            this.tlpSplashCell1.SetRowSpan(this.picSplashIcon, 5);
            this.picSplashIcon.Size = new System.Drawing.Size(90, 92);
            this.picSplashIcon.TabIndex = 1;
            this.picSplashIcon.TabStop = false;
            // 
            // lblSplashInfo
            // 
            this.lblSplashInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSplashInfo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSplashInfo.ForeColor = System.Drawing.Color.DarkGray;
            this.lblSplashInfo.Location = new System.Drawing.Point(3, 306);
            this.lblSplashInfo.Name = "lblSplashInfo";
            this.lblSplashInfo.Size = new System.Drawing.Size(454, 24);
            this.lblSplashInfo.TabIndex = 1;
            this.lblSplashInfo.Text = "Bản quyền © 2019 - 2021 Vũ Phát";
            this.lblSplashInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 330);
            this.Controls.Add(this.tlpSplash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(460, 330);
            this.Name = "frmSplash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSplash";
            this.Load += new System.EventHandler(this.frmSplash_Load);
            this.tlpSplash.ResumeLayout(false);
            this.tlpSplashCell1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSplashIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpSplash;
        private System.Windows.Forms.Label lblSplashInfo;
        private System.Windows.Forms.PictureBox picSplashIcon;
        private System.Windows.Forms.TableLayoutPanel tlpSplashCell1;
        private System.Windows.Forms.Label lblSplashName;
        private System.Windows.Forms.Label lblSplashVersion;
    }
}
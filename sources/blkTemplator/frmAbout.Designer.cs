namespace blkTemplator
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.tlpAbout = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblAbout = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pnlBorder = new System.Windows.Forms.Panel();
            this.picAbout = new System.Windows.Forms.PictureBox();
            this.ttpAbout = new System.Windows.Forms.ToolTip();
            this.tlpAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAbout)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpAbout
            // 
            this.tlpAbout.ColumnCount = 3;
            this.tlpAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpAbout.Controls.Add(this.btnOK, 2, 3);
            this.tlpAbout.Controls.Add(this.lblAbout, 1, 0);
            this.tlpAbout.Controls.Add(this.lblInfo, 1, 2);
            this.tlpAbout.Controls.Add(this.pnlBorder, 1, 1);
            this.tlpAbout.Controls.Add(this.picAbout, 0, 0);
            this.tlpAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAbout.Location = new System.Drawing.Point(0, 0);
            this.tlpAbout.Name = "tlpAbout";
            this.tlpAbout.Padding = new System.Windows.Forms.Padding(4);
            this.tlpAbout.RowCount = 4;
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tlpAbout.Size = new System.Drawing.Size(334, 236);
            this.tlpAbout.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(233, 204);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.ttpAbout.SetToolTip(this.btnOK, "OK để hoàn tất");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.tlpAbout.SetColumnSpan(this.lblAbout, 2);
            this.lblAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAbout.Location = new System.Drawing.Point(49, 4);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(278, 42);
            this.lblAbout.TabIndex = 1;
            this.lblAbout.Text = "BanLinhKien Templator\r\nPhiên bản 1.0\r\nBản quyền © 2019 - 2021 Vũ Phát";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.tlpAbout.SetColumnSpan(this.lblInfo, 2);
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.Location = new System.Drawing.Point(49, 56);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(278, 145);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = resources.GetString("lblInfo.Text");
            // 
            // pnlBorder
            // 
            this.pnlBorder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tlpAbout.SetColumnSpan(this.pnlBorder, 2);
            this.pnlBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBorder.Enabled = false;
            this.pnlBorder.Location = new System.Drawing.Point(49, 49);
            this.pnlBorder.Name = "pnlBorder";
            this.pnlBorder.Size = new System.Drawing.Size(278, 4);
            this.pnlBorder.TabIndex = 3;
            // 
            // picAbout
            // 
            this.picAbout.BackgroundImage = global::blkTemplator.Properties.Resources.about_icon;
            this.picAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picAbout.Location = new System.Drawing.Point(7, 7);
            this.picAbout.Name = "picAbout";
            this.picAbout.Size = new System.Drawing.Size(36, 36);
            this.picAbout.TabIndex = 0;
            this.picAbout.TabStop = false;
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 236);
            this.Controls.Add(this.tlpAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 275);
            this.Name = "frmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông tin BanLinhKien Templator";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.tlpAbout.ResumeLayout(false);
            this.tlpAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAbout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAbout;
        private System.Windows.Forms.PictureBox picAbout;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlBorder;
        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ToolTip ttpAbout;
    }
}
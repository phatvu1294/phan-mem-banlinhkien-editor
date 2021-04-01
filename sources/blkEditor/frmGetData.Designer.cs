namespace blkEditor
{
    partial class frmGetData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGetData));
            this.tlpGetLink = new System.Windows.Forms.TableLayoutPanel();
            this.txtLinkAddressName = new System.Windows.Forms.TextBox();
            this.btnGetDataDone = new System.Windows.Forms.Button();
            this.lblLinkAddressName = new System.Windows.Forms.Label();
            this.chkGetImageProfile = new System.Windows.Forms.CheckBox();
            this.chkAddToListImage = new System.Windows.Forms.CheckBox();
            this.lblGetDataNote = new System.Windows.Forms.Label();
            this.ttpGetLink = new System.Windows.Forms.ToolTip();
            this.tlpGetLink.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpGetLink
            // 
            this.tlpGetLink.ColumnCount = 3;
            this.tlpGetLink.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpGetLink.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGetLink.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGetLink.Controls.Add(this.txtLinkAddressName, 1, 0);
            this.tlpGetLink.Controls.Add(this.btnGetDataDone, 2, 0);
            this.tlpGetLink.Controls.Add(this.lblLinkAddressName, 0, 0);
            this.tlpGetLink.Controls.Add(this.chkGetImageProfile, 1, 1);
            this.tlpGetLink.Controls.Add(this.chkAddToListImage, 1, 2);
            this.tlpGetLink.Controls.Add(this.lblGetDataNote, 1, 3);
            this.tlpGetLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGetLink.Location = new System.Drawing.Point(0, 0);
            this.tlpGetLink.Name = "tlpGetLink";
            this.tlpGetLink.RowCount = 4;
            this.tlpGetLink.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGetLink.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tlpGetLink.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tlpGetLink.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGetLink.Size = new System.Drawing.Size(624, 141);
            this.tlpGetLink.TabIndex = 0;
            // 
            // txtLinkAddressName
            // 
            this.txtLinkAddressName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLinkAddressName.Location = new System.Drawing.Point(83, 5);
            this.txtLinkAddressName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 4);
            this.txtLinkAddressName.Name = "txtLinkAddressName";
            this.txtLinkAddressName.Size = new System.Drawing.Size(508, 20);
            this.txtLinkAddressName.TabIndex = 0;
            this.ttpGetLink.SetToolTip(this.txtLinkAddressName, "Nhập hoặc dán link cần lấy dữ liệu và nhấn Enter để hoàn tất");
            this.txtLinkAddressName.WordWrap = false;
            this.txtLinkAddressName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLinkAddressName_KeyPress);
            // 
            // btnGetDataDone
            // 
            this.btnGetDataDone.BackgroundImage = global::blkEditor.Properties.Resources.done;
            this.btnGetDataDone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetDataDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetDataDone.Location = new System.Drawing.Point(597, 3);
            this.btnGetDataDone.Name = "btnGetDataDone";
            this.btnGetDataDone.Size = new System.Drawing.Size(24, 24);
            this.btnGetDataDone.TabIndex = 1;
            this.ttpGetLink.SetToolTip(this.btnGetDataDone, "Hoàn thành lấy dữ liệu");
            this.btnGetDataDone.UseVisualStyleBackColor = true;
            this.btnGetDataDone.Click += new System.EventHandler(this.btnGetDataDone_Click);
            // 
            // lblLinkAddressName
            // 
            this.lblLinkAddressName.AutoSize = true;
            this.lblLinkAddressName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLinkAddressName.Location = new System.Drawing.Point(3, 0);
            this.lblLinkAddressName.Name = "lblLinkAddressName";
            this.lblLinkAddressName.Size = new System.Drawing.Size(74, 30);
            this.lblLinkAddressName.TabIndex = 2;
            this.lblLinkAddressName.Text = "Link/Tên SP:";
            this.lblLinkAddressName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkGetImageProfile
            // 
            this.chkGetImageProfile.AutoSize = true;
            this.chkGetImageProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkGetImageProfile.Location = new System.Drawing.Point(83, 33);
            this.chkGetImageProfile.Name = "chkGetImageProfile";
            this.chkGetImageProfile.Size = new System.Drawing.Size(508, 17);
            this.chkGetImageProfile.TabIndex = 3;
            this.chkGetImageProfile.Text = "Lấy ảnh sản phẩm từ hình đại diện trên Web";
            this.ttpGetLink.SetToolTip(this.chkGetImageProfile, "Lấy ảnh sản phẩm từ hình đại diện trên Web");
            this.chkGetImageProfile.UseVisualStyleBackColor = true;
            this.chkGetImageProfile.Click += new System.EventHandler(this.chkGetImageProfile_Click);
            this.chkGetImageProfile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLinkAddressName_KeyPress);
            // 
            // chkAddToListImage
            // 
            this.chkAddToListImage.AutoSize = true;
            this.chkAddToListImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAddToListImage.Location = new System.Drawing.Point(83, 56);
            this.chkAddToListImage.Name = "chkAddToListImage";
            this.chkAddToListImage.Size = new System.Drawing.Size(508, 17);
            this.chkAddToListImage.TabIndex = 4;
            this.chkAddToListImage.Text = "Thêm ảnh sản phẩm vào danh sách chỉnh sửa";
            this.ttpGetLink.SetToolTip(this.chkAddToListImage, "Thêm ảnh sản phẩm vào danh sách chỉnh sửa");
            this.chkAddToListImage.UseVisualStyleBackColor = true;
            this.chkAddToListImage.Click += new System.EventHandler(this.chkAddToListImage_Click);
            this.chkAddToListImage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLinkAddressName_KeyPress);
            // 
            // lblGetDataNote
            // 
            this.lblGetDataNote.AutoSize = true;
            this.tlpGetLink.SetColumnSpan(this.lblGetDataNote, 2);
            this.lblGetDataNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGetDataNote.Location = new System.Drawing.Point(83, 76);
            this.lblGetDataNote.Name = "lblGetDataNote";
            this.lblGetDataNote.Size = new System.Drawing.Size(538, 65);
            this.lblGetDataNote.TabIndex = 5;
            this.lblGetDataNote.Text = resources.GetString("lblGetDataNote.Text");
            this.lblGetDataNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmGetData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 141);
            this.Controls.Add(this.tlpGetLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 160);
            this.Name = "frmGetData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BanLinhKien GetData";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmGetLink_FormClosed);
            this.Load += new System.EventHandler(this.frmGetLink_Load);
            this.Shown += new System.EventHandler(this.frmGetLink_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLinkAddressName_KeyPress);
            this.tlpGetLink.ResumeLayout(false);
            this.tlpGetLink.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpGetLink;
        private System.Windows.Forms.TextBox txtLinkAddressName;
        private System.Windows.Forms.Button btnGetDataDone;
        private System.Windows.Forms.Label lblLinkAddressName;
        private System.Windows.Forms.Label lblGetDataNote;
        private System.Windows.Forms.ToolTip ttpGetLink;
        public System.Windows.Forms.CheckBox chkAddToListImage;
        public System.Windows.Forms.CheckBox chkGetImageProfile;
    }
}
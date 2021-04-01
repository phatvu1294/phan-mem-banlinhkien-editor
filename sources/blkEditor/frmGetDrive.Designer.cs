namespace blkEditor
{
    partial class frmGetDrive
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGetDrive));
            this.tlpGetDrive = new System.Windows.Forms.TableLayoutPanel();
            this.tlpGetDriveCell1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFilterFileDriveListByName = new System.Windows.Forms.Button();
            this.txtFilterFileDriveByName = new System.Windows.Forms.TextBox();
            this.lblFilterImageDriveByName = new System.Windows.Forms.Label();
            this.cbxFileDriveListOrderBy = new System.Windows.Forms.ComboBox();
            this.btnImageDriveListDirOrderBy = new System.Windows.Forms.Button();
            this.lblFileDriveListOrderBy = new System.Windows.Forms.Label();
            this.btnGetDriveDone = new System.Windows.Forms.Button();
            this.lsvFileDriveList = new System.Windows.Forms.ListView();
            this.stsGetDrive = new System.Windows.Forms.StatusStrip();
            this.tspLoadDownloadFileDriveList = new System.Windows.Forms.ToolStripProgressBar();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.bgwLoadImageDriveList = new System.ComponentModel.BackgroundWorker();
            this.ttpGetDrive = new System.Windows.Forms.ToolTip(this.components);
            this.tmrHideMessage = new System.Windows.Forms.Timer(this.components);
            this.bgwDownloadImageDrive = new System.ComponentModel.BackgroundWorker();
            this.tlpGetDrive.SuspendLayout();
            this.tlpGetDriveCell1.SuspendLayout();
            this.stsGetDrive.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpGetDrive
            // 
            this.tlpGetDrive.ColumnCount = 1;
            this.tlpGetDrive.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGetDrive.Controls.Add(this.tlpGetDriveCell1, 0, 0);
            this.tlpGetDrive.Controls.Add(this.lsvFileDriveList, 0, 1);
            this.tlpGetDrive.Controls.Add(this.stsGetDrive, 0, 2);
            this.tlpGetDrive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGetDrive.Location = new System.Drawing.Point(0, 0);
            this.tlpGetDrive.Name = "tlpGetDrive";
            this.tlpGetDrive.RowCount = 3;
            this.tlpGetDrive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpGetDrive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGetDrive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpGetDrive.Size = new System.Drawing.Size(784, 561);
            this.tlpGetDrive.TabIndex = 0;
            // 
            // tlpGetDriveCell1
            // 
            this.tlpGetDriveCell1.ColumnCount = 8;
            this.tlpGetDriveCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpGetDriveCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGetDriveCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpGetDriveCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpGetDriveCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpGetDriveCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpGetDriveCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGetDriveCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tlpGetDriveCell1.Controls.Add(this.btnFilterFileDriveListByName, 0, 0);
            this.tlpGetDriveCell1.Controls.Add(this.txtFilterFileDriveByName, 2, 1);
            this.tlpGetDriveCell1.Controls.Add(this.lblFilterImageDriveByName, 2, 0);
            this.tlpGetDriveCell1.Controls.Add(this.cbxFileDriveListOrderBy, 4, 1);
            this.tlpGetDriveCell1.Controls.Add(this.lblFileDriveListOrderBy, 4, 0);
            this.tlpGetDriveCell1.Controls.Add(this.btnImageDriveListDirOrderBy, 5, 1);
            this.tlpGetDriveCell1.Controls.Add(this.btnGetDriveDone, 7, 0);
            this.tlpGetDriveCell1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGetDriveCell1.Location = new System.Drawing.Point(3, 3);
            this.tlpGetDriveCell1.Name = "tlpGetDriveCell1";
            this.tlpGetDriveCell1.RowCount = 2;
            this.tlpGetDriveCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tlpGetDriveCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGetDriveCell1.Size = new System.Drawing.Size(778, 44);
            this.tlpGetDriveCell1.TabIndex = 0;
            // 
            // btnFilterFileDriveListByName
            // 
            this.btnFilterFileDriveListByName.BackgroundImage = global::blkEditor.Properties.Resources.filter;
            this.btnFilterFileDriveListByName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFilterFileDriveListByName.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFilterFileDriveListByName.Location = new System.Drawing.Point(3, 3);
            this.btnFilterFileDriveListByName.Name = "btnFilterFileDriveListByName";
            this.tlpGetDriveCell1.SetRowSpan(this.btnFilterFileDriveListByName, 2);
            this.btnFilterFileDriveListByName.Size = new System.Drawing.Size(32, 32);
            this.btnFilterFileDriveListByName.TabIndex = 0;
            this.ttpGetDrive.SetToolTip(this.btnFilterFileDriveListByName, "Lọc danh sách ảnh Drive theo tên SP");
            this.btnFilterFileDriveListByName.UseVisualStyleBackColor = true;
            this.btnFilterFileDriveListByName.Click += new System.EventHandler(this.btnFilterFileDriveListByName_Click);
            // 
            // txtFilterFileDriveByName
            // 
            this.txtFilterFileDriveByName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilterFileDriveByName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterFileDriveByName.Location = new System.Drawing.Point(103, 20);
            this.txtFilterFileDriveByName.Name = "txtFilterFileDriveByName";
            this.txtFilterFileDriveByName.Size = new System.Drawing.Size(294, 21);
            this.txtFilterFileDriveByName.TabIndex = 1;
            this.ttpGetDrive.SetToolTip(this.txtFilterFileDriveByName, "Nhập tên SP cần lọc danh sách ảnh và nhấn Enter để hoàn tất");
            this.txtFilterFileDriveByName.TextChanged += new System.EventHandler(this.txtFilterFileDriveByName_TextChanged);
            this.txtFilterFileDriveByName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilterFileDriveByProductName_KeyDown);
            // 
            // lblFilterImageDriveByName
            // 
            this.lblFilterImageDriveByName.AutoSize = true;
            this.lblFilterImageDriveByName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFilterImageDriveByName.Location = new System.Drawing.Point(103, 4);
            this.lblFilterImageDriveByName.Name = "lblFilterImageDriveByName";
            this.lblFilterImageDriveByName.Size = new System.Drawing.Size(294, 13);
            this.lblFilterImageDriveByName.TabIndex = 2;
            this.lblFilterImageDriveByName.Text = "Lọc danh sách ảnh theo tên SP:";
            // 
            // cbxFileDriveListOrderBy
            // 
            this.cbxFileDriveListOrderBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxFileDriveListOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFileDriveListOrderBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFileDriveListOrderBy.FormattingEnabled = true;
            this.cbxFileDriveListOrderBy.Location = new System.Drawing.Point(503, 19);
            this.cbxFileDriveListOrderBy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxFileDriveListOrderBy.Name = "cbxFileDriveListOrderBy";
            this.cbxFileDriveListOrderBy.Size = new System.Drawing.Size(144, 23);
            this.cbxFileDriveListOrderBy.TabIndex = 3;
            this.ttpGetDrive.SetToolTip(this.cbxFileDriveListOrderBy, "Chọn kiểu sắp xếp danh sách");
            this.cbxFileDriveListOrderBy.SelectedIndexChanged += new System.EventHandler(this.cbxFileDriveListOrderBy_SelectedIndexChanged);
            // 
            // btnImageDriveListDirOrderBy
            // 
            this.btnImageDriveListDirOrderBy.BackgroundImage = global::blkEditor.Properties.Resources.sort_down;
            this.btnImageDriveListDirOrderBy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnImageDriveListDirOrderBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImageDriveListDirOrderBy.Location = new System.Drawing.Point(652, 19);
            this.btnImageDriveListDirOrderBy.Margin = new System.Windows.Forms.Padding(2);
            this.btnImageDriveListDirOrderBy.Name = "btnImageDriveListDirOrderBy";
            this.btnImageDriveListDirOrderBy.Size = new System.Drawing.Size(23, 23);
            this.btnImageDriveListDirOrderBy.TabIndex = 5;
            this.ttpGetDrive.SetToolTip(this.btnImageDriveListDirOrderBy, "Đảo hướng sắp xếp");
            this.btnImageDriveListDirOrderBy.UseVisualStyleBackColor = true;
            this.btnImageDriveListDirOrderBy.Click += new System.EventHandler(this.btnImageDriveListDirOrderBy_Click);
            // 
            // lblFileDriveListOrderBy
            // 
            this.lblFileDriveListOrderBy.AutoSize = true;
            this.lblFileDriveListOrderBy.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFileDriveListOrderBy.Location = new System.Drawing.Point(503, 4);
            this.lblFileDriveListOrderBy.Name = "lblFileDriveListOrderBy";
            this.lblFileDriveListOrderBy.Size = new System.Drawing.Size(144, 13);
            this.lblFileDriveListOrderBy.TabIndex = 4;
            this.lblFileDriveListOrderBy.Text = "Sắp xếp theo:";
            // 
            // btnGetDriveDone
            // 
            this.btnGetDriveDone.BackgroundImage = global::blkEditor.Properties.Resources.done;
            this.btnGetDriveDone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetDriveDone.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGetDriveDone.Location = new System.Drawing.Point(742, 3);
            this.btnGetDriveDone.Name = "btnGetDriveDone";
            this.tlpGetDriveCell1.SetRowSpan(this.btnGetDriveDone, 2);
            this.btnGetDriveDone.Size = new System.Drawing.Size(33, 32);
            this.btnGetDriveDone.TabIndex = 6;
            this.ttpGetDrive.SetToolTip(this.btnGetDriveDone, "Hoàn thành lựa chọn");
            this.btnGetDriveDone.UseVisualStyleBackColor = true;
            this.btnGetDriveDone.Click += new System.EventHandler(this.btnGetDriveDone_Click);
            // 
            // lsvFileDriveList
            // 
            this.lsvFileDriveList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvFileDriveList.Location = new System.Drawing.Point(3, 53);
            this.lsvFileDriveList.Name = "lsvFileDriveList";
            this.lsvFileDriveList.Size = new System.Drawing.Size(778, 480);
            this.lsvFileDriveList.TabIndex = 1;
            this.lsvFileDriveList.UseCompatibleStateImageBehavior = false;
            // 
            // stsGetDrive
            // 
            this.stsGetDrive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stsGetDrive.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspLoadDownloadFileDriveList,
            this.tslStatus});
            this.stsGetDrive.Location = new System.Drawing.Point(0, 536);
            this.stsGetDrive.Name = "stsGetDrive";
            this.stsGetDrive.Size = new System.Drawing.Size(784, 25);
            this.stsGetDrive.TabIndex = 2;
            this.stsGetDrive.Text = "stsGetDrive";
            // 
            // tspLoadDownloadFileDriveList
            // 
            this.tspLoadDownloadFileDriveList.Name = "tspLoadDownloadFileDriveList";
            this.tspLoadDownloadFileDriveList.Size = new System.Drawing.Size(100, 19);
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(54, 20);
            this.tslStatus.Text = "Sẵn sàng";
            // 
            // bgwLoadImageDriveList
            // 
            this.bgwLoadImageDriveList.WorkerReportsProgress = true;
            this.bgwLoadImageDriveList.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoadImageDriveList_DoWork);
            this.bgwLoadImageDriveList.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwLoadImageDriveList_ProgressChanged);
            this.bgwLoadImageDriveList.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLoadImageDriveList_RunWorkerCompleted);
            // 
            // tmrHideMessage
            // 
            this.tmrHideMessage.Interval = 1500;
            this.tmrHideMessage.Tick += new System.EventHandler(this.tmrHideMessage_Tick);
            // 
            // bgwDownloadImageDrive
            // 
            this.bgwDownloadImageDrive.WorkerReportsProgress = true;
            this.bgwDownloadImageDrive.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDownloadImageDrive_DoWork);
            this.bgwDownloadImageDrive.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwDownloadImageDrive_ProgressChanged);
            this.bgwDownloadImageDrive.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwDownloadImageDrive_RunWorkerCompleted);
            // 
            // frmGetDrive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tlpGetDrive);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmGetDrive";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BanLinhKien GetDrive";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmGetDrive_FormClosed);
            this.Load += new System.EventHandler(this.frmGetDrive_Load);
            this.Shown += new System.EventHandler(this.frmGetDrive_Shown);
            this.tlpGetDrive.ResumeLayout(false);
            this.tlpGetDrive.PerformLayout();
            this.tlpGetDriveCell1.ResumeLayout(false);
            this.tlpGetDriveCell1.PerformLayout();
            this.stsGetDrive.ResumeLayout(false);
            this.stsGetDrive.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpGetDrive;
        private System.Windows.Forms.ListView lsvFileDriveList;
        private System.ComponentModel.BackgroundWorker bgwLoadImageDriveList;
        private System.Windows.Forms.StatusStrip stsGetDrive;
        private System.Windows.Forms.TableLayoutPanel tlpGetDriveCell1;
        private System.Windows.Forms.Button btnGetDriveDone;
        private System.Windows.Forms.ToolTip ttpGetDrive;
        private System.Windows.Forms.ToolStripProgressBar tspLoadDownloadFileDriveList;
        private System.Windows.Forms.Timer tmrHideMessage;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.Label lblFilterImageDriveByName;
        public System.Windows.Forms.TextBox txtFilterFileDriveByName;
        private System.Windows.Forms.ComboBox cbxFileDriveListOrderBy;
        private System.Windows.Forms.Label lblFileDriveListOrderBy;
        private System.Windows.Forms.Button btnImageDriveListDirOrderBy;
        private System.ComponentModel.BackgroundWorker bgwDownloadImageDrive;
        public System.Windows.Forms.Button btnFilterFileDriveListByName;
    }
}
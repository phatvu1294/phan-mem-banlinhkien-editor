namespace blkTemplator
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mniOpenTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSaveTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSaveTemplateAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mniCloseTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mniGetTemplateFromDrive = new System.Windows.Forms.ToolStripMenuItem();
            this.mniUploadTemplateToDrive = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mniImportFormExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mniExportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mniSyncCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFileSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.mniExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mniDeleteCategory1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mniToolSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mniAppendDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mniAppendSpec = new System.Windows.Forms.ToolStripMenuItem();
            this.mniAppendKeyword = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHomepage = new System.Windows.Forms.ToolStripMenuItem();
            this.mniManual = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mniAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mniLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.tlpTemplatorCell1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxCategoryLevel1 = new System.Windows.Forms.ComboBox();
            this.lblCategoryLevel1Title = new System.Windows.Forms.Label();
            this.cbxCategoryLevel2 = new System.Windows.Forms.ComboBox();
            this.lblCategoryLevel2Title = new System.Windows.Forms.Label();
            this.trvCategory = new System.Windows.Forms.TreeView();
            this.tlpTemplatorCell2 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpTemplatorCell2Cell1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtProductDetail = new System.Windows.Forms.RichTextBox();
            this.lblProductDetail = new System.Windows.Forms.Label();
            this.txtProductDetailCustom = new System.Windows.Forms.RichTextBox();
            this.lblProductDetailCustom = new System.Windows.Forms.Label();
            this.btnAppendDetail = new System.Windows.Forms.Button();
            this.tlpTemplatorCell2Cell2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtProductSpec = new System.Windows.Forms.RichTextBox();
            this.lblProductSpec = new System.Windows.Forms.Label();
            this.txtProductSpecCustom = new System.Windows.Forms.RichTextBox();
            this.lblProductSpecCustom = new System.Windows.Forms.Label();
            this.btnApendSpec = new System.Windows.Forms.Button();
            this.tlpTemplatorCell2Cell3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtProductKeyword = new System.Windows.Forms.RichTextBox();
            this.lblProductKeyword = new System.Windows.Forms.Label();
            this.txtProductKeywordCustom = new System.Windows.Forms.RichTextBox();
            this.lblProductKeywordCustom = new System.Windows.Forms.Label();
            this.btnAppendKeyword = new System.Windows.Forms.Button();
            this.tlpTemplatorCell2Cell4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSyncCategory = new System.Windows.Forms.Button();
            this.btnImportFromExcel = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnGetTemplateFromDrive = new System.Windows.Forms.Button();
            this.btnUploadTemplateToDrive = new System.Windows.Forms.Button();
            this.btnOpenTemplate = new System.Windows.Forms.Button();
            this.btnCloseTemplate = new System.Windows.Forms.Button();
            this.btnSaveTemplateAs = new System.Windows.Forms.Button();
            this.btnSaveTemplate = new System.Windows.Forms.Button();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tslPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspImportExport = new System.Windows.Forms.ToolStripProgressBar();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ttpMain = new System.Windows.Forms.ToolTip(this.components);
            this.bgwImportFromExcel = new System.ComponentModel.BackgroundWorker();
            this.bgwExportToExcel = new System.ComponentModel.BackgroundWorker();
            this.tmrHideMessage = new System.Windows.Forms.Timer(this.components);
            this.bgwUploadToDrive = new System.ComponentModel.BackgroundWorker();
            this.bgwGetFromDrive = new System.ComponentModel.BackgroundWorker();
            this.bgwSyncCategory = new System.ComponentModel.BackgroundWorker();
            this.cmsCategoryAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniDeleteCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).BeginInit();
            this.spcMain.Panel1.SuspendLayout();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            this.tlpTemplatorCell1.SuspendLayout();
            this.tlpTemplatorCell2.SuspendLayout();
            this.tlpTemplatorCell2Cell1.SuspendLayout();
            this.tlpTemplatorCell2Cell2.SuspendLayout();
            this.tlpTemplatorCell2Cell3.SuspendLayout();
            this.tlpTemplatorCell2Cell4.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.cmsCategoryAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.mnuMain, 0, 0);
            this.tlpMain.Controls.Add(this.spcMain, 0, 1);
            this.tlpMain.Controls.Add(this.stsMain, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.Size = new System.Drawing.Size(1264, 681);
            this.tlpMain.TabIndex = 0;
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuHelp,
            this.mniLogout});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1264, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "mnuMain";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniOpenTemplate,
            this.mniSaveTemplate,
            this.mniSaveTemplateAs,
            this.mniCloseTemplate,
            this.mniFileSep1,
            this.mniGetTemplateFromDrive,
            this.mniUploadTemplateToDrive,
            this.mniFileSep2,
            this.mniImportFormExcel,
            this.mniExportToExcel,
            this.mniFileSep3,
            this.mniSyncCategory,
            this.mniFileSep4,
            this.mniExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(39, 20);
            this.mnuFile.Text = "&Tệp";
            // 
            // mniOpenTemplate
            // 
            this.mniOpenTemplate.Image = global::blkTemplator.Properties.Resources.open;
            this.mniOpenTemplate.Name = "mniOpenTemplate";
            this.mniOpenTemplate.Size = new System.Drawing.Size(205, 22);
            this.mniOpenTemplate.Text = "&Mở tệp mẫu";
            this.mniOpenTemplate.Click += new System.EventHandler(this.btnOpenTemplate_Click);
            // 
            // mniSaveTemplate
            // 
            this.mniSaveTemplate.Image = global::blkTemplator.Properties.Resources.save;
            this.mniSaveTemplate.Name = "mniSaveTemplate";
            this.mniSaveTemplate.Size = new System.Drawing.Size(205, 22);
            this.mniSaveTemplate.Text = "&Lưu tệp mẫu";
            this.mniSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
            // 
            // mniSaveTemplateAs
            // 
            this.mniSaveTemplateAs.Image = global::blkTemplator.Properties.Resources.save_as;
            this.mniSaveTemplateAs.Name = "mniSaveTemplateAs";
            this.mniSaveTemplateAs.Size = new System.Drawing.Size(205, 22);
            this.mniSaveTemplateAs.Text = "Lư&u tệp mẫu như là...";
            this.mniSaveTemplateAs.Click += new System.EventHandler(this.btnSaveTemplateAs_Click);
            // 
            // mniCloseTemplate
            // 
            this.mniCloseTemplate.Image = global::blkTemplator.Properties.Resources.close;
            this.mniCloseTemplate.Name = "mniCloseTemplate";
            this.mniCloseTemplate.Size = new System.Drawing.Size(205, 22);
            this.mniCloseTemplate.Text = "Đón&g tệp mẫu";
            this.mniCloseTemplate.Click += new System.EventHandler(this.btnCloseTemplate_Click);
            // 
            // mniFileSep1
            // 
            this.mniFileSep1.Name = "mniFileSep1";
            this.mniFileSep1.Size = new System.Drawing.Size(202, 6);
            // 
            // mniGetTemplateFromDrive
            // 
            this.mniGetTemplateFromDrive.Image = global::blkTemplator.Properties.Resources.get_drive;
            this.mniGetTemplateFromDrive.Name = "mniGetTemplateFromDrive";
            this.mniGetTemplateFromDrive.Size = new System.Drawing.Size(205, 22);
            this.mniGetTemplateFromDrive.Text = "Lấy tệp mẫu từ &Drive";
            this.mniGetTemplateFromDrive.Click += new System.EventHandler(this.btnGetTemplateFromDrive_Click);
            // 
            // mniUploadTemplateToDrive
            // 
            this.mniUploadTemplateToDrive.Image = global::blkTemplator.Properties.Resources.upload;
            this.mniUploadTemplateToDrive.Name = "mniUploadTemplateToDrive";
            this.mniUploadTemplateToDrive.Size = new System.Drawing.Size(205, 22);
            this.mniUploadTemplateToDrive.Text = "Tải tệ&p mẫu lên Drive";
            this.mniUploadTemplateToDrive.Click += new System.EventHandler(this.btnUploadTemplateToDrive_Click);
            // 
            // mniFileSep2
            // 
            this.mniFileSep2.Name = "mniFileSep2";
            this.mniFileSep2.Size = new System.Drawing.Size(202, 6);
            // 
            // mniImportFormExcel
            // 
            this.mniImportFormExcel.Image = global::blkTemplator.Properties.Resources.import;
            this.mniImportFormExcel.Name = "mniImportFormExcel";
            this.mniImportFormExcel.Size = new System.Drawing.Size(205, 22);
            this.mniImportFormExcel.Text = "&Nhập dữ liệu từ tệp Excel";
            this.mniImportFormExcel.Click += new System.EventHandler(this.btnImportFromExcel_Click);
            // 
            // mniExportToExcel
            // 
            this.mniExportToExcel.Image = global::blkTemplator.Properties.Resources.export;
            this.mniExportToExcel.Name = "mniExportToExcel";
            this.mniExportToExcel.Size = new System.Drawing.Size(205, 22);
            this.mniExportToExcel.Text = "&Xuất dữ liệu ra tệp Excel";
            this.mniExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // mniFileSep3
            // 
            this.mniFileSep3.Name = "mniFileSep3";
            this.mniFileSep3.Size = new System.Drawing.Size(202, 6);
            // 
            // mniSyncCategory
            // 
            this.mniSyncCategory.Image = global::blkTemplator.Properties.Resources.sync;
            this.mniSyncCategory.Name = "mniSyncCategory";
            this.mniSyncCategory.Size = new System.Drawing.Size(205, 22);
            this.mniSyncCategory.Text = "Đồng &bộ danh mục";
            this.mniSyncCategory.Click += new System.EventHandler(this.btnSyncCategory_Click);
            // 
            // mniFileSep4
            // 
            this.mniFileSep4.Name = "mniFileSep4";
            this.mniFileSep4.Size = new System.Drawing.Size(202, 6);
            // 
            // mniExit
            // 
            this.mniExit.Image = global::blkTemplator.Properties.Resources.quit;
            this.mniExit.Name = "mniExit";
            this.mniExit.Size = new System.Drawing.Size(205, 22);
            this.mniExit.Text = "T&hoát";
            this.mniExit.Click += new System.EventHandler(this.mniExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniDeleteCategory1,
            this.mniToolSep1,
            this.mniAppendDetail,
            this.mniAppendSpec,
            this.mniAppendKeyword});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(72, 20);
            this.mnuEdit.Text = "&Chỉnh sửa";
            // 
            // mniDeleteCategory1
            // 
            this.mniDeleteCategory1.Image = global::blkTemplator.Properties.Resources.clear;
            this.mniDeleteCategory1.Name = "mniDeleteCategory1";
            this.mniDeleteCategory1.Size = new System.Drawing.Size(201, 22);
            this.mniDeleteCategory1.Text = "&Xoá danh mục đã chọn";
            this.mniDeleteCategory1.Click += new System.EventHandler(this.mniDeleteCategory_Click);
            // 
            // mniToolSep1
            // 
            this.mniToolSep1.Name = "mniToolSep1";
            this.mniToolSep1.Size = new System.Drawing.Size(198, 6);
            // 
            // mniAppendDetail
            // 
            this.mniAppendDetail.Image = global::blkTemplator.Properties.Resources.append;
            this.mniAppendDetail.Name = "mniAppendDetail";
            this.mniAppendDetail.Size = new System.Drawing.Size(201, 22);
            this.mniAppendDetail.Text = "Đẩy &tính năng tuỳ chỉnh";
            this.mniAppendDetail.Click += new System.EventHandler(this.btnAppendDetail_Click);
            // 
            // mniAppendSpec
            // 
            this.mniAppendSpec.Image = global::blkTemplator.Properties.Resources.append;
            this.mniAppendSpec.Name = "mniAppendSpec";
            this.mniAppendSpec.Size = new System.Drawing.Size(201, 22);
            this.mniAppendSpec.Text = "Đẩy thông &số tuỳ chỉnh";
            this.mniAppendSpec.Click += new System.EventHandler(this.btnApendSpec_Click);
            // 
            // mniAppendKeyword
            // 
            this.mniAppendKeyword.Image = global::blkTemplator.Properties.Resources.append;
            this.mniAppendKeyword.Name = "mniAppendKeyword";
            this.mniAppendKeyword.Size = new System.Drawing.Size(201, 22);
            this.mniAppendKeyword.Text = "Đẩy từ &khoá tuỳ chỉnh";
            this.mniAppendKeyword.Click += new System.EventHandler(this.btnAppendKeyword_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniHomepage,
            this.mniManual,
            this.mniHelpSep1,
            this.mniAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(63, 20);
            this.mnuHelp.Text = "Trợ &giúp";
            // 
            // mniHomepage
            // 
            this.mniHomepage.Image = global::blkTemplator.Properties.Resources.homepage;
            this.mniHomepage.Name = "mniHomepage";
            this.mniHomepage.Size = new System.Drawing.Size(134, 22);
            this.mniHomepage.Text = "&Trang chủ";
            this.mniHomepage.Click += new System.EventHandler(this.mniHomepage_Click);
            // 
            // mniManual
            // 
            this.mniManual.Image = global::blkTemplator.Properties.Resources.help;
            this.mniManual.Name = "mniManual";
            this.mniManual.Size = new System.Drawing.Size(134, 22);
            this.mniManual.Text = "&Hướng dẫn";
            this.mniManual.Click += new System.EventHandler(this.mniManual_Click);
            // 
            // mniHelpSep1
            // 
            this.mniHelpSep1.Name = "mniHelpSep1";
            this.mniHelpSep1.Size = new System.Drawing.Size(131, 6);
            // 
            // mniAbout
            // 
            this.mniAbout.Image = global::blkTemplator.Properties.Resources.about;
            this.mniAbout.Name = "mniAbout";
            this.mniAbout.Size = new System.Drawing.Size(134, 22);
            this.mniAbout.Text = "Thông t&in";
            this.mniAbout.Click += new System.EventHandler(this.mniAbout_Click);
            // 
            // mniLogout
            // 
            this.mniLogout.Name = "mniLogout";
            this.mniLogout.Size = new System.Drawing.Size(72, 20);
            this.mniLogout.Text = "Đăng &xuất";
            this.mniLogout.Click += new System.EventHandler(this.mniLogout_Click);
            // 
            // spcMain
            // 
            this.spcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcMain.Location = new System.Drawing.Point(3, 47);
            this.spcMain.Name = "spcMain";
            // 
            // spcMain.Panel1
            // 
            this.spcMain.Panel1.Controls.Add(this.tlpTemplatorCell1);
            // 
            // spcMain.Panel2
            // 
            this.spcMain.Panel2.Controls.Add(this.tlpTemplatorCell2);
            this.spcMain.Size = new System.Drawing.Size(1258, 606);
            this.spcMain.SplitterDistance = 266;
            this.spcMain.TabIndex = 1;
            // 
            // tlpTemplatorCell1
            // 
            this.tlpTemplatorCell1.ColumnCount = 2;
            this.tlpTemplatorCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tlpTemplatorCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTemplatorCell1.Controls.Add(this.cbxCategoryLevel1, 1, 0);
            this.tlpTemplatorCell1.Controls.Add(this.lblCategoryLevel1Title, 0, 0);
            this.tlpTemplatorCell1.Controls.Add(this.cbxCategoryLevel2, 1, 1);
            this.tlpTemplatorCell1.Controls.Add(this.lblCategoryLevel2Title, 0, 1);
            this.tlpTemplatorCell1.Controls.Add(this.trvCategory, 0, 2);
            this.tlpTemplatorCell1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTemplatorCell1.Location = new System.Drawing.Point(0, 0);
            this.tlpTemplatorCell1.Name = "tlpTemplatorCell1";
            this.tlpTemplatorCell1.RowCount = 3;
            this.tlpTemplatorCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpTemplatorCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpTemplatorCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTemplatorCell1.Size = new System.Drawing.Size(266, 606);
            this.tlpTemplatorCell1.TabIndex = 0;
            // 
            // cbxCategoryLevel1
            // 
            this.cbxCategoryLevel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxCategoryLevel1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCategoryLevel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbxCategoryLevel1.FormattingEnabled = true;
            this.cbxCategoryLevel1.Location = new System.Drawing.Point(78, 3);
            this.cbxCategoryLevel1.Name = "cbxCategoryLevel1";
            this.cbxCategoryLevel1.Size = new System.Drawing.Size(185, 23);
            this.cbxCategoryLevel1.TabIndex = 0;
            this.cbxCategoryLevel1.SelectedIndexChanged += new System.EventHandler(this.cbxCategoryLevel1_SelectedIndexChanged);
            // 
            // lblCategoryLevel1Title
            // 
            this.lblCategoryLevel1Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategoryLevel1Title.Location = new System.Drawing.Point(3, 0);
            this.lblCategoryLevel1Title.Name = "lblCategoryLevel1Title";
            this.lblCategoryLevel1Title.Size = new System.Drawing.Size(69, 29);
            this.lblCategoryLevel1Title.TabIndex = 1;
            this.lblCategoryLevel1Title.Text = "Danh mục:";
            this.lblCategoryLevel1Title.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxCategoryLevel2
            // 
            this.cbxCategoryLevel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxCategoryLevel2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCategoryLevel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbxCategoryLevel2.FormattingEnabled = true;
            this.cbxCategoryLevel2.Location = new System.Drawing.Point(78, 32);
            this.cbxCategoryLevel2.Name = "cbxCategoryLevel2";
            this.cbxCategoryLevel2.Size = new System.Drawing.Size(185, 23);
            this.cbxCategoryLevel2.TabIndex = 2;
            this.cbxCategoryLevel2.SelectedIndexChanged += new System.EventHandler(this.cbxCategoryLevel2_SelectedIndexChanged);
            // 
            // lblCategoryLevel2Title
            // 
            this.lblCategoryLevel2Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategoryLevel2Title.Location = new System.Drawing.Point(3, 29);
            this.lblCategoryLevel2Title.Name = "lblCategoryLevel2Title";
            this.lblCategoryLevel2Title.Size = new System.Drawing.Size(69, 29);
            this.lblCategoryLevel2Title.TabIndex = 3;
            this.lblCategoryLevel2Title.Text = "Danh mục 1:";
            this.lblCategoryLevel2Title.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trvCategory
            // 
            this.tlpTemplatorCell1.SetColumnSpan(this.trvCategory, 2);
            this.trvCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCategory.Location = new System.Drawing.Point(3, 61);
            this.trvCategory.Name = "trvCategory";
            this.trvCategory.Size = new System.Drawing.Size(260, 542);
            this.trvCategory.TabIndex = 4;
            this.ttpMain.SetToolTip(this.trvCategory, "Danh mục sản phẩm");
            this.trvCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCategory_AfterSelect);
            this.trvCategory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvCategory_MouseDown);
            // 
            // tlpTemplatorCell2
            // 
            this.tlpTemplatorCell2.ColumnCount = 1;
            this.tlpTemplatorCell2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTemplatorCell2.Controls.Add(this.tlpTemplatorCell2Cell1, 0, 0);
            this.tlpTemplatorCell2.Controls.Add(this.tlpTemplatorCell2Cell2, 0, 2);
            this.tlpTemplatorCell2.Controls.Add(this.tlpTemplatorCell2Cell3, 0, 4);
            this.tlpTemplatorCell2.Controls.Add(this.tlpTemplatorCell2Cell4, 0, 5);
            this.tlpTemplatorCell2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTemplatorCell2.Location = new System.Drawing.Point(0, 0);
            this.tlpTemplatorCell2.Name = "tlpTemplatorCell2";
            this.tlpTemplatorCell2.RowCount = 6;
            this.tlpTemplatorCell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpTemplatorCell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpTemplatorCell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpTemplatorCell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpTemplatorCell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpTemplatorCell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tlpTemplatorCell2.Size = new System.Drawing.Size(988, 606);
            this.tlpTemplatorCell2.TabIndex = 0;
            // 
            // tlpTemplatorCell2Cell1
            // 
            this.tlpTemplatorCell2Cell1.ColumnCount = 3;
            this.tlpTemplatorCell2Cell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTemplatorCell2Cell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTemplatorCell2Cell1.Controls.Add(this.txtProductDetail, 0, 1);
            this.tlpTemplatorCell2Cell1.Controls.Add(this.lblProductDetail, 0, 0);
            this.tlpTemplatorCell2Cell1.Controls.Add(this.txtProductDetailCustom, 2, 1);
            this.tlpTemplatorCell2Cell1.Controls.Add(this.lblProductDetailCustom, 2, 0);
            this.tlpTemplatorCell2Cell1.Controls.Add(this.btnAppendDetail, 1, 1);
            this.tlpTemplatorCell2Cell1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTemplatorCell2Cell1.Location = new System.Drawing.Point(3, 3);
            this.tlpTemplatorCell2Cell1.Name = "tlpTemplatorCell2Cell1";
            this.tlpTemplatorCell2Cell1.RowCount = 2;
            this.tlpTemplatorCell2Cell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tlpTemplatorCell2Cell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTemplatorCell2Cell1.Size = new System.Drawing.Size(982, 214);
            this.tlpTemplatorCell2Cell1.TabIndex = 0;
            // 
            // txtProductDetail
            // 
            this.txtProductDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductDetail.Location = new System.Drawing.Point(3, 20);
            this.txtProductDetail.Name = "txtProductDetail";
            this.txtProductDetail.Size = new System.Drawing.Size(466, 191);
            this.txtProductDetail.TabIndex = 0;
            this.txtProductDetail.Text = "";
            this.ttpMain.SetToolTip(this.txtProductDetail, "Nhập mẫu tính năng sản phẩm");
            this.txtProductDetail.TextChanged += new System.EventHandler(this.txtProduct_TextChanged);
            this.txtProductDetail.Validating += new System.ComponentModel.CancelEventHandler(this.txtProduct_Validating);
            this.txtProductDetail.Validated += new System.EventHandler(this.txtProduct_Validated);
            // 
            // lblProductDetail
            // 
            this.lblProductDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductDetail.Location = new System.Drawing.Point(3, 0);
            this.lblProductDetail.Name = "lblProductDetail";
            this.lblProductDetail.Size = new System.Drawing.Size(466, 17);
            this.lblProductDetail.TabIndex = 1;
            this.lblProductDetail.Text = "Tính năng:";
            // 
            // txtProductDetailCustom
            // 
            this.txtProductDetailCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductDetailCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductDetailCustom.Location = new System.Drawing.Point(513, 20);
            this.txtProductDetailCustom.Name = "txtProductDetailCustom";
            this.txtProductDetailCustom.Size = new System.Drawing.Size(466, 191);
            this.txtProductDetailCustom.TabIndex = 2;
            this.txtProductDetailCustom.Text = "";
            this.ttpMain.SetToolTip(this.txtProductDetailCustom, "Nhập mẫu tính năng sản phẩm");
            this.txtProductDetailCustom.TextChanged += new System.EventHandler(this.txtProduct_TextChanged);
            this.txtProductDetailCustom.Validating += new System.ComponentModel.CancelEventHandler(this.txtProduct_Validating);
            this.txtProductDetailCustom.Validated += new System.EventHandler(this.txtProduct_Validated);
            // 
            // lblProductDetailCustom
            // 
            this.lblProductDetailCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductDetailCustom.Location = new System.Drawing.Point(513, 0);
            this.lblProductDetailCustom.Name = "lblProductDetailCustom";
            this.lblProductDetailCustom.Size = new System.Drawing.Size(466, 17);
            this.lblProductDetailCustom.TabIndex = 3;
            this.lblProductDetailCustom.Text = "Tính năng tuỳ chỉnh:";
            // 
            // btnAppendDetail
            // 
            this.btnAppendDetail.BackgroundImage = global::blkTemplator.Properties.Resources.append;
            this.btnAppendDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAppendDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAppendDetail.Location = new System.Drawing.Point(475, 20);
            this.btnAppendDetail.Name = "btnAppendDetail";
            this.btnAppendDetail.Size = new System.Drawing.Size(32, 32);
            this.btnAppendDetail.TabIndex = 4;
            this.ttpMain.SetToolTip(this.btnAppendDetail, "Đẩy tính năng tuỳ chỉnh sang tính năng");
            this.btnAppendDetail.UseVisualStyleBackColor = true;
            this.btnAppendDetail.Click += new System.EventHandler(this.btnAppendDetail_Click);
            // 
            // tlpTemplatorCell2Cell2
            // 
            this.tlpTemplatorCell2Cell2.ColumnCount = 3;
            this.tlpTemplatorCell2Cell2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTemplatorCell2Cell2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTemplatorCell2Cell2.Controls.Add(this.txtProductSpec, 0, 1);
            this.tlpTemplatorCell2Cell2.Controls.Add(this.lblProductSpec, 0, 0);
            this.tlpTemplatorCell2Cell2.Controls.Add(this.txtProductSpecCustom, 2, 1);
            this.tlpTemplatorCell2Cell2.Controls.Add(this.lblProductSpecCustom, 2, 0);
            this.tlpTemplatorCell2Cell2.Controls.Add(this.btnApendSpec, 1, 1);
            this.tlpTemplatorCell2Cell2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTemplatorCell2Cell2.Location = new System.Drawing.Point(3, 228);
            this.tlpTemplatorCell2Cell2.Name = "tlpTemplatorCell2Cell2";
            this.tlpTemplatorCell2Cell2.RowCount = 2;
            this.tlpTemplatorCell2Cell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tlpTemplatorCell2Cell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTemplatorCell2Cell2.Size = new System.Drawing.Size(982, 214);
            this.tlpTemplatorCell2Cell2.TabIndex = 1;
            // 
            // txtProductSpec
            // 
            this.txtProductSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductSpec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductSpec.Location = new System.Drawing.Point(3, 20);
            this.txtProductSpec.Name = "txtProductSpec";
            this.txtProductSpec.Size = new System.Drawing.Size(466, 191);
            this.txtProductSpec.TabIndex = 0;
            this.txtProductSpec.Text = "";
            this.ttpMain.SetToolTip(this.txtProductSpec, resources.GetString("txtProductSpec.ToolTip"));
            this.txtProductSpec.TextChanged += new System.EventHandler(this.txtProduct_TextChanged);
            this.txtProductSpec.Validating += new System.ComponentModel.CancelEventHandler(this.txtProduct_Validating);
            this.txtProductSpec.Validated += new System.EventHandler(this.txtProduct_Validated);
            // 
            // lblProductSpec
            // 
            this.lblProductSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductSpec.Location = new System.Drawing.Point(3, 0);
            this.lblProductSpec.Name = "lblProductSpec";
            this.lblProductSpec.Size = new System.Drawing.Size(466, 17);
            this.lblProductSpec.TabIndex = 1;
            this.lblProductSpec.Text = "Thông số kỹ thuật/Bảng thông số:";
            // 
            // txtProductSpecCustom
            // 
            this.txtProductSpecCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductSpecCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductSpecCustom.Location = new System.Drawing.Point(513, 20);
            this.txtProductSpecCustom.Name = "txtProductSpecCustom";
            this.txtProductSpecCustom.Size = new System.Drawing.Size(466, 191);
            this.txtProductSpecCustom.TabIndex = 2;
            this.txtProductSpecCustom.Text = "";
            this.ttpMain.SetToolTip(this.txtProductSpecCustom, resources.GetString("txtProductSpecCustom.ToolTip"));
            this.txtProductSpecCustom.TextChanged += new System.EventHandler(this.txtProduct_TextChanged);
            this.txtProductSpecCustom.Validating += new System.ComponentModel.CancelEventHandler(this.txtProduct_Validating);
            this.txtProductSpecCustom.Validated += new System.EventHandler(this.txtProduct_Validated);
            // 
            // lblProductSpecCustom
            // 
            this.lblProductSpecCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductSpecCustom.Location = new System.Drawing.Point(513, 0);
            this.lblProductSpecCustom.Name = "lblProductSpecCustom";
            this.lblProductSpecCustom.Size = new System.Drawing.Size(466, 17);
            this.lblProductSpecCustom.TabIndex = 3;
            this.lblProductSpecCustom.Text = "Thông số kỹ thuật/Bảng thông số tuỳ chỉnh:";
            // 
            // btnApendSpec
            // 
            this.btnApendSpec.BackgroundImage = global::blkTemplator.Properties.Resources.append;
            this.btnApendSpec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnApendSpec.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnApendSpec.Location = new System.Drawing.Point(475, 20);
            this.btnApendSpec.Name = "btnApendSpec";
            this.btnApendSpec.Size = new System.Drawing.Size(32, 32);
            this.btnApendSpec.TabIndex = 4;
            this.ttpMain.SetToolTip(this.btnApendSpec, "Đẩy thông số tuỳ chỉnh sang thông số");
            this.btnApendSpec.UseVisualStyleBackColor = true;
            this.btnApendSpec.Click += new System.EventHandler(this.btnApendSpec_Click);
            // 
            // tlpTemplatorCell2Cell3
            // 
            this.tlpTemplatorCell2Cell3.ColumnCount = 3;
            this.tlpTemplatorCell2Cell3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTemplatorCell2Cell3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTemplatorCell2Cell3.Controls.Add(this.txtProductKeyword, 0, 1);
            this.tlpTemplatorCell2Cell3.Controls.Add(this.lblProductKeyword, 0, 0);
            this.tlpTemplatorCell2Cell3.Controls.Add(this.txtProductKeywordCustom, 2, 1);
            this.tlpTemplatorCell2Cell3.Controls.Add(this.lblProductKeywordCustom, 2, 0);
            this.tlpTemplatorCell2Cell3.Controls.Add(this.btnAppendKeyword, 1, 1);
            this.tlpTemplatorCell2Cell3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTemplatorCell2Cell3.Location = new System.Drawing.Point(3, 453);
            this.tlpTemplatorCell2Cell3.Name = "tlpTemplatorCell2Cell3";
            this.tlpTemplatorCell2Cell3.RowCount = 2;
            this.tlpTemplatorCell2Cell3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tlpTemplatorCell2Cell3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTemplatorCell2Cell3.Size = new System.Drawing.Size(982, 104);
            this.tlpTemplatorCell2Cell3.TabIndex = 2;
            // 
            // txtProductKeyword
            // 
            this.txtProductKeyword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductKeyword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductKeyword.Location = new System.Drawing.Point(3, 20);
            this.txtProductKeyword.Name = "txtProductKeyword";
            this.txtProductKeyword.Size = new System.Drawing.Size(466, 81);
            this.txtProductKeyword.TabIndex = 0;
            this.txtProductKeyword.Text = "";
            this.ttpMain.SetToolTip(this.txtProductKeyword, "Nhập mẫu từ khoá sản phẩm");
            this.txtProductKeyword.TextChanged += new System.EventHandler(this.txtProduct_TextChanged);
            this.txtProductKeyword.Validating += new System.ComponentModel.CancelEventHandler(this.txtProduct_Validating);
            this.txtProductKeyword.Validated += new System.EventHandler(this.txtProduct_Validated);
            // 
            // lblProductKeyword
            // 
            this.lblProductKeyword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductKeyword.Location = new System.Drawing.Point(3, 0);
            this.lblProductKeyword.Name = "lblProductKeyword";
            this.lblProductKeyword.Size = new System.Drawing.Size(466, 17);
            this.lblProductKeyword.TabIndex = 1;
            this.lblProductKeyword.Text = "Từ khoá:";
            // 
            // txtProductKeywordCustom
            // 
            this.txtProductKeywordCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductKeywordCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductKeywordCustom.Location = new System.Drawing.Point(513, 20);
            this.txtProductKeywordCustom.Name = "txtProductKeywordCustom";
            this.txtProductKeywordCustom.Size = new System.Drawing.Size(466, 81);
            this.txtProductKeywordCustom.TabIndex = 2;
            this.txtProductKeywordCustom.Text = "";
            this.ttpMain.SetToolTip(this.txtProductKeywordCustom, "Nhập mẫu từ khoá sản phẩm");
            this.txtProductKeywordCustom.TextChanged += new System.EventHandler(this.txtProduct_TextChanged);
            this.txtProductKeywordCustom.Validating += new System.ComponentModel.CancelEventHandler(this.txtProduct_Validating);
            this.txtProductKeywordCustom.Validated += new System.EventHandler(this.txtProduct_Validated);
            // 
            // lblProductKeywordCustom
            // 
            this.lblProductKeywordCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductKeywordCustom.Location = new System.Drawing.Point(513, 0);
            this.lblProductKeywordCustom.Name = "lblProductKeywordCustom";
            this.lblProductKeywordCustom.Size = new System.Drawing.Size(466, 17);
            this.lblProductKeywordCustom.TabIndex = 3;
            this.lblProductKeywordCustom.Text = "Từ khoá tuỳ chỉnh:";
            // 
            // btnAppendKeyword
            // 
            this.btnAppendKeyword.BackgroundImage = global::blkTemplator.Properties.Resources.append;
            this.btnAppendKeyword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAppendKeyword.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAppendKeyword.Location = new System.Drawing.Point(475, 20);
            this.btnAppendKeyword.Name = "btnAppendKeyword";
            this.btnAppendKeyword.Size = new System.Drawing.Size(32, 32);
            this.btnAppendKeyword.TabIndex = 4;
            this.ttpMain.SetToolTip(this.btnAppendKeyword, "Đẩy từ khoá tuỳ chỉnh sang từ khoá");
            this.btnAppendKeyword.UseVisualStyleBackColor = true;
            this.btnAppendKeyword.Click += new System.EventHandler(this.btnAppendKeyword_Click);
            // 
            // tlpTemplatorCell2Cell4
            // 
            this.tlpTemplatorCell2Cell4.ColumnCount = 10;
            this.tlpTemplatorCell2Cell4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTemplatorCell2Cell4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpTemplatorCell2Cell4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTemplatorCell2Cell4.Controls.Add(this.btnSyncCategory, 0, 0);
            this.tlpTemplatorCell2Cell4.Controls.Add(this.btnImportFromExcel, 2, 0);
            this.tlpTemplatorCell2Cell4.Controls.Add(this.btnExportToExcel, 3, 0);
            this.tlpTemplatorCell2Cell4.Controls.Add(this.btnGetTemplateFromDrive, 4, 0);
            this.tlpTemplatorCell2Cell4.Controls.Add(this.btnUploadTemplateToDrive, 5, 0);
            this.tlpTemplatorCell2Cell4.Controls.Add(this.btnOpenTemplate, 6, 0);
            this.tlpTemplatorCell2Cell4.Controls.Add(this.btnCloseTemplate, 7, 0);
            this.tlpTemplatorCell2Cell4.Controls.Add(this.btnSaveTemplateAs, 8, 0);
            this.tlpTemplatorCell2Cell4.Controls.Add(this.btnSaveTemplate, 9, 0);
            this.tlpTemplatorCell2Cell4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTemplatorCell2Cell4.Location = new System.Drawing.Point(3, 563);
            this.tlpTemplatorCell2Cell4.Name = "tlpTemplatorCell2Cell4";
            this.tlpTemplatorCell2Cell4.RowCount = 1;
            this.tlpTemplatorCell2Cell4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTemplatorCell2Cell4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTemplatorCell2Cell4.Size = new System.Drawing.Size(982, 40);
            this.tlpTemplatorCell2Cell4.TabIndex = 3;
            // 
            // btnSyncCategory
            // 
            this.btnSyncCategory.BackgroundImage = global::blkTemplator.Properties.Resources.sync;
            this.btnSyncCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSyncCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSyncCategory.Location = new System.Drawing.Point(3, 3);
            this.btnSyncCategory.Name = "btnSyncCategory";
            this.btnSyncCategory.Size = new System.Drawing.Size(32, 34);
            this.btnSyncCategory.TabIndex = 0;
            this.ttpMain.SetToolTip(this.btnSyncCategory, "Đồng bộ danh mục\r\nVui lòng nạp danh mục trên BanLinhKien Editor trước khi thực\r\nh" +
        "iện việc này nếu không bạn cần phải có tệp danh mục để đồng bộ");
            this.btnSyncCategory.UseVisualStyleBackColor = true;
            this.btnSyncCategory.Click += new System.EventHandler(this.btnSyncCategory_Click);
            // 
            // btnImportFromExcel
            // 
            this.btnImportFromExcel.BackgroundImage = global::blkTemplator.Properties.Resources.import;
            this.btnImportFromExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnImportFromExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImportFromExcel.Location = new System.Drawing.Point(681, 3);
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.Size = new System.Drawing.Size(32, 34);
            this.btnImportFromExcel.TabIndex = 1;
            this.ttpMain.SetToolTip(this.btnImportFromExcel, "Nhập dữ liệu từ tệp Excel");
            this.btnImportFromExcel.UseVisualStyleBackColor = true;
            this.btnImportFromExcel.Click += new System.EventHandler(this.btnImportFromExcel_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.BackgroundImage = global::blkTemplator.Properties.Resources.export;
            this.btnExportToExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExportToExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportToExcel.Location = new System.Drawing.Point(719, 3);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(32, 34);
            this.btnExportToExcel.TabIndex = 2;
            this.ttpMain.SetToolTip(this.btnExportToExcel, "Xuất dữ liệu ra tệp Excel");
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnGetTemplateFromDrive
            // 
            this.btnGetTemplateFromDrive.BackgroundImage = global::blkTemplator.Properties.Resources.get_drive;
            this.btnGetTemplateFromDrive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetTemplateFromDrive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetTemplateFromDrive.Location = new System.Drawing.Point(757, 3);
            this.btnGetTemplateFromDrive.Name = "btnGetTemplateFromDrive";
            this.btnGetTemplateFromDrive.Size = new System.Drawing.Size(32, 34);
            this.btnGetTemplateFromDrive.TabIndex = 3;
            this.ttpMain.SetToolTip(this.btnGetTemplateFromDrive, "Lấy tệp mẫu từ Drive");
            this.btnGetTemplateFromDrive.UseVisualStyleBackColor = true;
            this.btnGetTemplateFromDrive.Click += new System.EventHandler(this.btnGetTemplateFromDrive_Click);
            // 
            // btnUploadTemplateToDrive
            // 
            this.btnUploadTemplateToDrive.BackgroundImage = global::blkTemplator.Properties.Resources.upload;
            this.btnUploadTemplateToDrive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUploadTemplateToDrive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUploadTemplateToDrive.Location = new System.Drawing.Point(795, 3);
            this.btnUploadTemplateToDrive.Name = "btnUploadTemplateToDrive";
            this.btnUploadTemplateToDrive.Size = new System.Drawing.Size(32, 34);
            this.btnUploadTemplateToDrive.TabIndex = 4;
            this.ttpMain.SetToolTip(this.btnUploadTemplateToDrive, "Tải tệp mẫu lên Drive");
            this.btnUploadTemplateToDrive.UseVisualStyleBackColor = true;
            this.btnUploadTemplateToDrive.Click += new System.EventHandler(this.btnUploadTemplateToDrive_Click);
            // 
            // btnOpenTemplate
            // 
            this.btnOpenTemplate.BackgroundImage = global::blkTemplator.Properties.Resources.open;
            this.btnOpenTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOpenTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenTemplate.Location = new System.Drawing.Point(833, 3);
            this.btnOpenTemplate.Name = "btnOpenTemplate";
            this.btnOpenTemplate.Size = new System.Drawing.Size(32, 34);
            this.btnOpenTemplate.TabIndex = 5;
            this.ttpMain.SetToolTip(this.btnOpenTemplate, "Mở tệp mẫu");
            this.btnOpenTemplate.UseVisualStyleBackColor = true;
            this.btnOpenTemplate.Click += new System.EventHandler(this.btnOpenTemplate_Click);
            // 
            // btnCloseTemplate
            // 
            this.btnCloseTemplate.BackgroundImage = global::blkTemplator.Properties.Resources.close;
            this.btnCloseTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCloseTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCloseTemplate.Location = new System.Drawing.Point(871, 3);
            this.btnCloseTemplate.Name = "btnCloseTemplate";
            this.btnCloseTemplate.Size = new System.Drawing.Size(32, 34);
            this.btnCloseTemplate.TabIndex = 6;
            this.ttpMain.SetToolTip(this.btnCloseTemplate, "Đóng tệp mẫu");
            this.btnCloseTemplate.UseVisualStyleBackColor = true;
            this.btnCloseTemplate.Click += new System.EventHandler(this.btnCloseTemplate_Click);
            // 
            // btnSaveTemplateAs
            // 
            this.btnSaveTemplateAs.BackgroundImage = global::blkTemplator.Properties.Resources.save_as;
            this.btnSaveTemplateAs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSaveTemplateAs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveTemplateAs.Location = new System.Drawing.Point(909, 3);
            this.btnSaveTemplateAs.Name = "btnSaveTemplateAs";
            this.btnSaveTemplateAs.Size = new System.Drawing.Size(32, 34);
            this.btnSaveTemplateAs.TabIndex = 7;
            this.ttpMain.SetToolTip(this.btnSaveTemplateAs, "Lưu tệp mẫu như là...");
            this.btnSaveTemplateAs.UseVisualStyleBackColor = true;
            this.btnSaveTemplateAs.Click += new System.EventHandler(this.btnSaveTemplateAs_Click);
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.BackgroundImage = global::blkTemplator.Properties.Resources.save;
            this.btnSaveTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveTemplate.Location = new System.Drawing.Point(947, 3);
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size(32, 34);
            this.btnSaveTemplate.TabIndex = 8;
            this.ttpMain.SetToolTip(this.btnSaveTemplate, "Lưu tệp mẫu");
            this.btnSaveTemplate.UseVisualStyleBackColor = true;
            this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
            // 
            // stsMain
            // 
            this.stsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslPath,
            this.tspImportExport,
            this.tslStatus});
            this.stsMain.Location = new System.Drawing.Point(0, 656);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(1264, 25);
            this.stsMain.TabIndex = 2;
            this.stsMain.Text = "stsMain";
            // 
            // tslPath
            // 
            this.tslPath.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tslPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslPath.Name = "tslPath";
            this.tslPath.Size = new System.Drawing.Size(58, 20);
            this.tslPath.Text = "Sẵn sàng";
            // 
            // tspImportExport
            // 
            this.tspImportExport.Name = "tspImportExport";
            this.tspImportExport.Size = new System.Drawing.Size(100, 19);
            this.tspImportExport.Visible = false;
            // 
            // tslStatus
            // 
            this.tslStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(54, 20);
            this.tslStatus.Text = "Sẵn sàng";
            // 
            // bgwImportFromExcel
            // 
            this.bgwImportFromExcel.WorkerReportsProgress = true;
            this.bgwImportFromExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwImportFromExcel_DoWork);
            this.bgwImportFromExcel.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwImportFromExcel_ProgressChanged);
            this.bgwImportFromExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwImportFromExcel_RunWorkerCompleted);
            // 
            // bgwExportToExcel
            // 
            this.bgwExportToExcel.WorkerReportsProgress = true;
            this.bgwExportToExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwExportToExcel_DoWork);
            this.bgwExportToExcel.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwExportToExcel_ProgressChanged);
            this.bgwExportToExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwExportToExcel_RunWorkerCompleted);
            // 
            // tmrHideMessage
            // 
            this.tmrHideMessage.Enabled = true;
            this.tmrHideMessage.Interval = 1500;
            this.tmrHideMessage.Tick += new System.EventHandler(this.tmrHideMessage_Tick);
            // 
            // bgwUploadToDrive
            // 
            this.bgwUploadToDrive.WorkerReportsProgress = true;
            this.bgwUploadToDrive.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwUploadToDrive_DoWork);
            this.bgwUploadToDrive.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwUploadToDrive_ProgressChanged);
            this.bgwUploadToDrive.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwUploadToDrive_RunWorkerCompleted);
            // 
            // bgwGetFromDrive
            // 
            this.bgwGetFromDrive.WorkerReportsProgress = true;
            this.bgwGetFromDrive.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwGetFromDrive_DoWork);
            this.bgwGetFromDrive.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwGetFromDrive_ProgressChanged);
            this.bgwGetFromDrive.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwGetFromDrive_RunWorkerCompleted);
            // 
            // bgwSyncCategory
            // 
            this.bgwSyncCategory.WorkerReportsProgress = true;
            this.bgwSyncCategory.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSyncCategory_DoWork);
            this.bgwSyncCategory.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwSyncCategory_ProgressChanged);
            this.bgwSyncCategory.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwSyncCategory_RunWorkerCompleted);
            // 
            // cmsCategoryAction
            // 
            this.cmsCategoryAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniDeleteCategory});
            this.cmsCategoryAction.Name = "cmsCategory";
            this.cmsCategoryAction.Size = new System.Drawing.Size(152, 26);
            this.cmsCategoryAction.Text = "Danh mục";
            // 
            // mniDeleteCategory
            // 
            this.mniDeleteCategory.Name = "mniDeleteCategory";
            this.mniDeleteCategory.Size = new System.Drawing.Size(151, 22);
            this.mniDeleteCategory.Text = "&Xoá danh mục";
            this.mniDeleteCategory.Click += new System.EventHandler(this.mniDeleteCategory_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.tlpMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(1080, 720);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BanLinhKien Templator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.spcMain.Panel1.ResumeLayout(false);
            this.spcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).EndInit();
            this.spcMain.ResumeLayout(false);
            this.tlpTemplatorCell1.ResumeLayout(false);
            this.tlpTemplatorCell2.ResumeLayout(false);
            this.tlpTemplatorCell2Cell1.ResumeLayout(false);
            this.tlpTemplatorCell2Cell2.ResumeLayout(false);
            this.tlpTemplatorCell2Cell3.ResumeLayout(false);
            this.tlpTemplatorCell2Cell4.ResumeLayout(false);
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.cmsCategoryAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.SplitContainer spcMain;
        private System.Windows.Forms.TreeView trvCategory;
        private System.Windows.Forms.TableLayoutPanel tlpTemplatorCell2;
        private System.Windows.Forms.RichTextBox txtProductDetail;
        private System.Windows.Forms.RichTextBox txtProductSpec;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripSeparator mniFileSep1;
        private System.Windows.Forms.ToolStripMenuItem mniExit;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolTip ttpMain;
        private System.Windows.Forms.ToolStripMenuItem mniSaveTemplate;
        private System.Windows.Forms.Label lblProductDetail;
        private System.Windows.Forms.Label lblProductSpec;
        private System.Windows.Forms.ToolStripMenuItem mniOpenTemplate;
        private System.Windows.Forms.TableLayoutPanel tlpTemplatorCell2Cell4;
        private System.Windows.Forms.Button btnSaveTemplate;
        private System.Windows.Forms.Button btnOpenTemplate;
        private System.Windows.Forms.ToolStripMenuItem mniAbout;
        private System.Windows.Forms.ToolStripMenuItem mniHomepage;
        private System.Windows.Forms.ToolStripMenuItem mniManual;
        private System.Windows.Forms.ToolStripSeparator mniHelpSep1;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tslPath;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button btnImportFromExcel;
        private System.ComponentModel.BackgroundWorker bgwImportFromExcel;
        private System.ComponentModel.BackgroundWorker bgwExportToExcel;
        private System.Windows.Forms.ToolStripProgressBar tspImportExport;
        private System.Windows.Forms.ToolStripMenuItem mniImportFormExcel;
        private System.Windows.Forms.ToolStripMenuItem mniExportToExcel;
        private System.Windows.Forms.ToolStripSeparator mniFileSep2;
        private System.Windows.Forms.Timer tmrHideMessage;
        private System.Windows.Forms.Button btnSaveTemplateAs;
        private System.Windows.Forms.ToolStripMenuItem mniSaveTemplateAs;
        private System.Windows.Forms.ToolStripMenuItem mniGetTemplateFromDrive;
        private System.Windows.Forms.ToolStripSeparator mniFileSep3;
        private System.Windows.Forms.ToolStripMenuItem mniUploadTemplateToDrive;
        private System.Windows.Forms.Button btnGetTemplateFromDrive;
        private System.Windows.Forms.Button btnUploadTemplateToDrive;
        private System.ComponentModel.BackgroundWorker bgwUploadToDrive;
        private System.Windows.Forms.Button btnCloseTemplate;
        private System.Windows.Forms.ToolStripMenuItem mniCloseTemplate;
        private System.ComponentModel.BackgroundWorker bgwGetFromDrive;
        private System.Windows.Forms.Button btnSyncCategory;
        private System.ComponentModel.BackgroundWorker bgwSyncCategory;
        private System.Windows.Forms.TableLayoutPanel tlpTemplatorCell2Cell2;
        private System.Windows.Forms.TableLayoutPanel tlpTemplatorCell2Cell3;
        private System.Windows.Forms.TableLayoutPanel tlpTemplatorCell2Cell1;
        private System.Windows.Forms.RichTextBox txtProductKeyword;
        private System.Windows.Forms.Label lblProductKeyword;
        private System.Windows.Forms.ToolStripMenuItem mniSyncCategory;
        private System.Windows.Forms.ToolStripSeparator mniFileSep4;
        private System.Windows.Forms.ToolStripMenuItem mniLogout;
        private System.Windows.Forms.TableLayoutPanel tlpTemplatorCell1;
        private System.Windows.Forms.ComboBox cbxCategoryLevel1;
        private System.Windows.Forms.ComboBox cbxCategoryLevel2;
        private System.Windows.Forms.Label lblCategoryLevel1Title;
        private System.Windows.Forms.Label lblCategoryLevel2Title;
        private System.Windows.Forms.ContextMenuStrip cmsCategoryAction;
        private System.Windows.Forms.ToolStripMenuItem mniDeleteCategory;
        private System.Windows.Forms.RichTextBox txtProductDetailCustom;
        private System.Windows.Forms.Label lblProductDetailCustom;
        private System.Windows.Forms.RichTextBox txtProductSpecCustom;
        private System.Windows.Forms.Label lblProductSpecCustom;
        private System.Windows.Forms.Label lblProductKeywordCustom;
        private System.Windows.Forms.RichTextBox txtProductKeywordCustom;
        private System.Windows.Forms.Button btnAppendDetail;
        private System.Windows.Forms.Button btnApendSpec;
        private System.Windows.Forms.Button btnAppendKeyword;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mniAppendDetail;
        private System.Windows.Forms.ToolStripMenuItem mniAppendSpec;
        private System.Windows.Forms.ToolStripMenuItem mniAppendKeyword;
        private System.Windows.Forms.ToolStripMenuItem mniDeleteCategory1;
        private System.Windows.Forms.ToolStripSeparator mniToolSep1;
    }
}


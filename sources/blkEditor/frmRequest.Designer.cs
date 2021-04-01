namespace blkEditor
{
    partial class frmRequest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRequest));
            this.tlpRequest = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRequestCell1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClearProductNameRequestList = new System.Windows.Forms.Button();
            this.btnImportRequestListFromExcel = new System.Windows.Forms.Button();
            this.txtAddProductNameRequest = new System.Windows.Forms.TextBox();
            this.btnAddProductNameRequest = new System.Windows.Forms.Button();
            this.lblAddProductNameRequest = new System.Windows.Forms.Label();
            this.btnClearProductNameDoneList = new System.Windows.Forms.Button();
            this.spcRequest = new System.Windows.Forms.SplitContainer();
            this.tlpRequestCell2 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRequesCell2Cell1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClearProductNameRequest = new System.Windows.Forms.Button();
            this.btnMarkProductAsDone = new System.Windows.Forms.Button();
            this.lblProductNameRequestList = new System.Windows.Forms.Label();
            this.lbxProductNameRequestList = new System.Windows.Forms.ListBox();
            this.tlpRequestCell3 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRequestCell3Cell1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClearProductNameDone = new System.Windows.Forms.Button();
            this.btnProductNameRequestAgain = new System.Windows.Forms.Button();
            this.btnEditWithThisProduct = new System.Windows.Forms.Button();
            this.lblProductNameDoneList = new System.Windows.Forms.Label();
            this.lbxProductNameDoneList = new System.Windows.Forms.ListBox();
            this.stsRequest = new System.Windows.Forms.StatusStrip();
            this.tspImport = new System.Windows.Forms.ToolStripProgressBar();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ttpReques = new System.Windows.Forms.ToolTip(this.components);
            this.tmrMonitorUI = new System.Windows.Forms.Timer(this.components);
            this.tmrHideMessage = new System.Windows.Forms.Timer(this.components);
            this.bgwImportFromExcel = new System.ComponentModel.BackgroundWorker();
            this.tlpRequest.SuspendLayout();
            this.tlpRequestCell1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcRequest)).BeginInit();
            this.spcRequest.Panel1.SuspendLayout();
            this.spcRequest.Panel2.SuspendLayout();
            this.spcRequest.SuspendLayout();
            this.tlpRequestCell2.SuspendLayout();
            this.tlpRequesCell2Cell1.SuspendLayout();
            this.tlpRequestCell3.SuspendLayout();
            this.tlpRequestCell3Cell1.SuspendLayout();
            this.stsRequest.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpRequest
            // 
            this.tlpRequest.ColumnCount = 1;
            this.tlpRequest.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRequest.Controls.Add(this.tlpRequestCell1, 0, 0);
            this.tlpRequest.Controls.Add(this.spcRequest, 0, 1);
            this.tlpRequest.Controls.Add(this.stsRequest, 0, 2);
            this.tlpRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRequest.Location = new System.Drawing.Point(0, 0);
            this.tlpRequest.Name = "tlpRequest";
            this.tlpRequest.RowCount = 3;
            this.tlpRequest.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpRequest.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRequest.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpRequest.Size = new System.Drawing.Size(784, 561);
            this.tlpRequest.TabIndex = 0;
            // 
            // tlpRequestCell1
            // 
            this.tlpRequestCell1.ColumnCount = 7;
            this.tlpRequestCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpRequestCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpRequestCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRequestCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpRequestCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpRequestCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRequestCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tlpRequestCell1.Controls.Add(this.btnClearProductNameRequestList, 0, 0);
            this.tlpRequestCell1.Controls.Add(this.btnImportRequestListFromExcel, 1, 0);
            this.tlpRequestCell1.Controls.Add(this.txtAddProductNameRequest, 3, 1);
            this.tlpRequestCell1.Controls.Add(this.lblAddProductNameRequest, 3, 0);
            this.tlpRequestCell1.Controls.Add(this.btnAddProductNameRequest, 4, 1);
            this.tlpRequestCell1.Controls.Add(this.btnClearProductNameDoneList, 6, 0);
            this.tlpRequestCell1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRequestCell1.Location = new System.Drawing.Point(3, 3);
            this.tlpRequestCell1.Name = "tlpRequestCell1";
            this.tlpRequestCell1.RowCount = 2;
            this.tlpRequestCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tlpRequestCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRequestCell1.Size = new System.Drawing.Size(778, 44);
            this.tlpRequestCell1.TabIndex = 0;
            // 
            // btnClearProductNameRequestList
            // 
            this.btnClearProductNameRequestList.BackgroundImage = global::blkEditor.Properties.Resources.delete;
            this.btnClearProductNameRequestList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClearProductNameRequestList.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClearProductNameRequestList.Location = new System.Drawing.Point(3, 3);
            this.btnClearProductNameRequestList.Name = "btnClearProductNameRequestList";
            this.tlpRequestCell1.SetRowSpan(this.btnClearProductNameRequestList, 2);
            this.btnClearProductNameRequestList.Size = new System.Drawing.Size(32, 32);
            this.btnClearProductNameRequestList.TabIndex = 0;
            this.ttpReques.SetToolTip(this.btnClearProductNameRequestList, "Xóa toàn bộ danh sách yêu cầu");
            this.btnClearProductNameRequestList.UseVisualStyleBackColor = true;
            this.btnClearProductNameRequestList.Click += new System.EventHandler(this.btnClearProductNameRequestList_Click);
            // 
            // btnImportRequestListFromExcel
            // 
            this.btnImportRequestListFromExcel.BackgroundImage = global::blkEditor.Properties.Resources.import;
            this.btnImportRequestListFromExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnImportRequestListFromExcel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnImportRequestListFromExcel.Location = new System.Drawing.Point(41, 3);
            this.btnImportRequestListFromExcel.Name = "btnImportRequestListFromExcel";
            this.tlpRequestCell1.SetRowSpan(this.btnImportRequestListFromExcel, 2);
            this.btnImportRequestListFromExcel.Size = new System.Drawing.Size(32, 32);
            this.btnImportRequestListFromExcel.TabIndex = 1;
            this.ttpReques.SetToolTip(this.btnImportRequestListFromExcel, "Nhập danh sách yêu cầu từ Excel");
            this.btnImportRequestListFromExcel.UseVisualStyleBackColor = true;
            this.btnImportRequestListFromExcel.Click += new System.EventHandler(this.btnImportRequestListFromExcel_Click);
            // 
            // txtAddProductNameRequest
            // 
            this.txtAddProductNameRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddProductNameRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddProductNameRequest.Location = new System.Drawing.Point(247, 20);
            this.txtAddProductNameRequest.Name = "txtAddProductNameRequest";
            this.txtAddProductNameRequest.Size = new System.Drawing.Size(294, 21);
            this.txtAddProductNameRequest.TabIndex = 2;
            this.ttpReques.SetToolTip(this.txtAddProductNameRequest, "Nhập tên SP cần yêu cầu và nhấn Enter để hoàn tất");
            this.txtAddProductNameRequest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddProductNameRequest_KeyDown);
            // 
            // btnAddProductNameRequest
            // 
            this.btnAddProductNameRequest.BackgroundImage = global::blkEditor.Properties.Resources.add;
            this.btnAddProductNameRequest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddProductNameRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddProductNameRequest.Location = new System.Drawing.Point(546, 19);
            this.btnAddProductNameRequest.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddProductNameRequest.Name = "btnAddProductNameRequest";
            this.btnAddProductNameRequest.Size = new System.Drawing.Size(23, 23);
            this.btnAddProductNameRequest.TabIndex = 4;
            this.ttpReques.SetToolTip(this.btnAddProductNameRequest, "Thêm vào danh sách yêu cầu");
            this.btnAddProductNameRequest.UseVisualStyleBackColor = true;
            this.btnAddProductNameRequest.Click += new System.EventHandler(this.btnAddProductNameRequest_Click);
            // 
            // lblAddProductNameRequest
            // 
            this.lblAddProductNameRequest.AutoSize = true;
            this.lblAddProductNameRequest.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAddProductNameRequest.Location = new System.Drawing.Point(247, 4);
            this.lblAddProductNameRequest.Name = "lblAddProductNameRequest";
            this.lblAddProductNameRequest.Size = new System.Drawing.Size(294, 13);
            this.lblAddProductNameRequest.TabIndex = 3;
            this.lblAddProductNameRequest.Text = "Thêm sản phẩm cần yêu cầu chụp:";
            // 
            // btnClearProductNameDoneList
            // 
            this.btnClearProductNameDoneList.BackgroundImage = global::blkEditor.Properties.Resources.delete;
            this.btnClearProductNameDoneList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClearProductNameDoneList.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClearProductNameDoneList.Location = new System.Drawing.Point(742, 3);
            this.btnClearProductNameDoneList.Name = "btnClearProductNameDoneList";
            this.tlpRequestCell1.SetRowSpan(this.btnClearProductNameDoneList, 2);
            this.btnClearProductNameDoneList.Size = new System.Drawing.Size(33, 32);
            this.btnClearProductNameDoneList.TabIndex = 5;
            this.ttpReques.SetToolTip(this.btnClearProductNameDoneList, "Xóa toàn bộ danh sách hoàn thành");
            this.btnClearProductNameDoneList.UseVisualStyleBackColor = true;
            this.btnClearProductNameDoneList.Click += new System.EventHandler(this.btnClearProductNameDoneList_Click);
            // 
            // spcRequest
            // 
            this.spcRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcRequest.Location = new System.Drawing.Point(3, 53);
            this.spcRequest.Name = "spcRequest";
            // 
            // spcRequest.Panel1
            // 
            this.spcRequest.Panel1.Controls.Add(this.tlpRequestCell2);
            // 
            // spcRequest.Panel2
            // 
            this.spcRequest.Panel2.Controls.Add(this.tlpRequestCell3);
            this.spcRequest.Size = new System.Drawing.Size(778, 480);
            this.spcRequest.SplitterDistance = 386;
            this.spcRequest.TabIndex = 1;
            // 
            // tlpRequestCell2
            // 
            this.tlpRequestCell2.ColumnCount = 1;
            this.tlpRequestCell2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRequestCell2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRequestCell2.Controls.Add(this.tlpRequesCell2Cell1, 0, 0);
            this.tlpRequestCell2.Controls.Add(this.lbxProductNameRequestList, 0, 1);
            this.tlpRequestCell2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRequestCell2.Location = new System.Drawing.Point(0, 0);
            this.tlpRequestCell2.Name = "tlpRequestCell2";
            this.tlpRequestCell2.RowCount = 2;
            this.tlpRequestCell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpRequestCell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRequestCell2.Size = new System.Drawing.Size(386, 480);
            this.tlpRequestCell2.TabIndex = 0;
            // 
            // tlpRequesCell2Cell1
            // 
            this.tlpRequesCell2Cell1.ColumnCount = 3;
            this.tlpRequesCell2Cell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRequesCell2Cell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpRequesCell2Cell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpRequesCell2Cell1.Controls.Add(this.btnClearProductNameRequest, 2, 0);
            this.tlpRequesCell2Cell1.Controls.Add(this.btnMarkProductAsDone, 1, 0);
            this.tlpRequesCell2Cell1.Controls.Add(this.lblProductNameRequestList, 0, 0);
            this.tlpRequesCell2Cell1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRequesCell2Cell1.Location = new System.Drawing.Point(3, 3);
            this.tlpRequesCell2Cell1.Name = "tlpRequesCell2Cell1";
            this.tlpRequesCell2Cell1.RowCount = 1;
            this.tlpRequesCell2Cell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRequesCell2Cell1.Size = new System.Drawing.Size(380, 27);
            this.tlpRequesCell2Cell1.TabIndex = 0;
            // 
            // btnClearProductNameRequest
            // 
            this.btnClearProductNameRequest.BackgroundImage = global::blkEditor.Properties.Resources.clear;
            this.btnClearProductNameRequest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClearProductNameRequest.Location = new System.Drawing.Point(355, 2);
            this.btnClearProductNameRequest.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearProductNameRequest.Name = "btnClearProductNameRequest";
            this.btnClearProductNameRequest.Size = new System.Drawing.Size(23, 23);
            this.btnClearProductNameRequest.TabIndex = 0;
            this.ttpReques.SetToolTip(this.btnClearProductNameRequest, "Xóa tên SP yêu cầu");
            this.btnClearProductNameRequest.UseVisualStyleBackColor = true;
            this.btnClearProductNameRequest.Click += new System.EventHandler(this.btnClearProductNameRequest_Click);
            // 
            // btnMarkProductAsDone
            // 
            this.btnMarkProductAsDone.BackgroundImage = global::blkEditor.Properties.Resources.mark;
            this.btnMarkProductAsDone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMarkProductAsDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMarkProductAsDone.Location = new System.Drawing.Point(328, 2);
            this.btnMarkProductAsDone.Margin = new System.Windows.Forms.Padding(2);
            this.btnMarkProductAsDone.Name = "btnMarkProductAsDone";
            this.btnMarkProductAsDone.Size = new System.Drawing.Size(23, 23);
            this.btnMarkProductAsDone.TabIndex = 1;
            this.ttpReques.SetToolTip(this.btnMarkProductAsDone, "Đánh dấu là đã hoàn thành");
            this.btnMarkProductAsDone.UseVisualStyleBackColor = true;
            this.btnMarkProductAsDone.Click += new System.EventHandler(this.btnMarkProductAsDone_Click);
            // 
            // lblProductNameRequestList
            // 
            this.lblProductNameRequestList.AutoSize = true;
            this.lblProductNameRequestList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblProductNameRequestList.Location = new System.Drawing.Point(3, 14);
            this.lblProductNameRequestList.Name = "lblProductNameRequestList";
            this.lblProductNameRequestList.Size = new System.Drawing.Size(320, 13);
            this.lblProductNameRequestList.TabIndex = 2;
            this.lblProductNameRequestList.Text = "Danh sách yêu cầu:";
            // 
            // lbxProductNameRequestList
            // 
            this.lbxProductNameRequestList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxProductNameRequestList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxProductNameRequestList.FormattingEnabled = true;
            this.lbxProductNameRequestList.ItemHeight = 15;
            this.lbxProductNameRequestList.Location = new System.Drawing.Point(3, 36);
            this.lbxProductNameRequestList.Name = "lbxProductNameRequestList";
            this.lbxProductNameRequestList.ScrollAlwaysVisible = true;
            this.lbxProductNameRequestList.Size = new System.Drawing.Size(380, 441);
            this.lbxProductNameRequestList.TabIndex = 1;
            this.lbxProductNameRequestList.SelectedIndexChanged += new System.EventHandler(this.lbxProductNameRequestList_SelectedIndexChanged);
            this.lbxProductNameRequestList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbxProductNameRequestList_KeyDown);
            // 
            // tlpRequestCell3
            // 
            this.tlpRequestCell3.ColumnCount = 1;
            this.tlpRequestCell3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRequestCell3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRequestCell3.Controls.Add(this.tlpRequestCell3Cell1, 0, 0);
            this.tlpRequestCell3.Controls.Add(this.lbxProductNameDoneList, 0, 1);
            this.tlpRequestCell3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRequestCell3.Location = new System.Drawing.Point(0, 0);
            this.tlpRequestCell3.Name = "tlpRequestCell3";
            this.tlpRequestCell3.RowCount = 2;
            this.tlpRequestCell3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpRequestCell3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRequestCell3.Size = new System.Drawing.Size(388, 480);
            this.tlpRequestCell3.TabIndex = 0;
            // 
            // tlpRequestCell3Cell1
            // 
            this.tlpRequestCell3Cell1.ColumnCount = 4;
            this.tlpRequestCell3Cell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRequestCell3Cell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpRequestCell3Cell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpRequestCell3Cell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpRequestCell3Cell1.Controls.Add(this.btnClearProductNameDone, 3, 0);
            this.tlpRequestCell3Cell1.Controls.Add(this.btnProductNameRequestAgain, 2, 0);
            this.tlpRequestCell3Cell1.Controls.Add(this.btnEditWithThisProduct, 1, 0);
            this.tlpRequestCell3Cell1.Controls.Add(this.lblProductNameDoneList, 0, 0);
            this.tlpRequestCell3Cell1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRequestCell3Cell1.Location = new System.Drawing.Point(3, 3);
            this.tlpRequestCell3Cell1.Name = "tlpRequestCell3Cell1";
            this.tlpRequestCell3Cell1.RowCount = 1;
            this.tlpRequestCell3Cell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRequestCell3Cell1.Size = new System.Drawing.Size(382, 27);
            this.tlpRequestCell3Cell1.TabIndex = 0;
            // 
            // btnClearProductNameDone
            // 
            this.btnClearProductNameDone.BackgroundImage = global::blkEditor.Properties.Resources.clear;
            this.btnClearProductNameDone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClearProductNameDone.Location = new System.Drawing.Point(357, 2);
            this.btnClearProductNameDone.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearProductNameDone.Name = "btnClearProductNameDone";
            this.btnClearProductNameDone.Size = new System.Drawing.Size(23, 23);
            this.btnClearProductNameDone.TabIndex = 0;
            this.ttpReques.SetToolTip(this.btnClearProductNameDone, "Xóa tên SP hoàn thành");
            this.btnClearProductNameDone.UseVisualStyleBackColor = true;
            this.btnClearProductNameDone.Click += new System.EventHandler(this.btnClearProductNameDone_Click);
            // 
            // btnProductNameRequestAgain
            // 
            this.btnProductNameRequestAgain.BackgroundImage = global::blkEditor.Properties.Resources.restore;
            this.btnProductNameRequestAgain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnProductNameRequestAgain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnProductNameRequestAgain.Location = new System.Drawing.Point(330, 2);
            this.btnProductNameRequestAgain.Margin = new System.Windows.Forms.Padding(2);
            this.btnProductNameRequestAgain.Name = "btnProductNameRequestAgain";
            this.btnProductNameRequestAgain.Size = new System.Drawing.Size(23, 23);
            this.btnProductNameRequestAgain.TabIndex = 1;
            this.ttpReques.SetToolTip(this.btnProductNameRequestAgain, "Yêu cầu chụp lại");
            this.btnProductNameRequestAgain.UseVisualStyleBackColor = true;
            this.btnProductNameRequestAgain.Click += new System.EventHandler(this.btnProductNameRequestAgain_Click);
            // 
            // btnEditWithThisProduct
            // 
            this.btnEditWithThisProduct.BackgroundImage = global::blkEditor.Properties.Resources.edit;
            this.btnEditWithThisProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEditWithThisProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEditWithThisProduct.Location = new System.Drawing.Point(303, 2);
            this.btnEditWithThisProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditWithThisProduct.Name = "btnEditWithThisProduct";
            this.btnEditWithThisProduct.Size = new System.Drawing.Size(23, 23);
            this.btnEditWithThisProduct.TabIndex = 2;
            this.ttpReques.SetToolTip(this.btnEditWithThisProduct, "Chỉnh sửa với sản phẩm này");
            this.btnEditWithThisProduct.UseVisualStyleBackColor = true;
            this.btnEditWithThisProduct.Click += new System.EventHandler(this.btnEditWithThisProduct_Click);
            // 
            // lblProductNameDoneList
            // 
            this.lblProductNameDoneList.AutoSize = true;
            this.lblProductNameDoneList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblProductNameDoneList.Location = new System.Drawing.Point(3, 14);
            this.lblProductNameDoneList.Name = "lblProductNameDoneList";
            this.lblProductNameDoneList.Size = new System.Drawing.Size(295, 13);
            this.lblProductNameDoneList.TabIndex = 3;
            this.lblProductNameDoneList.Text = "Danh sách hoàn thành:";
            // 
            // lbxProductNameDoneList
            // 
            this.lbxProductNameDoneList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxProductNameDoneList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxProductNameDoneList.FormattingEnabled = true;
            this.lbxProductNameDoneList.ItemHeight = 15;
            this.lbxProductNameDoneList.Location = new System.Drawing.Point(3, 36);
            this.lbxProductNameDoneList.Name = "lbxProductNameDoneList";
            this.lbxProductNameDoneList.ScrollAlwaysVisible = true;
            this.lbxProductNameDoneList.Size = new System.Drawing.Size(382, 441);
            this.lbxProductNameDoneList.TabIndex = 1;
            this.lbxProductNameDoneList.SelectedIndexChanged += new System.EventHandler(this.lbxProductNameDoneList_SelectedIndexChanged);
            this.lbxProductNameDoneList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbxProductNameDoneList_KeyDown);
            // 
            // stsRequest
            // 
            this.stsRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stsRequest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspImport,
            this.tslStatus});
            this.stsRequest.Location = new System.Drawing.Point(0, 536);
            this.stsRequest.Name = "stsRequest";
            this.stsRequest.Size = new System.Drawing.Size(784, 25);
            this.stsRequest.TabIndex = 2;
            this.stsRequest.Text = "stsRequest";
            // 
            // tspImport
            // 
            this.tspImport.Name = "tspImport";
            this.tspImport.Size = new System.Drawing.Size(100, 19);
            this.tspImport.Visible = false;
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(54, 20);
            this.tslStatus.Text = "Sẵn sàng";
            // 
            // tmrMonitorUI
            // 
            this.tmrMonitorUI.Tick += new System.EventHandler(this.tmrMonitorUI_Tick);
            // 
            // tmrHideMessage
            // 
            this.tmrHideMessage.Interval = 1500;
            this.tmrHideMessage.Tick += new System.EventHandler(this.tmrHideMessage_Tick);
            // 
            // bgwImportFromExcel
            // 
            this.bgwImportFromExcel.WorkerReportsProgress = true;
            this.bgwImportFromExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwImportFromExcel_DoWork);
            this.bgwImportFromExcel.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwImportFromExcel_ProgressChanged);
            this.bgwImportFromExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwImportFromExcel_RunWorkerCompleted);
            // 
            // frmRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tlpRequest);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmRequest";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BanLinhKien Request";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRequest_FormClosed);
            this.Load += new System.EventHandler(this.frmRequest_Load);
            this.Shown += new System.EventHandler(this.frmRequest_Shown);
            this.tlpRequest.ResumeLayout(false);
            this.tlpRequest.PerformLayout();
            this.tlpRequestCell1.ResumeLayout(false);
            this.tlpRequestCell1.PerformLayout();
            this.spcRequest.Panel1.ResumeLayout(false);
            this.spcRequest.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcRequest)).EndInit();
            this.spcRequest.ResumeLayout(false);
            this.tlpRequestCell2.ResumeLayout(false);
            this.tlpRequesCell2Cell1.ResumeLayout(false);
            this.tlpRequesCell2Cell1.PerformLayout();
            this.tlpRequestCell3.ResumeLayout(false);
            this.tlpRequestCell3Cell1.ResumeLayout(false);
            this.tlpRequestCell3Cell1.PerformLayout();
            this.stsRequest.ResumeLayout(false);
            this.stsRequest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRequest;
        private System.Windows.Forms.SplitContainer spcRequest;
        private System.Windows.Forms.TableLayoutPanel tlpRequestCell2;
        private System.Windows.Forms.TableLayoutPanel tlpRequestCell3;
        private System.Windows.Forms.TableLayoutPanel tlpRequestCell1;
        private System.Windows.Forms.TextBox txtAddProductNameRequest;
        private System.Windows.Forms.Label lblAddProductNameRequest;
        public System.Windows.Forms.ListBox lbxProductNameRequestList;
        public System.Windows.Forms.ListBox lbxProductNameDoneList;
        private System.Windows.Forms.Button btnAddProductNameRequest;
        private System.Windows.Forms.TableLayoutPanel tlpRequesCell2Cell1;
        private System.Windows.Forms.TableLayoutPanel tlpRequestCell3Cell1;
        private System.Windows.Forms.Label lblProductNameRequestList;
        private System.Windows.Forms.Label lblProductNameDoneList;
        private System.Windows.Forms.ToolTip ttpReques;
        private System.Windows.Forms.Button btnClearProductNameRequestList;
        private System.Windows.Forms.Button btnClearProductNameDoneList;
        private System.Windows.Forms.Button btnClearProductNameRequest;
        private System.Windows.Forms.Button btnClearProductNameDone;
        private System.Windows.Forms.Button btnProductNameRequestAgain;
        private System.Windows.Forms.Timer tmrMonitorUI;
        private System.Windows.Forms.StatusStrip stsRequest;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.Timer tmrHideMessage;
        private System.Windows.Forms.Button btnEditWithThisProduct;
        private System.Windows.Forms.Button btnMarkProductAsDone;
        private System.Windows.Forms.Button btnImportRequestListFromExcel;
        private System.ComponentModel.BackgroundWorker bgwImportFromExcel;
        private System.Windows.Forms.ToolStripProgressBar tspImport;
    }
}
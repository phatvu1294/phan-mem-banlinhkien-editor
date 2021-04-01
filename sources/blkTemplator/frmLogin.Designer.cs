namespace blkTemplator
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.tlpLogin = new System.Windows.Forms.TableLayoutPanel();
            this.tlpLoginCell1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.stsLogin = new System.Windows.Forms.StatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlpLoginCell2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tmrHideMessage = new System.Windows.Forms.Timer(this.components);
            this.tlpLogin.SuspendLayout();
            this.tlpLoginCell1.SuspendLayout();
            this.stsLogin.SuspendLayout();
            this.tlpLoginCell2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpLogin
            // 
            this.tlpLogin.ColumnCount = 1;
            this.tlpLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLogin.Controls.Add(this.tlpLoginCell1, 0, 1);
            this.tlpLogin.Controls.Add(this.tlpLoginCell2, 0, 2);
            this.tlpLogin.Controls.Add(this.stsLogin, 0, 3);
            this.tlpLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLogin.Location = new System.Drawing.Point(0, 0);
            this.tlpLogin.Name = "tlpLogin";
            this.tlpLogin.RowCount = 4;
            this.tlpLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tlpLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpLogin.Size = new System.Drawing.Size(344, 201);
            this.tlpLogin.TabIndex = 0;
            // 
            // tlpLoginCell1
            // 
            this.tlpLoginCell1.ColumnCount = 4;
            this.tlpLoginCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoginCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpLoginCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpLoginCell1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoginCell1.Controls.Add(this.txtUsername, 2, 0);
            this.tlpLoginCell1.Controls.Add(this.lblUsername, 1, 0);
            this.tlpLoginCell1.Controls.Add(this.txtPassword, 2, 1);
            this.tlpLoginCell1.Controls.Add(this.lblPassword, 1, 1);
            this.tlpLoginCell1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLoginCell1.Location = new System.Drawing.Point(3, 59);
            this.tlpLoginCell1.Name = "tlpLoginCell1";
            this.tlpLoginCell1.RowCount = 2;
            this.tlpLoginCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoginCell1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoginCell1.Size = new System.Drawing.Size(338, 58);
            this.tlpLoginCell1.TabIndex = 0;
            // 
            // lblUsername
            // 
            this.lblUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUsername.Location = new System.Drawing.Point(22, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(94, 29);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Tên người dùng: ";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPassword
            // 
            this.lblPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPassword.Location = new System.Drawing.Point(22, 29);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(94, 29);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Mật khẩu:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUsername
            // 
            this.txtUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(122, 3);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(194, 23);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.Enter += new System.EventHandler(this.txt_Enter);
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(122, 32);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(194, 23);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Enter += new System.EventHandler(this.txt_Enter);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // stsLogin
            // 
            this.stsLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stsLogin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.stsLogin.Location = new System.Drawing.Point(0, 176);
            this.stsLogin.Name = "stsLogin";
            this.stsLogin.Size = new System.Drawing.Size(344, 25);
            this.stsLogin.SizingGrip = false;
            this.stsLogin.TabIndex = 2;
            this.stsLogin.Text = "stsLogin";
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(111, 20);
            this.tslStatus.Text = "Vui lòng đăng nhập";
            // 
            // tlpLoginCell2
            // 
            this.tlpLoginCell2.ColumnCount = 4;
            this.tlpLoginCell2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoginCell2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpLoginCell2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpLoginCell2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoginCell2.Controls.Add(this.btnLogin, 2, 0);
            this.tlpLoginCell2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLoginCell2.Location = new System.Drawing.Point(3, 123);
            this.tlpLoginCell2.Name = "tlpLoginCell2";
            this.tlpLoginCell2.RowCount = 2;
            this.tlpLoginCell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tlpLoginCell2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLoginCell2.Size = new System.Drawing.Size(338, 50);
            this.tlpLoginCell2.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogin.Location = new System.Drawing.Point(222, 3);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(94, 25);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tmrHideMessage
            // 
            this.tmrHideMessage.Enabled = true;
            this.tmrHideMessage.Interval = 1500;
            this.tmrHideMessage.Tick += new System.EventHandler(this.tmrHideMessage_Tick);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 201);
            this.Controls.Add(this.tlpLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đăng nhập";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Shown += new System.EventHandler(this.frmLogin_Shown);
            this.tlpLogin.ResumeLayout(false);
            this.tlpLogin.PerformLayout();
            this.tlpLoginCell1.ResumeLayout(false);
            this.tlpLoginCell1.PerformLayout();
            this.stsLogin.ResumeLayout(false);
            this.stsLogin.PerformLayout();
            this.tlpLoginCell2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpLogin;
        private System.Windows.Forms.TableLayoutPanel tlpLoginCell1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.StatusStrip stsLogin;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.Timer tmrHideMessage;
        private System.Windows.Forms.TableLayoutPanel tlpLoginCell2;
        private System.Windows.Forms.Button btnLogin;
    }
}
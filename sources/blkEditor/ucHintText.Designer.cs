namespace blkEditor
{
    partial class ucHintText
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtHintText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtHintText
            // 
            this.txtHintText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHintText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHintText.Location = new System.Drawing.Point(3, 3);
            this.txtHintText.Name = "txtHintText";
            this.txtHintText.Size = new System.Drawing.Size(522, 142);
            this.txtHintText.TabIndex = 0;
            this.txtHintText.Text = "";
            // 
            // ucHintText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtHintText);
            this.DoubleBuffered = true;
            this.Name = "ucHintText";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(528, 148);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtHintText;
    }
}

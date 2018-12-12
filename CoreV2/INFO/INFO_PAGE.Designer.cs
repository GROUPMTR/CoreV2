namespace CoreV2.INFO
{
    partial class INFO_PAGE
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
            this.MEMOS_INFO = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.MEMOS_INFO.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // MEMOS_INFO
            // 
            this.MEMOS_INFO.Location = new System.Drawing.Point(12, 29);
            this.MEMOS_INFO.Name = "MEMOS_INFO";
            this.MEMOS_INFO.Size = new System.Drawing.Size(589, 232);
            this.MEMOS_INFO.TabIndex = 0;
            // 
            // INFO_PAGE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 286);
            this.Controls.Add(this.MEMOS_INFO);
            this.Name = "INFO_PAGE";
            this.Text = "INFO_PAGE";
            this.Load += new System.EventHandler(this.INFO_PAGE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MEMOS_INFO.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit MEMOS_INFO;
    }
}
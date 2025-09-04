namespace RegistrationForm1
{
    partial class FrmThongtin
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
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.lblUploadTimestamp = new System.Windows.Forms.Label();
            this.btnDownloadFile = new System.Windows.Forms.Button();
            this.btnOpenFileExternal = new System.Windows.Forms.Button();
            this.lblLoadDuration = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.rtbFileContent = new System.Windows.Forms.RichTextBox();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.trvFiles = new System.Windows.Forms.TreeView();
            this.btnUploadFile = new System.Windows.Forms.Button();
            this.pnlMainContent.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.Controls.Add(this.lblUploadTimestamp);
            this.pnlMainContent.Controls.Add(this.btnDownloadFile);
            this.pnlMainContent.Controls.Add(this.btnOpenFileExternal);
            this.pnlMainContent.Controls.Add(this.lblLoadDuration);
            this.pnlMainContent.Controls.Add(this.lblFileName);
            this.pnlMainContent.Controls.Add(this.rtbFileContent);
            this.pnlMainContent.Location = new System.Drawing.Point(280, -21);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(1418, 891);
            this.pnlMainContent.TabIndex = 10;
            // 
            // lblUploadTimestamp
            // 
            this.lblUploadTimestamp.AutoSize = true;
            this.lblUploadTimestamp.Location = new System.Drawing.Point(26, 110);
            this.lblUploadTimestamp.Name = "lblUploadTimestamp";
            this.lblUploadTimestamp.Size = new System.Drawing.Size(85, 13);
            this.lblUploadTimestamp.TabIndex = 9;
            this.lblUploadTimestamp.Text = "Thời gian tải lên:";
            // 
            // btnDownloadFile
            // 
            this.btnDownloadFile.Location = new System.Drawing.Point(416, 187);
            this.btnDownloadFile.Name = "btnDownloadFile";
            this.btnDownloadFile.Size = new System.Drawing.Size(75, 23);
            this.btnDownloadFile.TabIndex = 8;
            this.btnDownloadFile.Text = "Tải về";
            this.btnDownloadFile.UseVisualStyleBackColor = true;
            // 
            // btnOpenFileExternal
            // 
            this.btnOpenFileExternal.Location = new System.Drawing.Point(200, 232);
            this.btnOpenFileExternal.Name = "btnOpenFileExternal";
            this.btnOpenFileExternal.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFileExternal.TabIndex = 7;
            this.btnOpenFileExternal.Text = "Mở file";
            this.btnOpenFileExternal.UseVisualStyleBackColor = true;
            // 
            // lblLoadDuration
            // 
            this.lblLoadDuration.AutoSize = true;
            this.lblLoadDuration.Location = new System.Drawing.Point(26, 81);
            this.lblLoadDuration.Name = "lblLoadDuration";
            this.lblLoadDuration.Size = new System.Drawing.Size(68, 13);
            this.lblLoadDuration.TabIndex = 1;
            this.lblLoadDuration.Text = "Thời gian tải:";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(35, 32);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(47, 13);
            this.lblFileName.TabIndex = 0;
            this.lblFileName.Text = "Tên tệp:";
            // 
            // rtbFileContent
            // 
            this.rtbFileContent.Location = new System.Drawing.Point(3, 126);
            this.rtbFileContent.Name = "rtbFileContent";
            this.rtbFileContent.Size = new System.Drawing.Size(671, 452);
            this.rtbFileContent.TabIndex = 0;
            this.rtbFileContent.Text = "";
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.Controls.Add(this.trvFiles);
            this.pnlSidebar.Controls.Add(this.btnUploadFile);
            this.pnlSidebar.Location = new System.Drawing.Point(12, -21);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(262, 620);
            this.pnlSidebar.TabIndex = 9;
            // 
            // trvFiles
            // 
            this.trvFiles.Location = new System.Drawing.Point(21, 114);
            this.trvFiles.Name = "trvFiles";
            this.trvFiles.Size = new System.Drawing.Size(199, 303);
            this.trvFiles.TabIndex = 1;
            // 
            // btnUploadFile
            // 
            this.btnUploadFile.Location = new System.Drawing.Point(12, 32);
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.Size = new System.Drawing.Size(120, 23);
            this.btnUploadFile.TabIndex = 0;
            this.btnUploadFile.Text = "Tải tệp lên...";
            this.btnUploadFile.UseVisualStyleBackColor = true;
            // 
            // FrmThongtin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1710, 848);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "FrmThongtin";
            this.Text = "FrmThongtin";
            this.pnlMainContent.ResumeLayout(false);
            this.pnlMainContent.PerformLayout();
            this.pnlSidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Label lblUploadTimestamp;
        private System.Windows.Forms.Button btnDownloadFile;
        private System.Windows.Forms.Button btnOpenFileExternal;
        private System.Windows.Forms.Label lblLoadDuration;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.RichTextBox rtbFileContent;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.TreeView trvFiles;
        private System.Windows.Forms.Button btnUploadFile;
    }
}
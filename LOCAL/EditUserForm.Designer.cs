namespace RegistrationForm1
{
    partial class EditUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditUserForm));
            this.cmbPosition = new System.Windows.Forms.ComboBox();
            this.dtpBirth = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.ahdPictureBox1 = new Ahd.Winforms.Controls.AhdPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ahdPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPosition
            // 
            this.cmbPosition.FormattingEnabled = true;
            this.cmbPosition.Items.AddRange(new object[] {
            "Quản trị viên ",
            "Nhân viên ",
            "Vận hành bảo trì"});
            this.cmbPosition.Location = new System.Drawing.Point(122, 277);
            this.cmbPosition.Name = "cmbPosition";
            this.cmbPosition.Size = new System.Drawing.Size(368, 23);
            this.cmbPosition.TabIndex = 37;
            // 
            // dtpBirth
            // 
            this.dtpBirth.Location = new System.Drawing.Point(122, 217);
            this.dtpBirth.Name = "dtpBirth";
            this.dtpBirth.Size = new System.Drawing.Size(368, 22);
            this.dtpBirth.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(122, 247);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 22);
            this.label6.TabIndex = 35;
            this.label6.Text = "Chức vụ:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(122, 338);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 43);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(122, 157);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(368, 22);
            this.txtFullName.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(122, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 22);
            this.label3.TabIndex = 28;
            this.label3.Text = "Ngày sinh:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(122, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 22);
            this.label2.TabIndex = 27;
            this.label2.Text = "Họ và tên:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(177, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 74);
            this.label1.TabIndex = 26;
            this.label1.Text = "Thay Đổi Thông Tin Người Dùng";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(122, 97);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(368, 22);
            this.txtUserID.TabIndex = 40;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(122, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 22);
            this.label8.TabIndex = 38;
            this.label8.Text = "Mã người dùng:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(369, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(121, 43);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(122, 308);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(85, 19);
            this.chkIsActive.TabIndex = 42;
            this.chkIsActive.Text = "Trạng Thái";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // ahdPictureBox1
            // 
            this.ahdPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.ahdPictureBox1.FillMode = Ahd.Winforms.Controls.ImageFillMode.Original;
            this.ahdPictureBox1.FlipMode = Ahd.Winforms.Controls.ImageFlipMode.None;
            this.ahdPictureBox1.Image = global::RegistrationForm1.Properties.Resources.logo_removebg_preview;
            this.ahdPictureBox1.Location = new System.Drawing.Point(5, 6);
            this.ahdPictureBox1.Name = "ahdPictureBox1";
            this.ahdPictureBox1.RotateAngle = 0;
            this.ahdPictureBox1.ShadedColor = System.Drawing.Color.Gray;
            this.ahdPictureBox1.Size = new System.Drawing.Size(100, 100);
            this.ahdPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.ahdPictureBox1.TabIndex = 43;
            this.ahdPictureBox1.TagPath = null;
            this.ahdPictureBox1.Text = "ahdPictureBox1";
            // 
            // EditUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(605, 419);
            this.Controls.Add(this.ahdPictureBox1);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbPosition);
            this.Controls.Add(this.dtpBirth);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông Tin Người Dùng";
            this.Load += new System.EventHandler(this.EditUserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ahdPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPosition;
        private System.Windows.Forms.DateTimePicker dtpBirth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkIsActive;
        private Ahd.Winforms.Controls.AhdPictureBox ahdPictureBox1;
    }
}
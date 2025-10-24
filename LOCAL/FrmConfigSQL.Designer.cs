namespace RegistrationForm1
{
    partial class FrmConfigSQL
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.grpAuth = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.rdoSqlAuth = new System.Windows.Forms.RadioButton();
            this.rdoWindowsAuth = new System.Windows.Forms.RadioButton();
            this.txtInitialCatalog = new System.Windows.Forms.TextBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.grpAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(184, 281);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 28);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(98, 281);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(80, 28);
            this.btnTestConnection.TabIndex = 21;
            this.btnTestConnection.Text = "Kiểm tra";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // grpAuth
            // 
            this.grpAuth.Controls.Add(this.txtPassword);
            this.grpAuth.Controls.Add(this.lblPassword);
            this.grpAuth.Controls.Add(this.txtUserID);
            this.grpAuth.Controls.Add(this.lblUser);
            this.grpAuth.Controls.Add(this.rdoSqlAuth);
            this.grpAuth.Controls.Add(this.rdoWindowsAuth);
            this.grpAuth.Location = new System.Drawing.Point(98, 135);
            this.grpAuth.Name = "grpAuth";
            this.grpAuth.Size = new System.Drawing.Size(355, 128);
            this.grpAuth.TabIndex = 20;
            this.grpAuth.TabStop = false;
            this.grpAuth.Text = "Authentication";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(109, 93);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(228, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(44, 96);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(109, 67);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(228, 20);
            this.txtUserID.TabIndex = 3;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(44, 70);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(61, 13);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "User name:";
            // 
            // rdoSqlAuth
            // 
            this.rdoSqlAuth.AutoSize = true;
            this.rdoSqlAuth.Location = new System.Drawing.Point(19, 44);
            this.rdoSqlAuth.Name = "rdoSqlAuth";
            this.rdoSqlAuth.Size = new System.Drawing.Size(151, 17);
            this.rdoSqlAuth.TabIndex = 1;
            this.rdoSqlAuth.Text = "SQL Server Authentication";
            this.rdoSqlAuth.UseVisualStyleBackColor = true;
            // 
            // rdoWindowsAuth
            // 
            this.rdoWindowsAuth.AutoSize = true;
            this.rdoWindowsAuth.Checked = true;
            this.rdoWindowsAuth.Location = new System.Drawing.Point(19, 21);
            this.rdoWindowsAuth.Name = "rdoWindowsAuth";
            this.rdoWindowsAuth.Size = new System.Drawing.Size(140, 17);
            this.rdoWindowsAuth.TabIndex = 0;
            this.rdoWindowsAuth.TabStop = true;
            this.rdoWindowsAuth.Text = "Windows Authentication";
            this.rdoWindowsAuth.UseVisualStyleBackColor = true;
            // 
            // txtInitialCatalog
            // 
            this.txtInitialCatalog.Location = new System.Drawing.Point(185, 95);
            this.txtInitialCatalog.Name = "txtInitialCatalog";
            this.txtInitialCatalog.Size = new System.Drawing.Size(268, 20);
            this.txtInitialCatalog.TabIndex = 19;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(95, 98);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(87, 13);
            this.lblDatabase.TabIndex = 18;
            this.lblDatabase.Text = "Database Name:";
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(185, 69);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(268, 20);
            this.txtDataSource.TabIndex = 17;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(95, 72);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(72, 13);
            this.lblServer.TabIndex = 16;
            this.lblServer.Text = "Server Name:";
            // 
            // FrmConfigSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.grpAuth);
            this.Controls.Add(this.txtInitialCatalog);
            this.Controls.Add(this.lblDatabase);
            this.Controls.Add(this.txtDataSource);
            this.Controls.Add(this.lblServer);
            this.Name = "FrmConfigSQL";
            this.Text = "FrmConfigSQL";
     
            this.grpAuth.ResumeLayout(false);
            this.grpAuth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.GroupBox grpAuth;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.RadioButton rdoSqlAuth;
        private System.Windows.Forms.RadioButton rdoWindowsAuth;
        private System.Windows.Forms.TextBox txtInitialCatalog;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.Label lblServer;
    }
}
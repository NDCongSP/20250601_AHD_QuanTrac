namespace RegistrationForm1
{
    partial class FrmCanhBao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCanhBao));
            this.dgvCanhBao = new System.Windows.Forms.DataGridView();
            this.ahdDriverConnector1 = new Ahd.Winforms.Controls.AhdDriverConnector(this.components);
            this.bntLoad = new System.Windows.Forms.Button();
            this.DgvHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdDriverConnector1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCanhBao
            // 
            this.dgvCanhBao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCanhBao.Location = new System.Drawing.Point(4, 14);
            this.dgvCanhBao.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dgvCanhBao.Name = "dgvCanhBao";
            this.dgvCanhBao.RowHeadersWidth = 82;
            this.dgvCanhBao.Size = new System.Drawing.Size(836, 627);
            this.dgvCanhBao.TabIndex = 0;
            // 
            // ahdDriverConnector1
            // 
            this.ahdDriverConnector1.CollectionName = null;
            this.ahdDriverConnector1.CommunicationMode = Ahd.Core.CommunicationMode.ReceiveFromServer;
            this.ahdDriverConnector1.DatabaseName = null;
            this.ahdDriverConnector1.MongoDb_ConnectionString = null;
            this.ahdDriverConnector1.Port = ((ushort)(8800));
            this.ahdDriverConnector1.RefreshRate = 1000;
            this.ahdDriverConnector1.ServerAddress = "127.0.0.1";
            this.ahdDriverConnector1.StationName = null;
            this.ahdDriverConnector1.Timeout = 30;
            this.ahdDriverConnector1.UseMongoDb = false;
            // 
            // bntLoad
            // 
            this.bntLoad.Location = new System.Drawing.Point(906, 733);
            this.bntLoad.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.bntLoad.Name = "bntLoad";
            this.bntLoad.Size = new System.Drawing.Size(125, 37);
            this.bntLoad.TabIndex = 1;
            this.bntLoad.Text = "Load Data";
            this.bntLoad.UseVisualStyleBackColor = true;
            this.bntLoad.Click += new System.EventHandler(this.bntLoad_Click);
            // 
            // DgvHistory
            // 
            this.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvHistory.Location = new System.Drawing.Point(862, 14);
            this.DgvHistory.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.DgvHistory.Name = "DgvHistory";
            this.DgvHistory.Size = new System.Drawing.Size(630, 627);
            this.DgvHistory.TabIndex = 2;
            // 
            // FrmCanhBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1710, 848);
            this.Controls.Add(this.DgvHistory);
            this.Controls.Add(this.bntLoad);
            this.Controls.Add(this.dgvCanhBao);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmCanhBao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cảnh Báo";
            this.Load += new System.EventHandler(this.FrmCanhBao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ahdDriverConnector1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCanhBao;
        private Ahd.Winforms.Controls.AhdDriverConnector ahdDriverConnector1;
        private System.Windows.Forms.Button bntLoad;
        private System.Windows.Forms.DataGridView DgvHistory;
    }
}
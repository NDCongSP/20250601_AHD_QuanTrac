using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class NoteForm: Form
    {
        public string NoteText { get; private set; }
        public bool IsForAllDevices { get; private set; }
        public NoteForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNote.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung ghi chú!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NoteText = txtNote.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

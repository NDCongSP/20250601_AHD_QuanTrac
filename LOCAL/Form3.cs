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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            var config = Globalvariable.ConfigSystem;

            Load += Form3_Load;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.Text = Globalvariable.TagsValues.FirstOrDefault(x => x.TagName == "A")?.Value ?? "0"; // Hiển thị giá trị của TagName "A" trong TextBox
        }
    }
}

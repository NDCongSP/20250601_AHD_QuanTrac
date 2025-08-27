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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            Load += Form2_Load;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var config = Globalvariable.ConfigSystem;

            button1.Click+=(s, args) =>
            {
                  //Globalvariable.TagsValues.FirstOrDefault(x=>x.TagName=="A").Value = "101"; // Cập nhật giá trị của TagName "A" thành "1"
               

                using (var form3 = new Form3())
                {
                    form3.ShowDialog();
                }
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var dbContext=new ApplicationDbContext())
            {
                var ft01 = dbContext.FT01s.FirstOrDefault();
                if (ft01 != null)
                {
                    MessageBox.Show("FT01 record found.");
                }
                else
                {
                    MessageBox.Show("No FT01 records found.");
                }
            }
        }
    }
}

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
    public partial class FrmCS1 : Form
    {
       // private Timer _timer = new Timer();
        public FrmCS1()
        {
            InitializeComponent();
           
        }
        public string Url { get; set; }

        private async void FrmCS1_Load(object sender, EventArgs e)
        {
            try
            {
                await webView21.EnsureCoreWebView2Async(null);
                webView21.Source = new Uri("https://simc.id.vn/simc_dti/cs1/coso1.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

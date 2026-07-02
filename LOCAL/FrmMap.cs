using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;


namespace RegistrationForm1
{
    public partial class FrmMap : Form
    {
      //  public string UrlToLoad { get; set; }
        public FrmMap()
        {
            InitializeComponent();
        }

        private  async void FrmMap_Load(object sender, EventArgs e)
        {
            // Khởi tạo WebView2
            await webView21.EnsureCoreWebView2Async(null);

            // Nhúng website
            webView21.Source = new Uri("https://floodcam.baonamdts.com/embed/dau-tieng-flood-scenario");
        }
    }
}

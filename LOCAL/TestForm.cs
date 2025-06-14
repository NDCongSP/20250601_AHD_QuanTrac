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
    public partial class TestForm : Form
    {
        private User currentUser;
        public TestForm(User loggedInUser)
        {
            this.currentUser = loggedInUser;
            InitializeComponent();
        }
    }
}

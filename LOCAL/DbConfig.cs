using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class DbConfig
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public bool UseSqlAuthentication { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Cân nhắc mã hóa mật khẩu khi lưu
    }
}

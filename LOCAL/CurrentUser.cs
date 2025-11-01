using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public static class CurrentUser
    {
        public static Guid Id { get; set; }
        public static string UserName { get; set; }
        public static string FullName { get; set; }
        public static EnumPermissionScada? PermissionScada { get; set; }
        public static DateTime LoginTime { get; set; }
    }
}

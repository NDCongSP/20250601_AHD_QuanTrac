using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public static class PermissionManager
    {
        public static string CurrentUsername { get; private set; }
        public static string CurrentUserRole { get; private set; }

        // Danh sách quyền cho từng vai trò
        private static readonly Dictionary<string, List<string>> RolePermissions = new Dictionary<string, List<string>>()
        {
            { "Admin", new List<string> { "add_user", "delete_user", "edit_user", "view_data", "export_data" } },
            { "User", new List<string> { "view_data", "export_data" } },
            { "Viewer", new List<string> { "view_data" } }
        };

        public static void SetUser(string username, string role)
        {
            CurrentUsername = username;
            CurrentUserRole = role;
        }

        /// <summary>
        /// Kiểm tra người dùng hiện tại có quyền thực hiện hành động không.
        /// </summary>
        public static bool HasPermission(string action)
        {
            if (string.IsNullOrEmpty(CurrentUserRole))
                return false;

            if (RolePermissions.ContainsKey(CurrentUserRole))
            {
                return RolePermissions[CurrentUserRole].Contains(action);
            }

            return false;
        }

        /// <summary>
        /// Kiểm tra quyền và hiện cảnh báo nếu không có quyền.
        /// </summary>
        public static bool CheckPermissionWithMessage(string action)
        {
            if (!HasPermission(action))
            {
                MessageBox.Show(
                    $"Tài khoản \"{CurrentUsername}\" ({CurrentUserRole}) không có quyền thực hiện chức năng này.",
                    "Không có quyền truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }
            return true;
        }
    }
}
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
    { "Admin", new List<string> { "add_user", "delete_user", "edit_user", "view_data", "edit_data" } },
    { "Quản Lý", new List<string> { "add_user", "view_data", "export_data", "edit_data" } },
    { "Vận Hành", new List<string> { "view_data", "export_data" } },
    { "Bảo Trì", new List<string> { "view_data" } }
};

        public static void SetUser(string username, string role)
        {
            CurrentUsername = username;
            CurrentUserRole = role;
        }

        
        public static bool HasPermission(string action)
        {
            if (string.IsNullOrEmpty(CurrentUserRole))
                return false;

            // Admin có tất cả quyền
            if (CurrentUserRole == "Admin")
                return true;

            // Kiểm tra quyền trong RolePermissions
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
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Tự động phân quyền cho control (Button, MenuItem, ToolStripItem,...)
        /// </summary>
        public static void ApplyPermission(Control control, string action)
        {
            if (!HasPermission(action))
            {
                control.Enabled = false;  // hoặc control.Visible = false;
              //  control.Visible = false; // Ẩn control nếu không có quyền
            }
        }

        /// Overload cho ToolStripItem (menu, toolbar...)
        public static void ApplyPermission(ToolStripItem item, string action)
        {
            if (!HasPermission(action))
            {
                item.Enabled = false;  // hoặc item.Visible = false;
            }
        }

    }
}
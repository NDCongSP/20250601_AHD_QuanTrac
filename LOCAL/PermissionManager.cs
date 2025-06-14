using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RegistrationForm1
{
    // Static class để quản lý thông tin người dùng hiện tại và phân quyền
    public static class PermissionManager
    {
        // Thông tin người dùng hiện tại
        public static UserRole CurrentUserRole { get; set; } = UserRole.Employee;
        public static string CurrentUserName { get; set; } = "";
        public static int CurrentUserID { get; set; } = 0;

        // Dictionary định nghĩa quyền cho từng role 
        private static readonly Dictionary<UserRole, List<string>> RolePermissions = new Dictionary<UserRole, List<string>>
        {
            {
                UserRole.Admin,
                new List<string> { "add", "edit", "delete", "view", "manage", "search" }  // Admin có tất cả quyền
            },
            {
                UserRole.Employee,
                new List<string> { "edit", "view", "search" }  // Nhân viên chỉ được sửa, xem, tim kiem
            },
            {
                UserRole.Maintenance,
                new List<string> { "edit", "view" }  // Vận hành bảo trì chỉ được sửa và xem
            }
        };

        // Kiểm tra quyền
        public static bool HasPermission(string permission)
        {
           ;

            if (RolePermissions.ContainsKey(CurrentUserRole))
            {
                bool hasPermission = RolePermissions[CurrentUserRole].Contains(permission.ToLower());
                return hasPermission;
            }

            System.Diagnostics.Debug.WriteLine("HasPermission result: false (role not found)");
            return false;
        }

        // Kiểm tra quyền với thông báo lỗi
        public static bool CheckPermissionWithMessage(string permission, string actionName = "")
        {
            
            if (CurrentUserID <= 0)
            {
                System.Diagnostics.Debug.WriteLine("No user logged in - permission denied");
                MessageBox.Show("Bạn cần đăng nhập để thực hiện chức năng này!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (HasPermission(permission))
            {
               
                return true;
            }

           
            if (string.IsNullOrEmpty(actionName))
            {
                actionName = permission;
            }

            ShowPermissionDeniedMessage(actionName);
            return false;
        }

        // Hiển thị thông báo không có quyền
        private static void ShowPermissionDeniedMessage(string action)
        {
            string actionText = GetActionDisplayName(action);
            string roleText = GetRoleDisplayName(CurrentUserRole);

            System.Windows.Forms.MessageBox.Show(
                $"Bạn không có quyền {actionText}!\n\nQuyền hiện tại: {roleText}\n\nCác quyền được phép: {string.Join(", ", GetCurrentUserPermissions())}",
                "Không có quyền truy cập",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Warning);
        }

        // Chuyển đổi tên action thành tiếng Việt
        private static string GetActionDisplayName(string action)
        {
            switch (action.ToLower())
            {
                case "add":
                    return "thêm người dùng";
                case "edit":
                    return "sửa thông tin người dùng";
                case "delete":
                    return "xóa người dùng";
                case "view":
                    return "xem thông tin";
                case "manage":
                    return "quản lý hệ thống";
                case "search":
                    return "tìm kiếm người dùng";
                default:
                    return action;
            }
        }
        public static bool CanSearch()
        {
            return HasPermission("search") || HasPermission("view"); // Có thể search nếu có quyền search hoặc view
        }
        // Lấy tên hiển thị của role
        public static string GetRoleDisplayName(UserRole role)
        {
            switch (role)
            {
                case UserRole.Employee:
                    return "Nhân viên";
                case UserRole.Maintenance:
                    return "Vận hành bảo trì";
                case UserRole.Admin:
                    return "Quản trị viên";
                default:
                    return "Không xác định";
            }
        }

        // Lấy danh sách quyền của role hiện tại
        public static List<string> GetCurrentUserPermissions()
        {
            if (RolePermissions.ContainsKey(CurrentUserRole))
            {
                return new List<string>(RolePermissions[CurrentUserRole]);
            }
            return new List<string>();
        }

        // Đăng nhập người dùng (set thông tin phân quyền)
        public static void Login(UserRole role, string userName, int userID = 0)
        {
            

            CurrentUserRole = role;
            CurrentUserName = userName;
            CurrentUserID = userID;

           
        }

        // Đăng xuất
        public static void Logout()
        {
            CurrentUserRole = UserRole.Employee;
            CurrentUserName = "";
            CurrentUserID = 0;
        }

        // Kiểm tra xem có phải Admin không
        public static bool IsAdmin()
        {
            return CurrentUserRole == UserRole.Admin;
        }

        // Kiểm tra xem có thể chỉnh sửa thông tin người dùng khác không
        public static bool CanEditOtherUsers()
        {
            return HasPermission("edit");
        }

        // Kiểm tra xem có thể xóa người dùng không
        public static bool CanDeleteUsers()
        {
            return HasPermission("delete");
        }

        // Lấy màu sắc cho button dựa trên quyền
        public static System.Drawing.Color GetButtonColor(string permission)
        {
            if (HasPermission(permission))
            {
                return System.Drawing.SystemColors.Control; // Màu mặc định
            }
            return System.Drawing.Color.LightGray; // Màu xám cho button bị vô hiệu hóa
        }

        // Lấy text cho button dựa trên quyền
        public static string GetButtonText(string permission, string defaultText)
        {
            if (HasPermission(permission))
            {
                return defaultText;
            }
            return $"{defaultText} (Không có quyền)";
        }

        // Method để debug - hiển thị thông tin phân quyền hiện tại
        public static void DebugCurrentPermissions()
        {
           
        }
    }

    // Extension method để dễ sử dụng với controls
    public static class ControlExtensions
    {
        public static void SetPermission(this System.Windows.Forms.Button button, string permission, string defaultText)
        {
            bool hasPermission = PermissionManager.HasPermission(permission);

            System.Diagnostics.Debug.WriteLine($"SetPermission for {defaultText}: {hasPermission}");

            button.Enabled = hasPermission;
            button.BackColor = PermissionManager.GetButtonColor(permission);
            button.Text = PermissionManager.GetButtonText(permission, defaultText);
        }
    }
}
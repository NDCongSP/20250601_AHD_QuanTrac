using Domain.Entities;

namespace Application.Models
{
    public class PermissionsInRoleModel : Permissions
    {
        /// <summary>
        /// dùng để hiển thị ở bước tạo Role, chọn permission.
        /// </summary>
        public bool IsSelected { get; set; } = false;
    }
}

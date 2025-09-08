using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public static class BCryptHelper
    {
        /// Tạo một hash an toàn cho mật khẩu.
        /// </summary>
        /// <param name="password">Mật khẩu gốc (plain text).</param>
        /// <returns>Chuỗi hash đã được salt và băm.</returns>
        public static string HashPassword(string password)
        {
            // BCrypt.HashPassword tự động thêm salt và băm mật khẩu.
            // Tham số thứ hai (12) là work factor, càng cao thì càng an toàn nhưng tốn CPU hơn.
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        /// <summary>
        /// Kiểm tra xem mật khẩu gốc có khớp với hash đã lưu không.
        /// </summary>
        /// <param name="password">Mật khẩu gốc (plain text).</param>
        /// <param name="hashedPassword">Chuỗi hash đã lưu trong database.</param>
        /// <returns>True nếu mật khẩu khớp, ngược lại là False.</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // BCrypt.Verify tự động tách salt từ hash và kiểm tra.
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}

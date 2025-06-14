
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public class DatabaseConnection
    {
        private static string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=RegistrationForm4;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }

    // Lớp User Model
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
    }

    // Lớp UserService để xử lý logic
    public class UserService
    {
        public static bool RegisterUser(User user)
        {
            // Kiểm tra quyền thêm người dùng
            if (!PermissionManager.HasPermission("add"))
            {
                throw new UnauthorizedAccessException("Bạn không có quyền thêm người dùng!");
            }

            // KIỂM TRA BỔ SUNG: CHỈ ADMIN MỚI ĐƯỢC THÊM USER
            if (PermissionManager.CurrentUserRole != UserRole.Admin)
            {
                throw new UnauthorizedAccessException("Chỉ Quản trị viên mới có thể thêm người dùng mới!");
            }

            return RegisterUserInternal(user);
        }

        // Method đăng ký công khai - KHÔNG kiểm tra quyền
        public static bool RegisterUserPublic(User user)
        {
            // Không kiểm tra quyền cho đăng ký công khai
            // Chỉ kiểm tra logic nghiệp vụ cơ bản

            // Kiểm tra admin limit nếu cần
            if (IsAdminPosition(user.Position) && !CanCreateAdmin())
            {
                throw new Exception("Hệ thống đã có quản trị viên! Vui lòng chọn chức vụ khác để đăng ký.");
            }

            return RegisterUserInternal(user);
        }

        // Method nội bộ thực hiện đăng ký (không kiểm tra quyền)
        private static bool RegisterUserInternal(User user)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_RegisterUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FullName", user.FullName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Position", user.Position);
                        cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber ?? "");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string result = reader["Result"].ToString();
                                if (result == "SUCCESS")
                                {
                                    return true;
                                }
                                else
                                {
                                    throw new Exception(result);
                                }
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đăng ký user: " + ex.Message);
            }
        }

        // Overload method với tham số isPublicRegistration
        public static bool RegisterUser(User user, bool isPublicRegistration)
        {
            if (isPublicRegistration)
            {
                return RegisterUserPublic(user);
            }
            else
            {
                return RegisterUser(user); // Call method có kiểm tra quyền
            }
        }
        public static bool IsAdminPosition(string position)
        {
            if (string.IsNullOrEmpty(position))
                return false;

            return position.Equals("Quản trị viên", StringComparison.OrdinalIgnoreCase);
        }

        public static int GetAdminCount()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Sử dụng stored procedure để đảm bảo consistency
                    using (SqlCommand cmd = new SqlCommand("sp_GetAdminCount", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi kiểm tra số lượng quản trị viên: " + ex.Message);
            }
        }

        // Method kiểm tra có thể tạo admin không
        public static bool CanCreateAdmin()
        {
            return GetAdminCount() == 0;
        }

        // Method đăng ký user với kiểm tra admin nghiêm ngặt


        public static User LoginUser(string FullName, string password)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_LoginUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FullName", FullName);
                        cmd.Parameters.AddWithValue("@Password", password);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    UserID = (int)reader["UserID"],
                                    FullName = reader["FullName"].ToString(),
                                    Position = reader["Position"].ToString(),
                                    IsActive = (bool)reader["IsActive"]
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đăng nhập: " + ex.Message);
            }
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            try
            {
                // Test kết nối trước
                if (!DatabaseConnection.TestConnection())
                {
                    throw new Exception("Không thể kết nối đến database. Kiểm tra connection string!");
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_GetAllUsers", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 30;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                string errorMsg = $"SQL Error: {sqlEx.Message}\nProcedure: sp_GetAllUsers\nNumber: {sqlEx.Number}";
                System.Diagnostics.Debug.WriteLine(errorMsg);
                System.Windows.Forms.MessageBox.Show(errorMsg, "Database Error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return dt;
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error loading users: {ex.Message}";
                System.Diagnostics.Debug.WriteLine(errorMsg);
                System.Windows.Forms.MessageBox.Show(errorMsg, "Error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return dt;
            }
        }

        public static User GetUserById(int userID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetUserByID", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    UserID = (int)reader["UserID"],
                                    FullName = reader["FullName"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    Position = reader["Position"].ToString(),
                                    IsActive = (bool)reader["IsActive"],
                                    PhoneNumber = reader["PhoneNumber"].ToString()
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin người dùng: " + ex.Message);
            }
        }

        public static bool UpdateUser(User user)
        {
            // KIỂM TRA QUYỀN CƠ BẢN
            if (!PermissionManager.HasPermission("edit"))
            {
                throw new UnauthorizedAccessException("Bạn không có quyền chỉnh sửa người dùng!");
            }

            // KIỂM TRA LOGIC BỔ SUNG
            // Nếu không phải Admin, chỉ được sửa thông tin của chính mình
            if (PermissionManager.CurrentUserRole != UserRole.Admin)
            {
                if (user.UserID != PermissionManager.CurrentUserID)
                {
                    throw new UnauthorizedAccessException("Bạn chỉ có thể sửa thông tin của chính mình!");
                }
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", user.UserID);
                        cmd.Parameters.AddWithValue("@FullName", user.FullName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Position", user.Position);
                        cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                        cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber ?? "");

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string result = reader["Result"].ToString();
                                if (result == "SUCCESS")
                                {
                                    return true;
                                }
                                else
                                {
                                    throw new Exception(result);
                                }
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật: " + ex.Message);
            }
        }
        public static bool CheckPhoneExists(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CheckPhoneExists", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // THÊM method kiểm tra số điện thoại cho update
        public static bool CheckPhoneExistsForUpdate(string phoneNumber, int currentUserID)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CheckPhoneExists", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.Parameters.AddWithValue("@ExcludeUserID", currentUserID);

                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteUser(int userID)
        {
            // KIỂM TRA QUYỀN NGHIÊM NGẶT
            if (!PermissionManager.HasPermission("delete"))
            {
                throw new UnauthorizedAccessException("Bạn không có quyền xóa người dùng!");
            }

            // KIỂM TRA BỔ SUNG: CHỈ ADMIN MỚI ĐƯỢC XÓA
            if (PermissionManager.CurrentUserRole != UserRole.Admin)
            {
                throw new UnauthorizedAccessException("Chỉ Quản trị viên mới có thể xóa người dùng!");
            }

            // KHÔNG CHO PHÉP XÓA CHÍNH MÌNH
            if (userID == PermissionManager.CurrentUserID)
            {
                throw new InvalidOperationException("Bạn không thể xóa tài khoản của chính mình!");
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string result = reader["Result"].ToString();
                                if (result == "SUCCESS")
                                {
                                    return true;
                                }
                                else if (result.Contains("admin"))
                                {
                                    throw new Exception(result);
                                }
                                return false;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa: " + ex.Message);
            }
        }

        public static bool CheckUsernameExists(string FullName)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CheckFullnameExists", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FullName", FullName);

                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckUsernameExistsForUpdate(string fullName, int currentUserID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CheckFullnameExists", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FullName", fullName);
                        cmd.Parameters.AddWithValue("@ExcludeUserID", currentUserID);

                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static Dictionary<string, int> GetUserStatistics()
        {
            try
            {
                var stats = new Dictionary<string, int>();

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users", conn))
                    {
                        stats["TotalUsers"] = (int)cmd.ExecuteScalar();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE IsActive = 1", conn))
                    {
                        stats["ActiveUsers"] = (int)cmd.ExecuteScalar();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE IsActive = 0", conn))
                    {
                        stats["InactiveUsers"] = (int)cmd.ExecuteScalar();
                    }

                    // Thêm thống kê admin
                    stats["AdminCount"] = GetAdminCount();
                }

                return stats;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thống kê: " + ex.Message);
            }
        }
        public static DataTable SearchUsersByDateRange(DateTime fromDate, DateTime toDate)
        {
            // Kiểm tra quyền tìm kiếm
            if (!PermissionManager.HasPermission("view") && !PermissionManager.HasPermission("search"))
            {
                throw new UnauthorizedAccessException("Bạn không có quyền tìm kiếm người dùng!");
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Sử dụng stored procedure hoặc query trực tiếp
                    string query = @"
                SELECT UserID, FullName, DateOfBirth, Position, CreatedDate, IsActive
                FROM Users 
                WHERE CreatedDate >= @FromDate AND CreatedDate <= @ToDate
                ORDER BY CreatedDate DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FromDate", fromDate);
                        cmd.Parameters.AddWithValue("@ToDate", toDate);
                        cmd.CommandTimeout = 30;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            System.Diagnostics.Debug.WriteLine($"SearchUsersByDateRange: Found {dt.Rows.Count} users between {fromDate:yyyy-MM-dd} and {toDate:yyyy-MM-dd}");

                            return dt;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                string errorMsg = $"SQL Error in SearchUsersByDateRange: {sqlEx.Message}\nNumber: {sqlEx.Number}";
                System.Diagnostics.Debug.WriteLine(errorMsg);
                throw new Exception("Lỗi truy vấn database khi tìm kiếm: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in SearchUsersByDateRange: {ex.Message}");
                throw new Exception("Lỗi tìm kiếm theo ngày: " + ex.Message);
            }
        }

        // THÊM: Method tìm kiếm nâng cao (kết hợp nhiều điều kiện)
        public static DataTable SearchUsersAdvanced(DateTime? fromDate = null, DateTime? toDate = null,
            string nameFilter = null, string positionFilter = null, bool? isActiveFilter = null, string PhoneNumberFilter = null)
        {
            // Kiểm tra quyền
            if (!PermissionManager.HasPermission("view") && !PermissionManager.HasPermission("search"))
            {
                throw new UnauthorizedAccessException("Bạn không có quyền tìm kiếm người dùng!");
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    StringBuilder queryBuilder = new StringBuilder();
                    queryBuilder.Append(@"
                SELECT UserID, FullName, DateOfBirth, Position, CreatedDate, IsActive, PhoneNumber
                FROM Users 
                WHERE 1=1");

                    List<SqlParameter> parameters = new List<SqlParameter>();

                    // Thêm điều kiện ngày
                    if (fromDate.HasValue)
                    {
                        queryBuilder.Append(" AND CreatedDate >= @FromDate");
                        parameters.Add(new SqlParameter("@FromDate", fromDate.Value));
                    }

                    if (toDate.HasValue)
                    {
                        queryBuilder.Append(" AND CreatedDate <= @ToDate");
                        parameters.Add(new SqlParameter("@ToDate", toDate.Value));
                    }

                    // Thêm điều kiện tên
                    if (!string.IsNullOrEmpty(nameFilter))
                    {
                        queryBuilder.Append(" AND FullName LIKE @NameFilter");
                        parameters.Add(new SqlParameter("@NameFilter", $"%{nameFilter}%"));
                    }

                    // Thêm điều kiện chức vụ
                    if (!string.IsNullOrEmpty(positionFilter))
                    {
                        queryBuilder.Append(" AND Position = @PositionFilter");
                        parameters.Add(new SqlParameter("@PositionFilter", positionFilter));
                    }

                    if (isActiveFilter.HasValue)
                    {
                        queryBuilder.Append(" AND IsActive = @IsActiveFilter");
                        parameters.Add(new SqlParameter("@IsActiveFilter", isActiveFilter.Value));
                    }

                    // Thêm điều kiện trạng thái
                    if (isActiveFilter.HasValue)
                    {
                        queryBuilder.Append(" AND PhoneNumber = @PhoneNumberFilter");
                        parameters.Add(new SqlParameter("@PhoneNumberFilter", PhoneNumberFilter));
                    }
                
                    queryBuilder.Append(" ORDER BY CreatedDate DESC");

                    using (SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn))
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                        cmd.CommandTimeout = 30;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            System.Diagnostics.Debug.WriteLine($"SearchUsersAdvanced: Found {dt.Rows.Count} users with applied filters");

                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in SearchUsersAdvanced: {ex.Message}");
                throw new Exception("Lỗi tìm kiếm nâng cao: " + ex.Message);
            }
        }
        public static bool UpdateLoginTime(int userID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Users SET LastLoginTime = GETDATE() WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateLoginTime Error: {ex.Message}");
                return false;
            }
        }

        // Method cập nhật thời gian đăng xuất
        public static bool UpdateLogoutTime(int userID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Users SET LastLogoutTime = GETDATE() WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateLogoutTime Error: {ex.Message}");
                return false;
            }
        }
        // CẬP NHẬT method SearchUsers hiện có để thêm kiểm tra quyền
        public static DataTable SearchUsers(string searchTerm)
        {
            // THÊM kiểm tra quyền
            if (!PermissionManager.HasPermission("view") && !PermissionManager.HasPermission("search"))
            {
                throw new UnauthorizedAccessException("Bạn không có quyền tìm kiếm người dùng!");
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT UserID, FullName, DateOfBirth, Position, CreatedDate, IsActive FROM Users WHERE (FullName LIKE @SearchTerm OR Position LIKE @SearchTerm) AND IsActive = 1", conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm: " + ex.Message);
            }
        }
    }
}
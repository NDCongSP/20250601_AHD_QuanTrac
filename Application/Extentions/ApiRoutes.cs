namespace Application.Extentions
{
    public static class ApiRoutes
    {
        /// <summary>
        /// httpGet.
        /// </summary>
        public const string GetAll = "";
        /// <summary>
        /// httpget.
        /// </summary>
        public const string GetById = "{id}";
        /// <summary>
        /// httppost.
        /// </summary>
        public const string Update = "update";
        /// <summary>
        /// httppost.
        /// </summary>
        public const string Insert = "insert";
        public const string Delete = "delete";
        public const string AddRange = "AddRange";
        public const string DeleteRange = "DeleteRange";
        public const string DeleteById = "DeleteById/{id}";

        public static class Identity
        {
            public const string BasePath = "api/account";
            public const string Login = "identity/loginasync";
            public const string Logout = "identity/logout";
            public const string LoginHt = "identity/loginht";
            public const string CreateAccount = "identity/create";
            public const string RefreshToken = "identity/refresh-token";
            public const string CreateRole = "identity/role/create";
            public const string RoleList = "identity/role/list";
            public const string CreateSuperAdminAccount = "identity/setting";
            public const string UserWithRole = "identity/user-with-role";
            public const string ChangePassword = "identity/change-pass";
            public const string ChangeUserRole = "identity/change-role";
            public const string AssignUserRole = "identity/assign_user_role";
            public const string DeleteUser = "identity/delete-user";
            public const string DeleteUserRole = "identity/delete-user-role";
            public const string UpdateRole = "identity/update-role-name";
            public const string UpdateRoleDTO = "identity/update-role";
            public const string UpdateUserInfo = "identity/update-user-info";
            public const string UserGetById = "identity/{id}";
            public const string DeleteRole = "identity/role/delete";
            public const string UserGetByEmail = "identity/UserGetByEmail/{email}";
            public const string RoleGetById = "identity/Role/{id}";
            public const string CheckPasswordAsync = "identity/CheckPasswordAsync/{email}/{password}";
        }
        public static class Permissions
        {
            public const string BasePath = "api/Permissions";
            public const string GetAllPermissionWithAssignedRole = "Get-All-Permission-With-Assigned-To-Role";
            public const string AddOrEdit = "add-or-edit";
        }
        public static class PermissionTenant
        {
            public const string BasePath = "api/PermissionTenant";
        }

        public static class RoleToPermissions
        {
            public const string BasePath = "api/RoleToPermissions";
            public const string GetByPermissionId = "GetByPermissionId/{id}";
        }
        public static class RoleToPermissionTenant
        {
            public const string BasePath = "api/RoleToPermissionsTenant";
        }

        public static class FT01
        {
            public const string BasePath = "api/FT01";
            public const string DeleteLocation = "DeleteLocation/{locationId}";
            public const string AddOrUpdateLocation = "AddOrUpdateLocation";
            public const string GetConfig = "get-config";
            public const string UpdateConfig = "update-config";
        }
        public static class FT02
        {
            public const string BasePath = "api/FT02";
            public const string GetRealtimeDisplay = "GetRealtimeDisplay";
        }

        public static class FT03
        {
            public const string BasePath = "api/FT03";
            public const string GetByFromDateToDateAsync = "GetByFromDateToDateAsync";
            public const string GetSampled = "GetSampled";

        }

        public static class FT04
        {
            public const string BasePath = "api/FT04";
        }

        public static class FT05
        {
            public const string BasePath = "api/FT05";
        }

        public static class FT06
        {
            public const string BasePath = "api/FT06";
        }

        public static class FT07
        {
            public const string BasePath = "api/FT07";
        }

        public static class ScadaUser
        {
            public const string BasePath = "api/ScadaUser";
            public const string ChangePassword = "change-password";
            public const string ResetPassword = "reset-password/{id}";
        }

        public static class FT08
        {
            public const string BasePath = "api/FT08";
            public const string GetPdfAsBase64Async = "GetPdfAsBase64Async/{pathFile}";
            public const string UploadPdfFileAsync = "UploadPdfFileAsync";
        }
    }
}

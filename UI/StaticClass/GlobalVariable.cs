using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Domain;

namespace UI
{
    public static class GlobalVariable
    {
        public static BreadCumb BreadCrumbData { get; set; } = new BreadCumb();
        public static BreadCumb BreadCrumbDataMaster { get; set; } = new BreadCumb();
        public static UserAuthorizationInfo UserAuthorizationInfo { get; set; } = new UserAuthorizationInfo();
        [CascadingParameter] public static AuthenticationState AuthenticationStateTask { get; set; }
        public static string FilePathTemporary { get; set; }
        public static string ApiURL { get; set; }
        public static bool AllowUpdateImageProduct { get; set; } = true;
        public static ConfigModel ConfigSystem { get; set; } = new();
    }
}

using System;

namespace Evoflare.API.Core.Permissions
{

    public static class CommonPermission
    {
        public const string Add = "add";
        public const string Edit = "edit";
        public const string Delete = "delete";
        public const string View = "view";
    }


    [Flags]
    public enum AccessFlag
    {
        Read = 1,
        Create = 2,
        Edit = 4,
        Delete = 8,
        Details = 16,
        Manage = 32,
        GodMode = 64
    }

    public static class AppPermissions
    {
        public static class AdminPermission
        {
            public const string Add = "admin.add";
            public const string Edit = "admin.edit";
            public const string Delete = "admin.delete";
            public const string View = "admin.view";
        }

        public class SalaryPermission
        {
            private const string moduleId = "salary";

            public static string Add  = $"{moduleId}.{CommonPermission.Add}"; 
            public static string Edit = $"{moduleId}.{CommonPermission.Edit}";
            public static string Delete = $"{moduleId}.{CommonPermission.Delete}";
            public static string View = $"{moduleId}.{CommonPermission.View}";
        }

        public static class EmployeePermission
        {
            public const string Add = "employee.add";
            public const string Edit = "employee.edit";
            public const string Delete = "employee.delete";
            public const string View = "employee.view";
        }

        public static class SystemPermission
        {
            public const string Manage = "system.manage";
        }

        public static class OrganizationsPermission
        {
            public const string Manage = "organization.manage";
            public const string Details = "organization.details";
        }

        public static class UsersPermission
        {
            public const string Add = "users.add";
            public const string Edit = "users.edit";
            public const string Delete = "users.delete";
            public const string View = "users.view";
            public const string Disable = "users.disable";
        }
    }
}

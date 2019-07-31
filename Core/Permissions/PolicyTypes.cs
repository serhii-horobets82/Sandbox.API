namespace Evoflare.API.Core.Permissions
{
    public static class PolicyTypes
    {
            
        public static class SysAdminPolicy
        {
            public const string Crud = "admin.crud";
            public const string Cru = "admin.cru";
            public const string View = "admin.view";
        }

        public static class AdminPolicy
        {
            public const string Crud = "admin.crud";
            public const string Cru = "admin.cru";
            public const string View = "admin.view";
        }

        public static class SalaryPolicy
        {
            public const string Cru = "salary.cru";
            public const string View = "salary.view";
            public const string Delete = "salary.delete";
        }

        public static class UserPolicy
        {
            public const string Manage = "user.manage";
        }

    }
}

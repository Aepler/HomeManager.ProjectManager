using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeManager.WebApplication.ViewModels.Admin
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string Roles => "Roles";

        public static string Users => "Users";

        public static string UserRoles => "User Roles";
        public static string Types => "Types";
        public static string Status => "Status";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string RolesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Roles);

        public static string UsersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Users);

        public static string UserRolesNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserRoles);
        public static string TypesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Types);
        public static string StatusNavClass(ViewContext viewContext) => PageNavClass(viewContext, Status);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeManager.WebApplication.Areas.Customize.ViewModels
{
    public static class CustomizeNavViews
    {
        public static string IndexFinance => "IndexFinance";

        public static string Categories => "Categories";

        public static string PaymentTemplates => "PaymentTemplates";

        public static string Types => "Types";

        public static string Status => "Status";

        public static string IndexFinanceNavClass(ViewContext viewContext) => PageNavClass(viewContext, IndexFinance);

        public static string CategoriesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Categories);

        public static string PaymentTemplatesNavClass(ViewContext viewContext) => PageNavClass(viewContext, PaymentTemplates);

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
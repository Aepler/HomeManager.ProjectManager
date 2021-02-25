using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeManager.WebApplication.Areas.Finance.ViewModels
{
    public static class FinanceNavViews
    {
        public static string Index => "Index";

        public static string Payments => "Payments";

        public static string RepeatingPayments => "RepeatingPayments";


        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string PaymentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Payments);

        public static string RepeatingPaymentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, RepeatingPayments);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}

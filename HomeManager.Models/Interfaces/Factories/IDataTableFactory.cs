using HomeManager.Models.Core;
using HomeManager.Models.DataTables.Finance;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.Models.Interfaces.Factories
{
    public interface IDataTableFactory
    {
        DataTableResponse<WalletDataTable> GetTableData(DataTableInput model, IEnumerable<Wallet> query);
        DataTableResponse<PaymentDataTable> GetTableData(DataTableInput model, IEnumerable<Payment> query);
        DataTableResponse<CategoryDataTable> GetTableData(DataTableInput model, IEnumerable<Category> query);
        DataTableResponse<TemplateDataTable> GetTableData(DataTableInput model, IEnumerable<Template> query);
        DataTableResponse<TypeDataTable> GetTableData(DataTableInput model, IEnumerable<Type> query);
        DataTableResponse<StatusDataTable> GetTableData(DataTableInput model, IEnumerable<Status> query);
        DataTableResponse<UserDataTable> GetTableData(DataTableInput model, IEnumerable<User> query);
        DataTableResponse<RoleDataTable> GetTableData(DataTableInput model, IEnumerable<Role> query);
        DataTableResponse<UserRoleDataTable> GetTableData(DataTableInput model, IEnumerable<UserRoleDataTable> query);
    }
}

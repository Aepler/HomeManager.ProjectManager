using HomeManager.Models.DataTable;
using HomeManager.Models.DataTable.Finance;
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
        DataTableResponse<WalletDataTable> GetTableData(DataTableInput model, ICollection<Wallet> list);
        DataTableResponse<PaymentDataTable> GetTableData(DataTableInput model, ICollection<Payment> list);
        DataTableResponse<CategoryDataTable> GetTableData(DataTableInput model, ICollection<Category> list);
        DataTableResponse<TemplateDataTable> GetTableData(DataTableInput model, ICollection<Template> list);
        DataTableResponse<TypeDataTable> GetTableData(DataTableInput model, ICollection<Type> list);
        DataTableResponse<StatusDataTable> GetTableData(DataTableInput model, ICollection<Status> list);
        DataTableResponse<UserDataTable> GetTableData(DataTableInput model, ICollection<User> list);
        DataTableResponse<RoleDataTable> GetTableData(DataTableInput model, ICollection<Role> list);
        DataTableResponse<UserRoleDataTable> GetTableData(DataTableInput model, ICollection<UserRoleDataTable> list);
    }
}

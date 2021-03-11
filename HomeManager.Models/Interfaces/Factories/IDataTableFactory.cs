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
        Task<DataTableResponse<WalletDataTable>> GetTableData(DataTableInput model, ICollection<Wallet> list);
        Task<DataTableResponse<PaymentDataTable>> GetTableData(DataTableInput model, ICollection<Payment> list);
        Task<DataTableResponse<CategoryDataTable>> GetTableData(DataTableInput model, ICollection<Category> list);
        Task<DataTableResponse<TemplateDataTable>> GetTableData(DataTableInput model, ICollection<Template> list);
        Task<DataTableResponse<TypeDataTable>> GetTableData(DataTableInput model, ICollection<Type> list);
        Task<DataTableResponse<StatusDataTable>> GetTableData(DataTableInput model, ICollection<Status> list);
        Task<DataTableResponse<UserDataTable>> GetTableData(DataTableInput model, ICollection<User> list);
        Task<DataTableResponse<RoleDataTable>> GetTableData(DataTableInput model, ICollection<Role> list);
        Task<DataTableResponse<UserRoleDataTable>> GetTableData(DataTableInput model, ICollection<UserRoleDataTable> list);
    }
}

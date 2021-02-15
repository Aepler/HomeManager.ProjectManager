﻿using HomeManager.Models.DataTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Interfaces.Factories
{
    public interface IDataTableFactory
    {
        Task<DataTableResultModel<PaymentDataTableModel>> GetTableData(DataTableModel model, ICollection<Payment> list);
        Task<DataTableResultModel<CategoryDataTableModel>> GetTableData(DataTableModel model, ICollection<Category> list);
        Task<DataTableResultModel<PaymentTemplateDataTableModel>> GetTableData(DataTableModel model, ICollection<Payment_Template> list);
        Task<DataTableResultModel<TypeDataTableModel>> GetTableData(DataTableModel model, ICollection<Type> list);
        Task<DataTableResultModel<StatusDataTableModel>> GetTableData(DataTableModel model, ICollection<Status> list);
        Task<DataTableResultModel<UserDataTableModel>> GetTableData(DataTableModel model, ICollection<User> list);
        Task<DataTableResultModel<RoleDataTableModel>> GetTableData(DataTableModel model, ICollection<Role> list);
        Task<DataTableResultModel<UserRoleDataTableModel>> GetTableData(DataTableModel model, ICollection<UserRoleDataTableModel> list);
    }
}

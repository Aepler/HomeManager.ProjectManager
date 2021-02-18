using HomeManager.Models.DataTableModels;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Factories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.WebApplication.Factories
{
    public class DataTableFactory : IDataTableFactory
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DataTableFactory(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<DataTableResultModel<RoleDataTableModel>> GetTableData(DataTableModel model, ICollection<Role> list)
        {
            try
            {
                var result = new DataTableResultModel<RoleDataTableModel>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new RoleDataTableModel
                {
                    Id = d.Id.ToString(),
                    Name = d.Name,
                    NormalizedName = d.NormalizedName,
                    ConcurrencyStamp = d.ConcurrencyStamp.ToString()
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.Name.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                result.draw = model.draw;
                result.recordsTotal = totalRecords;
                result.recordsFiltered = recFilter;
                result.data = modifiedData;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DataTableResultModel<PaymentDataTableModel>> GetTableData(DataTableModel model, ICollection<Payment> list)
        {
            try
            {
                var result = new DataTableResultModel<PaymentDataTableModel>();
                int totalRecords = list.Count;


                var modifiedData = list.Select(d => new PaymentDataTableModel
                {
                    Id = d.Id.ToString(),
                    Date = d.Date.ToString("dd.MM.yyyy"),
                    Description = d.Description,
                    Description_ExtraCosts = d.Description_ExtraCosts,
                    Description_TaxList = d.Description_TaxList,
                    Tax = d.Tax.ToString(),
                    Amount = d.Amount.ToString(),
                    Amount_Tax = d.Amount_Tax.ToString(),
                    Amount_Gross = d.Amount_Gross.ToString(),
                    Amount_Net = d.Amount_Net.ToString(),
                    Amount_ExtraCosts = d.Amount_ExtraCosts,
                    Amount_TaxList = d.Amount_TaxList,
                    fk_TypeId = d.fk_TypeId.ToString(),
                    Type = d.Type.Name,
                    fk_CategoryId = d.fk_CategoryId.ToString(),
                    Category = d.fk_CategoryId != null ? d.Category.Name : d.Type.Name,
                    fk_StatusId = d.fk_StatusId.ToString(),
                    Status = d.Status.Name
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.Date.Contains(model.search.value) ||
                        p.Description.ToLower().Contains(model.search.value) ||
                        p.Amount.Contains(model.search.value) ||
                        p.Type.ToLower().Contains(model.search.value) ||
                        p.Category.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                result.draw = model.draw;
                result.recordsTotal = totalRecords;
                result.recordsFiltered = recFilter;
                result.data = modifiedData;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DataTableResultModel<CategoryDataTableModel>> GetTableData(DataTableModel model, ICollection<Category> list)
        {
            try
            {
                var result = new DataTableResultModel<CategoryDataTableModel>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new CategoryDataTableModel
                {
                    Id = d.Id.ToString(),
                    Name = d.Name
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.Name.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                result.draw = model.draw;
                result.recordsTotal = totalRecords;
                result.recordsFiltered = recFilter;
                result.data = modifiedData;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DataTableResultModel<TemplateDataTableModel>> GetTableData(DataTableModel model, ICollection<Template> list)
        {
            try
            {
                var result = new DataTableResultModel<TemplateDataTableModel>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new TemplateDataTableModel
                {
                    Id = d.Id.ToString(),
                    Date = d.Date.ToString(),
                    Description = d.Description,
                    Amount = d.Amount.ToString(),
                    fk_TypeId = d.fk_TypeId.ToString(),
                    Type = d.Type.Name,
                    fk_CategoryId = d.fk_CategoryId.ToString(),
                    Category = d.fk_CategoryId != null ? d.Category.Name : null
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.Date.ToLower().Contains(model.search.value) ||
                        p.Description.ToLower().Contains(model.search.value) ||
                        p.Amount.ToLower().Contains(model.search.value) ||
                        p.Type.Contains(model.search.value) ||
                        p.Category.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                result.draw = model.draw;
                result.recordsTotal = totalRecords;
                result.recordsFiltered = recFilter;
                result.data = modifiedData;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DataTableResultModel<TypeDataTableModel>> GetTableData(DataTableModel model, ICollection<Type> list)
        {
            try
            {
                var result = new DataTableResultModel<TypeDataTableModel>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new TypeDataTableModel
                {
                    Id = d.Id.ToString(),
                    Name = d.Name,
                    EndTaxType = d.EndTaxType.ToString(),
                    Debit = d.TransactionType.ToString(),
                    ExtraInput = d.ExtraInput,
                    fk_StatusId = d.fk_StatusId.ToString(),
                    Status = d.Status.Name
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.Name.ToLower().Contains(model.search.value) ||
                        p.EndTaxType.ToLower().Contains(model.search.value) ||
                        p.Debit.ToLower().Contains(model.search.value) ||
                        p.ExtraInput.Contains(model.search.value) ||
                        p.Status.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                result.draw = model.draw;
                result.recordsTotal = totalRecords;
                result.recordsFiltered = recFilter;
                result.data = modifiedData;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DataTableResultModel<StatusDataTableModel>> GetTableData(DataTableModel model, ICollection<Status> list)
        {
            try
            {
                var result = new DataTableResultModel<StatusDataTableModel>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new StatusDataTableModel
                {
                    Id = d.Id.ToString(),
                    Name = d.Name,
                    EndPoint = d.EndPoint.ToString()
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.Name.ToLower().Contains(model.search.value) ||
                        p.EndPoint.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                result.draw = model.draw;
                result.recordsTotal = totalRecords;
                result.recordsFiltered = recFilter;
                result.data = modifiedData;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DataTableResultModel<UserRoleDataTableModel>> GetTableData(DataTableModel model, ICollection<UserRoleDataTableModel> list)
        {
            try
            {
                var result = new DataTableResultModel<UserRoleDataTableModel>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new UserRoleDataTableModel
                {
                    User = d.User,
                    UserId = d.UserId,
                    Role = d.Role,
                    RoleId = d.RoleId
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.User.ToLower().Contains(model.search.value) ||
                        p.Role.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                result.draw = model.draw;
                result.recordsTotal = totalRecords;
                result.recordsFiltered = recFilter;
                result.data = modifiedData;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DataTableResultModel<UserDataTableModel>> GetTableData(DataTableModel model, ICollection<User> list)
        {
            try
            {
                var result = new DataTableResultModel<UserDataTableModel>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new UserDataTableModel
                {
                    Id = d.Id.ToString(),
                    UserName = d.UserName,
                    Email = d.Email,
                    Name = d.Name,
                    Lastname = d.Lastname,
                    PhoneNumber = d.PhoneNumber != null ? d.PhoneNumber.ToString() : null,
                    TwoFactorEnabled = d.TwoFactorEnabled.ToString()
                }
                    );

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    modifiedData = modifiedData.Where(p => p.UserName.ToLower().Contains(model.search.value) ||
                        p.Email.ToLower().Contains(model.search.value) ||
                        p.Name.ToLower().Contains(model.search.value) ||
                        p.Lastname.ToLower().Contains(model.search.value) ||
                        p.PhoneNumber.ToLower().Contains(model.search.value) ||
                        p.TwoFactorEnabled.ToLower().Contains(model.search.value)
                     ).ToList();
                }

                string sortBy = "";
                string sortDir = "";

                if (model.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = model.columns[model.order[0].column].data;
                    sortDir = model.order[0].dir.ToLower();
                }

                int recFilter = modifiedData.Count();
                modifiedData = modifiedData.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length).ToList();

                result.draw = model.draw;
                result.recordsTotal = totalRecords;
                result.recordsFiltered = recFilter;
                result.data = modifiedData;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

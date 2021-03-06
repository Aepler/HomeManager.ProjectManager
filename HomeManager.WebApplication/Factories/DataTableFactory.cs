using HomeManager.Models.DataTable;
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
        public async Task<DataTableResponse<RoleDataTable>> GetTableData(DataTableInput model, ICollection<Role> list)
        {
            try
            {
                var result = new DataTableResponse<RoleDataTable>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new RoleDataTable
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

        public async Task<DataTableResponse<PaymentDataTable>> GetTableData(DataTableInput model, ICollection<Payment> list)
        {
            try
            {
                var result = new DataTableResponse<PaymentDataTable>();
                int totalRecords = list.Count;


                var modifiedData = list.Select(d => new PaymentDataTable
                {
                    Id = d.Id.ToString(),
                    Date = d.Date.ToString("dd.MM.yyyy"),
                    Description = d.Description,
                    Description_ExtraCosts = d.ExtraCostDescription,
                    Description_TaxList = d.DetailedTaxDescription,
                    Tax = d.TaxRate.ToString(),
                    Amount = d.Amount.ToString(),
                    Amount_Tax = d.TaxAmount.ToString(),
                    Amount_Gross = d.GrossAmount.ToString(),
                    Amount_Net = d.NetAmount.ToString(),
                    Amount_ExtraCosts = d.ExtraCostAmount,
                    Amount_TaxList = d.DetailedTaxAmount,
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

        public async Task<DataTableResponse<CategoryDataTable>> GetTableData(DataTableInput model, ICollection<Category> list)
        {
            try
            {
                var result = new DataTableResponse<CategoryDataTable>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new CategoryDataTable
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

        public async Task<DataTableResponse<TemplateDataTable>> GetTableData(DataTableInput model, ICollection<Template> list)
        {
            try
            {
                var result = new DataTableResponse<TemplateDataTable>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new TemplateDataTable
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

        public async Task<DataTableResponse<TypeDataTable>> GetTableData(DataTableInput model, ICollection<Type> list)
        {
            try
            {
                var result = new DataTableResponse<TypeDataTable>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new TypeDataTable
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

        public async Task<DataTableResponse<StatusDataTable>> GetTableData(DataTableInput model, ICollection<Status> list)
        {
            try
            {
                var result = new DataTableResponse<StatusDataTable>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new StatusDataTable
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

        public async Task<DataTableResponse<UserRoleDataTable>> GetTableData(DataTableInput model, ICollection<UserRoleDataTable> list)
        {
            try
            {
                var result = new DataTableResponse<UserRoleDataTable>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new UserRoleDataTable
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

        public async Task<DataTableResponse<UserDataTable>> GetTableData(DataTableInput model, ICollection<User> list)
        {
            try
            {
                var result = new DataTableResponse<UserDataTable>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new UserDataTable
                {
                    Id = d.Id.ToString(),
                    UserName = d.UserName,
                    Email = d.Email,
                    Name = d.FirstName,
                    Lastname = d.LastName,
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

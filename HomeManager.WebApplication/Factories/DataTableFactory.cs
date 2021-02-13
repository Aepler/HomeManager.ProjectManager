using HomeManager.Models;
using HomeManager.Models.DataTableModels;
using HomeManager.Models.Interfaces;
using HomeManager.Models.Interfaces.Factories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace HomeManager.WebApplication.Factories
{
    public class DataTableFactory : IDataTableFactory
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPaymentService _paymentService;
        private readonly ICategoryService _categoryService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;
        private readonly IPayment_TemplateService _payment_templateService;

        public DataTableFactory(UserManager<User> userManager, RoleManager<Role> roleManager, IPaymentService paymentService, ICategoryService categoryService, ITypeService typeService, IStatusService statusService, IPayment_TemplateService payment_templateService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _paymentService = paymentService;
            _categoryService = categoryService;
            _typeService = typeService;
            _statusService = statusService;
            _payment_templateService = payment_templateService;
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
                    ConcurrencyStamp = d.ConcurrencyStamp.ToString(),
                    Buttons = "<button class='buttonEditRoleAdmin btn btn-outline-secondary' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalEditRoleAdmin'>Edit</button>" +
                    " | " +
                              "<button class='buttonDeleteRoleAdmin btn btn-outline-danger' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalDeleteRoleAdmin'>Delete</button>"
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
                    Description_Extra = d.Description_Extra,
                    Description_Tax = d.Description_Tax,
                    Tax = d.Tax.ToString(),
                    Amount = d.Amount.ToString(),
                    Amount_Tax = d.Amount_Tax.ToString(),
                    Amount_Gross = d.Amount_Gross.ToString(),
                    Amount_Net = d.Amount_Net.ToString(),
                    Amount_Extra = d.Amount_Extra.ToString(),
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
                    Name = d.Name,
                    Buttons = "<button class='buttonEditTypeAdmin btn btn-outline-secondary' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalEditTypeAdmin'>Edit</button>" +
                    " | " +
                              "<button class='buttonDeleteTypeAdmin btn btn-outline-danger' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalLabelDeleteTypeAdmin'>Delete</button>"
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

        public async Task<DataTableResultModel<PaymentTemplateDataTableModel>> GetTableData(DataTableModel model, ICollection<Payment_Template> list)
        {
            try
            {
                var result = new DataTableResultModel<PaymentTemplateDataTableModel>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new PaymentTemplateDataTableModel
                {
                    Id = d.Id.ToString(),
                    Date = d.Date.ToString(),
                    Description = d.Description,
                    Amount = d.Amount.ToString(),
                    fk_TypeId = d.fk_TypeId.ToString(),
                    Type = d.Type.Name,
                    fk_CategoryId = d.fk_CategoryId.ToString(),
                    Category = d.fk_CategoryId != null ? d.Category.Name : null,
                    Buttons = "<button class='buttonEditTypeAdmin btn btn-outline-secondary' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalEditTypeAdmin'>Edit</button>" +
                    " | " +
                              "<button class='buttonDeleteTypeAdmin btn btn-outline-danger' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalLabelDeleteTypeAdmin'>Delete</button>"
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

        public async Task<DataTableResultModel<TypeDataTableModel>> GetTableData(DataTableModel model, ICollection<Models.Type> list)
        {
            try
            {
                var result = new DataTableResultModel<TypeDataTableModel>();
                int totalRecords = list.Count;

                var modifiedData = list.Select(d => new TypeDataTableModel
                {
                    Id = d.Id.ToString(),
                    Name = d.Name,
                    EndTaxType = d.EndTaxType,
                    Debit = d.Debit.ToString(),
                    ExtraInput = d.ExtraInput,
                    fk_StatusId = d.fk_StatusId.ToString(),
                    Status = d.Status.Name,
                    Buttons = "<button class='buttonEditTypeAdmin btn btn-outline-secondary' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalEditTypeAdmin'>Edit</button>" +
                    " | " +
                              "<button class='buttonDeleteTypeAdmin btn btn-outline-danger' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalLabelDeleteTypeAdmin'>Delete</button>"
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
                    EndPoint = d.EndPoint.ToString(),
                    Buttons = "<button class='buttonEditStatusAdmin btn btn-outline-secondary' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalEditStatusAdmin'>Edit</button>" +
                              " | " +
                              "<button class='buttonDeleteStatusAdmin btn btn-outline-danger' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalDeleteStatusAdmin'>Delete</button>"
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
                    RoleId = d.RoleId,
                    Buttons = "<button class='buttonDeleteUserRoleAdmin btn btn-outline-danger' value='" + d.UserId + "' role='" + d.Role + "' data-bs-toggle='modal' data-bs-target='#modalDeleteUserRoleAdmin'>Delete</button>"
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
                    TwoFactorEnabled = d.TwoFactorEnabled.ToString(),
                    Buttons = "<button class='buttonEditUserAdmin btn btn-outline-secondary' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalEditUserAdmin'>Edit</button>" +
                    " | " +
                              "<button class='buttonDeleteUserAdmin btn btn-outline-danger' value='" + d.Id + "' data-bs-toggle='modal' data-bs-target='#modalDeleteUserAdmin'>Delete</button>"
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

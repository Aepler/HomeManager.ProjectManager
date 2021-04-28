using HomeManager.Models.Core;
using HomeManager.Models.DataTables.Finance;
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

namespace HomeManager.Api.Factories
{
    public class DataTableFactory : IDataTableFactory
    {
        public DataTableResponse<WalletDataTable> GetTableData(DataTableInput model, IEnumerable<Wallet> query)
        {
            try
            {
                int totalRecords = query.Count();

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    query = query.Where(p => p.Name.ToLower().Contains(model.search.value.ToLower())
                     );
                }

                int recFilter = query.Count();

                if (model.order.Count > 0)
                {
                    var sortBy = model.columns[model.order[0].column].data;
                    var sortDir = model.order[0].dir.ToLower();

                    if (model.length == -1)
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir);
                    else
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }

                var modifiedData = query.ToList().Select(d => new WalletDataTable
                {
                    Id = d.Id.ToString(),
                    Name = d.Name,
                    Description = d.Description,
                    StartBalance = d.StartBalance.ToString(),
                    CurrentBalance = d.CurrentBalance.ToString()
                }
                    );

                return new DataTableResponse<WalletDataTable>
                {
                    draw = model.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTableResponse<RoleDataTable> GetTableData(DataTableInput model, IEnumerable<Role> query)
        {
            try
            {
                int totalRecords = query.Count();

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    query = query.Where(p => p.Name.ToLower().Contains(model.search.value.ToLower())
                     );
                }

                int recFilter = query.Count();

                if (model.order.Count > 0)
                {
                    var sortBy = model.columns[model.order[0].column].data;
                    var sortDir = model.order[0].dir.ToLower();

                    if (model.length == -1)
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir);
                    else
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }

                var modifiedData = query.ToList().Select(d => new RoleDataTable
                {
                    Id = d.Id.ToString(),
                    Name = d.Name,
                    NormalizedName = d.NormalizedName,
                    ConcurrencyStamp = d.ConcurrencyStamp.ToString()
                }
                    );

                return new DataTableResponse<RoleDataTable>
                {
                    draw = model.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTableResponse<PaymentDataTable> GetTableData(DataTableInput model, IEnumerable<Payment> query)
        {
            try
            {
                int totalRecords = query.Count();

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    query = query.Where(p => p.Date.ToString().Contains(model.search.value) ||
                        p.Description.ToLower().Contains(model.search.value.ToLower()) ||
                        p.Amount.ToString().Contains(model.search.value) ||
                        p.Type.Name.ToLower().Contains(model.search.value.ToLower()) ||
                        p.Category.Name.ToLower().Contains(model.search.value.ToLower())
                     );
                }

                int recFilter = query.Count();

                if (model.order.Count > 0)
                {
                    var sortBy = model.columns[model.order[0].column].data;
                    var sortDir = model.order[0].dir.ToLower();

                    if (model.length == -1)
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir);
                    else
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }

                var modifiedData = query.ToList().Select(d => new PaymentDataTable
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

                return new DataTableResponse<PaymentDataTable>
                {
                    draw = model.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTableResponse<CategoryDataTable> GetTableData(DataTableInput model, IEnumerable<Category> query)
        {
            try
            {
                int totalRecords = query.Count();

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    query = query.Where(p => p.Name.ToLower().Contains(model.search.value.ToLower())
                     );
                }

                int recFilter = query.Count();

                if (model.order.Count > 0)
                {
                    var sortBy = model.columns[model.order[0].column].data;
                    var sortDir = model.order[0].dir.ToLower();

                    if (model.length == -1)
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir);
                    else
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }

                var modifiedData = query.ToList().Select(d => new CategoryDataTable
                {
                    Id = d.Id.ToString(),
                    Name = d.Name
                }
                    );

                return new DataTableResponse<CategoryDataTable>
                {
                    draw = model.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTableResponse<TemplateDataTable> GetTableData(DataTableInput model, IEnumerable<Template> query)
        {
            try
            {
                int totalRecords = query.Count();

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    query = query.Where(p => p.Date.ToString().Contains(model.search.value) ||
                        p.Description.ToLower().Contains(model.search.value.ToLower()) ||
                        p.Amount.ToString().Contains(model.search.value) ||
                        p.Type.Name.ToLower().Contains(model.search.value.ToLower()) ||
                        p.Category.Name.ToLower().Contains(model.search.value.ToLower())
                     );
                }

                int recFilter = query.Count();

                if (model.order.Count > 0)
                {
                    var sortBy = model.columns[model.order[0].column].data;
                    var sortDir = model.order[0].dir.ToLower();

                    if (model.length == -1)
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir);
                    else
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }

                var modifiedData = query.ToList().Select(d => new TemplateDataTable
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

                return new DataTableResponse<TemplateDataTable>
                {
                    draw = model.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTableResponse<TypeDataTable> GetTableData(DataTableInput model, IEnumerable<Type> query)
        {
            try
            {
                int totalRecords = query.Count();

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    query = query.Where(p => p.Name.ToLower().Contains(model.search.value.ToLower()) ||
                        p.EndTaxType.ToString().ToLower().Contains(model.search.value.ToLower()) ||
                        p.TransactionType.ToString().ToLower().Contains(model.search.value.ToLower()) ||
                        p.ExtraInput.ToString().ToLower().Contains(model.search.value.ToLower()) ||
                        p.Status.Name.ToLower().Contains(model.search.value.ToLower())
                     );
                }

                int recFilter = query.Count();

                if (model.order.Count > 0)
                {
                    var sortBy = model.columns[model.order[0].column].data;
                    var sortDir = model.order[0].dir.ToLower();

                    if (model.length == -1)
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir);
                    else
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }

                var modifiedData = query.ToList().Select(d => new TypeDataTable
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

                return new DataTableResponse<TypeDataTable>
                {
                    draw = model.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTableResponse<StatusDataTable> GetTableData(DataTableInput model, IEnumerable<Status> query)
        {
            try
            {
                int totalRecords = query.Count();

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    query = query.Where(p => p.Name.ToLower().Contains(model.search.value.ToLower()) ||
                        p.EndPoint.ToString().ToLower().Contains(model.search.value.ToLower())
                     );
                }

                int recFilter = query.Count();

                if (model.order.Count > 0)
                {
                    var sortBy = model.columns[model.order[0].column].data;
                    var sortDir = model.order[0].dir.ToLower();

                    if (model.length == -1)
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir);
                    else
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }

                var modifiedData = query.ToList().Select(d => new StatusDataTable
                {
                    Id = d.Id.ToString(),
                    Name = d.Name,
                    EndPoint = d.EndPoint.ToString()
                }
                    );

                return new DataTableResponse<StatusDataTable>
                {
                    draw = model.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTableResponse<UserRoleDataTable> GetTableData(DataTableInput model, IEnumerable<UserRoleDataTable> query)
        {
            try
            {
                int totalRecords = query.Count();

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    query = query.Where(p => p.User.ToLower().Contains(model.search.value.ToLower()) ||
                        p.Role.ToLower().Contains(model.search.value.ToLower())
                     );
                }

                int recFilter = query.Count();

                if (model.order.Count > 0)
                {
                    var sortBy = model.columns[model.order[0].column].data;
                    var sortDir = model.order[0].dir.ToLower();

                    if (model.length == -1)
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir);
                    else
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length);
                }
                else
                {
                    query = query.OrderBy(x => x.User);
                }

                var modifiedData = query.ToList().Select(d => new UserRoleDataTable
                {
                    User = d.User,
                    UserId = d.UserId,
                    Role = d.Role,
                    RoleId = d.RoleId
                }
                    );

                return new DataTableResponse<UserRoleDataTable>
                {
                    draw = model.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTableResponse<UserDataTable> GetTableData(DataTableInput model, IEnumerable<User> query)
        {
            try
            {
                int totalRecords = query.Count();

                if (!string.IsNullOrEmpty(model.search.value) &&
                    !string.IsNullOrWhiteSpace(model.search.value))
                {
                    query = query.Where(p => p.UserName.ToLower().Contains(model.search.value.ToLower()) ||
                        p.Email.ToLower().Contains(model.search.value.ToLower()) ||
                        p.FirstName.ToLower().Contains(model.search.value.ToLower()) ||
                        p.LastName.ToLower().Contains(model.search.value.ToLower()) ||
                        p.PhoneNumber.ToLower().Contains(model.search.value.ToLower()) ||
                        p.TwoFactorEnabled.ToString().ToLower().Contains(model.search.value.ToLower())
                     );
                }

                int recFilter = query.Count();

                if (model.order.Count > 0)
                {
                    var sortBy = model.columns[model.order[0].column].data;
                    var sortDir = model.order[0].dir.ToLower();

                    if (model.length == -1)
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir);
                    else
                        query = query.AsQueryable().OrderBy(sortBy + " " + sortDir).Skip(model.start).Take(model.length);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }

                var modifiedData = query.ToList().Select(d => new UserDataTable
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

                return new DataTableResponse<UserDataTable>
                {
                    draw = model.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

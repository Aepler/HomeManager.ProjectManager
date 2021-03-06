using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Services.Finance;
using HomeManager.Models.Interfaces.Repositories.Finance;

namespace HomeManager.Services.Finance
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetById(User user, Guid id)
        {
            try
            {
                var category = _categoryRepository.GetById(id);
                if ((category.fk_UserId == user.Id || category.fk_UserId == null) && !category.Deleted)
                {
                    return category;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Category>> GetAll(User user)
        {
            try
            {
                return _categoryRepository.GetAll().Where(x => x.fk_UserId == user.Id || x.fk_UserId == null && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Category>> GetByUser(User user)
        {
            try
            {
                return _categoryRepository.GetAll().Where(x => x.fk_UserId == user.Id && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Category>> GetDefault()
        {
            try
            {
                return _categoryRepository.GetAll().Where(x => x.fk_UserId == null && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Add(User user, Category category)
        {
            try
            {
                category.fk_UserId = user.Id;
                return _categoryRepository.Add(category);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(User user, Category category)
        {
            try
            {
                var realCategory = await GetById(user, category.Id);
                if (realCategory != null && realCategory.fk_UserId == user.Id)
                {
                    category.fk_UserId = user.Id;
                    return _categoryRepository.Update(category);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(User user, Category category)
        {
            try
            {
                var realCategory = await GetById(user, category.Id);
                if (realCategory != null && realCategory.fk_UserId == user.Id)
                {
                    category.fk_UserId = user.Id;
                    category.Deleted = true;
                    category.DeletedOn = DateTime.Today;
                    return _categoryRepository.Delete(category);
                }
                return false;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> AddDefault(IList<string> userRoles, Category category)
        {
            try
            {

                if (userRoles.Contains("Admin"))
                {
                    category.fk_UserId = null;
                    return _categoryRepository.Add(category);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateDefault(IList<string> userRoles, Category category)
        {
            try
            {
                if (userRoles.Contains("Admin"))
                {
                    var realCategories = await GetDefault();
                    var realCategory = realCategories.Where(x => x.Id == category.Id).FirstOrDefault();
                    if (realCategory != null && realCategory.fk_UserId == null)
                    {
                        category.fk_UserId = null;
                        return _categoryRepository.Update(category);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteDefault(IList<string> userRoles, Category category)
        {
            try
            {
                if (userRoles.Contains("Admin"))
                {
                    var realCategories = await GetDefault();
                    var realCategory = realCategories.Where(x => x.Id == category.Id).FirstOrDefault();
                    if (realCategory != null && realCategory.fk_UserId == null)
                    {
                        category.fk_UserId = null;
                        category.Deleted = true;
                        category.DeletedOn = DateTime.Today;
                        return _categoryRepository.Update(category);
                    }
                }
                return false; 
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

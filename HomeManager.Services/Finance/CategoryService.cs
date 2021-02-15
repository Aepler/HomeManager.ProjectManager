using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Finance;
using HomeManager.Data.Repositories.Interfaces.Finance;

namespace HomeManager.Services.Finance
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetById(User user, int id)
        {
            try
            {
                return await _categoryRepository.GetById(user, id);
            }
            catch (Exception ex)
            {
                return new Category();
            }
        }

        public async Task<ICollection<Category>> GetAll(User user)
        {
            try
            {
                return await _categoryRepository.GetAll(user);
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }

        public async Task<ICollection<Category>> GetByUser(User user)
        {
            try
            {
                return await _categoryRepository.GetByUser(user);
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }

        public async Task<ICollection<Category>> GetDefault()
        {
            try
            {
                return await _categoryRepository.GetDefault();
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }

        public async Task<bool> Add(User user, Category category)
        {
            try
            {
                category.fk_UserId = user.Id;
                return await _categoryRepository.Add(category);
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
                var realCategory = await _categoryRepository.GetById(user, category.Id);
                if (realCategory != null && realCategory.fk_UserId == user.Id)
                {
                    category.fk_UserId = user.Id;
                    return await _categoryRepository.Update(category);
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
                var realCategory = await _categoryRepository.GetById(user, category.Id);
                if (realCategory != null && realCategory.fk_UserId == user.Id)
                {
                    category.fk_UserId = user.Id;
                    category.Deleted = true;
                    category.DeletedOn = DateTime.Today;
                    return await _categoryRepository.Delete(category);
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
                    return await _categoryRepository.Add(category);
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
                    category.fk_UserId = null;
                    return await _categoryRepository.Update(category);
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
                    category.fk_UserId = null;
                    category.Deleted = true;
                    category.DeletedOn = DateTime.Today;
                    return await _categoryRepository.Update(category);
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

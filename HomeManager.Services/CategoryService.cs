using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Models.Interfaces;
using HomeManager.Data.Repositories.Interfaces;

namespace HomeManager.Services
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

        public async Task<bool> Add(User user, Category category)
        {
            try
            {
                return await _categoryRepository.Add(user, category);
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
                return await _categoryRepository.Update(user, category);
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
                return await _categoryRepository.Delete(user, category);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

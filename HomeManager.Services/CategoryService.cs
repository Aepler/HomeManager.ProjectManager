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

        public async Task<bool> Add(Category category)
        {
            try
            {
                return await _categoryRepository.Add(category);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ICollection<Category>> GetAll()
        {
            try
            {
                return await _categoryRepository.GetAll();
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }

        public async Task<Category> GetById(int id)
        {
            try
            {
                return await _categoryRepository.GetById(id);
            }
            catch (Exception ex)
            {
                return new Category();
            }
        }

        public async Task<bool> Update(Category category)
        {
            try
            {
                return await _categoryRepository.Update(category);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

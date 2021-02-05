using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Services.Interfaces;
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

        public bool Add(Category category)
        {
            try
            {
                return _categoryRepository.Add(category);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ICollection<Category> GetAll()
        {
            try
            {
                return _categoryRepository.GetAll();
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }

        public Category GetById(int id)
        {
            try
            {
                return _categoryRepository.GetById(id);
            }
            catch (Exception ex)
            {
                return new Category();
            }
        }

        public bool Update(Category category)
        {
            try
            {
                return _categoryRepository.Update(category);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

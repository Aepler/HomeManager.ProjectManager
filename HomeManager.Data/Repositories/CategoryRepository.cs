using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HomeManagerContext _context;

        public CategoryRepository(HomeManagerContext context)
        {
            _context = context;
        }
        public bool Add(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ICollection<Category> GetAll()
        {
            ICollection<Category> categories = _context.Categories.ToList();
            return categories;
        }

        public Category GetById(int id)
        {
            ICollection<Category> categories = GetAll();
            return categories.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Update(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HomeManagerContext _context;

        public CategoryRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public async Task<Category> GetById(int id)
        {
            Category category = await _context.Categories.Where(x => x.Id == id && x.Deleted == false).FirstOrDefaultAsync();
            return category;
        }

        public async Task<ICollection<Category>> GetAll()
        {
            ICollection<Category> categories = await _context.Categories.Where(x => x.Deleted == false).ToListAsync();
            return categories;
        }



        public async Task<bool> Add(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

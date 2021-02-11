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

        public async Task<Category> GetById(User user, int id)
        {
            Category category = await _context.Categories.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.Id == id && x.Deleted == false).FirstOrDefaultAsync();
            return category;
        }

        public async Task<ICollection<Category>> GetAll(User user)
        {
            ICollection<Category> categories = await _context.Categories.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.Deleted == false).ToListAsync();
            return categories;
        }

        public async Task<ICollection<Category>> GetByUser(User user)
        {
            ICollection<Category> categories = await _context.Categories.Where(x => x.fk_UserId == user.Id && x.Deleted == false).ToListAsync();
            return categories;
        }

        public async Task<bool> Add(User user, Category category)
        {
            try
            {
                category.fk_UserId = user.Id;
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return true;
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
                var realCategory = await _context.Categories.AsNoTracking().FirstAsync(x => x.Id == category.Id);
                if (realCategory != null && realCategory.fk_UserId == user.Id)
                {
                    category.fk_UserId = user.Id;
                    _context.Categories.Update(category);
                    await _context.SaveChangesAsync();

                    return true;
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
                var realCategory = await _context.Categories.FindAsync(category.Id);
                if (realCategory != null && realCategory.fk_UserId == user.Id)
                {
                    category.Deleted = true;
                    category.DeletedOn = DateTime.Today;
                    _context.Categories.Update(category);
                    await _context.SaveChangesAsync();

                    return true;
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

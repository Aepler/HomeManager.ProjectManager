using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Data.Repositories.Interfaces.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Data.Repositories.Finance
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
            Category category = await _context.FinanceCategories.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.Id == id && x.Deleted == false).FirstOrDefaultAsync();
            return category;
        }

        public async Task<ICollection<Category>> GetAll(User user)
        {
            ICollection<Category> categories = await _context.FinanceCategories.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.Deleted == false).ToListAsync();
            return categories;
        }

        public async Task<ICollection<Category>> GetByUser(User user)
        {
            ICollection<Category> categories = await _context.FinanceCategories.Where(x => x.fk_UserId == user.Id && x.Deleted == false).ToListAsync();
            return categories;
        }

        public async Task<ICollection<Category>> GetDefault()
        {
            ICollection<Category> categories = await _context.FinanceCategories.Where(x => x.fk_UserId == null && x.Deleted == false).ToListAsync();
            return categories;
        }

        public async Task<bool> Add(Category category)
        {
            try
            {
                _context.FinanceCategories.Add(category);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(Category category)
        {
            try
            {
                _context.FinanceCategories.Update(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(Category category)
        {
            try
            {
                _context.FinanceCategories.Update(category);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

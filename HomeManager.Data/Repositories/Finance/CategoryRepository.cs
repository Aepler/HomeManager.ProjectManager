using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Repositories.Finance;
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
        private readonly HomeManagerDbContext _context;

        public CategoryRepository(HomeManagerDbContext context)
        {
            _context = context;
        }

        public Category GetById(Guid id)
        {
            return _context.FinanceCategories.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<Category> GetAll()
        {
            return _context.FinanceCategories;
        }

        public bool Add(Category category)
        {
            try
            {
                _context.FinanceCategories.Add(category);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(Category category)
        {
            try
            {
                _context.FinanceCategories.Update(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(Category category)
        {
            try
            {
                _context.FinanceCategories.Update(category);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

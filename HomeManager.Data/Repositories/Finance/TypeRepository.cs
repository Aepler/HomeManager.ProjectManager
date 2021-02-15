using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Data.Repositories.Interfaces.Finance;
using Microsoft.EntityFrameworkCore;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.Data.Repositories.Finance
{
    public class TypeRepository : ITypeRepository
    {
        private readonly HomeManagerContext _context;

        public TypeRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public async Task<Type> GetById(User user, int id)
        {
            return await _context.FinanceTypes.Include(x => x.Status).Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.Id == id && x.Deleted == false).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Type>> GetAll(User user)
        {
            return await _context.FinanceTypes.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.Deleted == false).Include(x => x.Status).ToListAsync();
        }

        public async Task<ICollection<Type>> GetByUser(User user)
        {
            return await _context.FinanceTypes.Where(x => x.fk_UserId == user.Id && x.Deleted == false).Include(x => x.Status).ToListAsync();
        }

        public async Task<ICollection<Type>> GetDefault()
        {
            return await _context.FinanceTypes.Where(x => x.fk_UserId == null && x.Deleted == false).Include(x => x.Status).ToListAsync();
        }

        public async Task<bool> Add(Type type)
        {
            try
            {
                _context.FinanceTypes.Add(type);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(Type type)
        {
            try
            {
                _context.FinanceTypes.Update(type);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(Type type)
        {
            try
            {
                _context.FinanceTypes.Update(type);
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

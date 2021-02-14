using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Data.Repositories.Interfaces;
using HomeManager.Models;
using Microsoft.EntityFrameworkCore;
using Type = HomeManager.Models.Type;

namespace HomeManager.Data.Repositories
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
            Type type = await _context.Types.Include(x => x.Status).Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.Id == id && x.Deleted == false).FirstOrDefaultAsync();
            return type;
        }

        public async Task<ICollection<Type>> GetAll(User user)
        {
            ICollection<Type> types = await _context.Types.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.Deleted == false).Include(x => x.Status).ToListAsync();
            return types;
        }

        public async Task<ICollection<Type>> GetByUser(User user)
        {
            ICollection<Type> types = await _context.Types.Where(x => x.fk_UserId == user.Id && x.Deleted == false).Include(x => x.Status).ToListAsync();
            return types;
        }

        public async Task<ICollection<Type>> GetDefault()
        {
            ICollection<Type> types = await _context.Types.Where(x => x.fk_UserId == null && x.Deleted == false).Include(x => x.Status).ToListAsync();
            return types;
        }

        public async Task<bool> Add(Type type)
        {
            try
            {
                _context.Types.Add(type);
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
                _context.Types.Update(type);
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
                _context.Types.Update(type);
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

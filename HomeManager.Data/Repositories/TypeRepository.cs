using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Data.Repositories.Interfaces;
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

        public async Task<Type> GetById(int id)
        {
            Type type = await _context.Types.Where(x => x.Id == id && x.Deleted == false).FirstOrDefaultAsync();
            return type;
        }

        public async Task<ICollection<Type>> GetAll()
        {
            ICollection<Type> types = await _context.Types.Where(x => x.Deleted == false).ToListAsync();
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
                return false;
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
                return false;
            }
        }
    }
}

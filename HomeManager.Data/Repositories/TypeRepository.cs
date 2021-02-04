using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool Add(Type type)
        {
            try
            {
                _context.Types.Add(type);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ICollection<Type> GetAll()
        {
            ICollection<Type> types = _context.Types.ToList();
            return types;
        }

        public Type GetById(int id)
        {
            ICollection<Type> types = GetAll();
            return types.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Update(Type type)
        {        
            try
            {
                _context.Types.Update(type);
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

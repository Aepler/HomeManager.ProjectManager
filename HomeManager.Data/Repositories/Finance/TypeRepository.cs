using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Repositories.Finance;
using Microsoft.EntityFrameworkCore;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.Data.Repositories.Finance
{
    public class TypeRepository : ITypeRepository
    {
        private readonly HomeManagerDbContext _context;

        public TypeRepository(HomeManagerDbContext context)
        {
            _context = context;
        }

        public Type GetById(Guid id)
        {
            return _context.FinanceTypes.Where(x => x.Id == id).Include(x => x.Status).FirstOrDefault();
        }

        public IQueryable<Type> GetAll()
        {
            return _context.FinanceTypes.Include(x => x.Status);
        }

        public bool Add(Type type)
        {
            try
            {
                _context.FinanceTypes.Add(type);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(Type type)
        {
            try
            {
                _context.FinanceTypes.Update(type);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(Type type)
        {
            try
            {
                _context.FinanceTypes.Update(type);
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

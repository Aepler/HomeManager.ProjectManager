using HomeManager.Models.Interfaces.Repositories.Finance;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories.Finance
{
    public class RepeatingRepository : IRepeatingRepository
    {
        private readonly HomeManagerContext _context;

        public RepeatingRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public Repeating GetById(Guid id)
        {
            return _context.FinanceRepeatings.Where(x => x.Id == id).Include(x => x.Category).Include(x => x.Type).Include(x => x.User).FirstOrDefault();
        }

        public ICollection<Repeating> GetAll()
        {
            return _context.FinanceRepeatings.Include(x => x.Category).Include(x => x.Type).Include(x => x.User).ToList();
        }

        public bool Add(Repeating repeating)
        {
            try
            {
                _context.FinanceRepeatings.Add(repeating);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(Repeating repeating)
        {
            try
            {
                _context.FinanceRepeatings.Update(repeating);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(Repeating repeating)
        {
            try
            {
                _context.FinanceRepeatings.Update(repeating);
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

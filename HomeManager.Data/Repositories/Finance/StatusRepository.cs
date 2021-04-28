using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Repositories.Finance;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Data.Repositories.Finance
{
    public class StatusRepository : IStatusRepository
    {
        private readonly HomeManagerDbContext _context;

        public StatusRepository(HomeManagerDbContext context)
        {
            _context = context;
        }

        public Status GetById(Guid id)
        {
            return _context.FinanceStatuses.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<Status> GetAll()
        {
            return _context.FinanceStatuses;
        }

        public bool Add(Status status)
        {
            try
            {
                _context.FinanceStatuses.Add(status);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(Status status)
        {
            try
            {
                _context.FinanceStatuses.Update(status);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(Status status)
        {
            try
            {
                _context.FinanceStatuses.Update(status);
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

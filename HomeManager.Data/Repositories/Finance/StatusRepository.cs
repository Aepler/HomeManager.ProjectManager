using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Data.Repositories.Interfaces.Finance;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Data.Repositories.Finance
{
    public class StatusRepository : IStatusRepository
    {
        private readonly HomeManagerContext _context;

        public StatusRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public async Task<Status> GetById(User user, int id)
        {
            return await _context.FinanceStatuses.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.Id == id && !x.Deleted).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Status>> GetAll(User user)
        {
            return await _context.FinanceStatuses.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && !x.Deleted).ToListAsync();
        }

        public async Task<ICollection<Status>> GetByUser(User user)
        {
            return await _context.FinanceStatuses.Where(x => x.fk_UserId == user.Id && !x.Deleted).ToListAsync();
        }

        public async Task<ICollection<Status>> GetByEndPoint(User user, bool endPoint)
        {
            return await _context.FinanceStatuses.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.EndPoint == endPoint && !x.Deleted).ToListAsync();
        }

        public async Task<ICollection<Status>> GetByTypeId(User user, int typeId)
        {
            return await _context.FinanceStatuses.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && (x.EndPoint == false || x.Id == typeId) && !x.Deleted).ToListAsync();
        }

        public async Task<ICollection<Status>> GetDefault()
        {
            return await _context.FinanceStatuses.Where(x => x.fk_UserId == null && !x.Deleted).ToListAsync();
        }

        public async Task<bool> Add(Status status)
        {
            try
            {
                _context.FinanceStatuses.Add(status);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(Status status)
        {
            try
            {
                _context.FinanceStatuses.Update(status);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(Status status)
        {
            try
            {
                _context.FinanceStatuses.Update(status);
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

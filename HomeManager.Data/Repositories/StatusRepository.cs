using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Data.Repositories
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
            Status status = await _context.Statuses.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.Id == id && x.Deleted == false).FirstOrDefaultAsync();
            return status;
        }

        public async Task<ICollection<Status>> GetAll(User user)
        {
            ICollection<Status> statuses = await _context.Statuses.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.Deleted == false).ToListAsync();
            return statuses;
        }

        public async Task<ICollection<Status>> GetByUser(User user)
        {
            ICollection<Status> statuses = await _context.Statuses.Where(x => x.fk_UserId == user.Id && x.Deleted == false).ToListAsync();
            return statuses;
        }

        public async Task<ICollection<Status>> GetByEndPoint(User user, bool endPoint)
        {
            ICollection<Status> statuses = await _context.Statuses.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && x.EndPoint == endPoint && x.Deleted == false).ToListAsync();
            return statuses;
        }

        public async Task<ICollection<Status>> GetPossibleStatus(User user, int id)
        {
            ICollection<Status> statuses = await _context.Statuses.Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && (x.EndPoint == false || x.Id == id) && x.Deleted == false).ToListAsync();
            return statuses;
        }

        public async Task<ICollection<Status>> GetDefault()
        {
            ICollection<Status> statuses = await _context.Statuses.Where(x => x.fk_UserId == null && x.Deleted == false).ToListAsync();
            return statuses;
        }

        public async Task<bool> Add(Status status)
        {
            try
            {
                _context.Statuses.Add(status);
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
                _context.Statuses.Update(status);
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
                _context.Statuses.Update(status);
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

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

        public async Task<Status> GetById(int id)
        {
            Status status = await _context.Statuses.Where(x => x.Id == id).FirstOrDefaultAsync();
            return status;
        }

        public async Task<ICollection<Status>> GetAll()
        {
            ICollection<Status> statuses = await _context.Statuses.ToListAsync();
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
                return false;
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
                return false;
            }
        }
    }
}

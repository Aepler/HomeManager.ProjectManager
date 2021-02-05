using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Data.Repositories.Interfaces;

namespace HomeManager.Data.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly HomeManagerContext _context;

        public StatusRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public Status GetById(int id)
        {
            Status status = _context.Statuses.Find(id);
            return status;
        }

        public ICollection<Status> GetAll()
        {
            ICollection<Status> statuses = _context.Statuses.ToList();
            return statuses;
        }



        public bool Add(Status status)
        {
            try
            {
                _context.Statuses.Add(status);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Status status)
        {
            try
            {
                _context.Statuses.Update(status);
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

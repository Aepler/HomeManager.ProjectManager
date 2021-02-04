using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly HomeManagerContext _context;

        public StatusRepository(HomeManagerContext context)
        {
            _context = context;
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

        public ICollection<Status> GetAll()
        {
            ICollection<Status> statuses = _context.Statuses.ToList();
            return statuses;
        }

        public Status GetById(int id)
        {
            ICollection<Status> statuses = GetAll();
            return statuses.Where(x => x.Id == id).FirstOrDefault();
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

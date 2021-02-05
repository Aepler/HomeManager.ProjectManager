using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Services.Interfaces;
using HomeManager.Data.Repositories.Interfaces;

namespace HomeManager.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public bool Add(Status status)
        {
            try
            {
                return _statusRepository.Add(status);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ICollection<Status> GetAll()
        {
            try
            {
                return _statusRepository.GetAll();
            }
            catch (Exception ex)
            {
                return new List<Status>();
            }
        }

        public Status GetById(int id)
        {
            try
            {
                return _statusRepository.GetById(id);
            }
            catch (Exception ex)
            {
                return new Status();
            }
        }

        public bool Update(Status status)
        {
            try
            {
                return _statusRepository.Update(status);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

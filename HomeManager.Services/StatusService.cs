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

        public async Task<bool> Add(Status status)
        {
            try
            {
                return await _statusRepository.Add(status);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ICollection<Status>> GetAll()
        {
            try
            {
                return await _statusRepository.GetAll();
            }
            catch (Exception ex)
            {
                return new List<Status>();
            }
        }

        public async Task<Status> GetById(int id)
        {
            try
            {
                return await _statusRepository.GetById(id);
            }
            catch (Exception ex)
            {
                return new Status();
            }
        }

        public async Task<bool> Update(Status status)
        {
            try
            {
                return await _statusRepository.Update(status);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

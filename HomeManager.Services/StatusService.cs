using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Models.Interfaces;
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

        public async Task<Status> GetById(User user, int id)
        {
            try
            {
                return await _statusRepository.GetById(user, id);
            }
            catch (Exception ex)
            {
                return new Status();
            }
        }

        public async Task<ICollection<Status>> GetAll(User user)
        {
            try
            {
                return await _statusRepository.GetAll(user);
            }
            catch (Exception ex)
            {
                return new List<Status>();
            }
        }

        public async Task<ICollection<Status>> GetByUser(User user)
        {
            try
            {
                return await _statusRepository.GetByUser(user);
            }
            catch (Exception ex)
            {
                return new List<Status>();
            }
        }

        public async Task<ICollection<Status>> GetByEndPoint(User user, bool endPoint)
        {
            try
            {
                return await _statusRepository.GetByEndPoint(user, endPoint);
            }
            catch (Exception ex)
            {
                return new List<Status>();
            }
        }

        public async Task<ICollection<Status>> GetPossibleStatus(User user, int id)
        {
            try
            {
                return await _statusRepository.GetPossibleStatus(user, id);
            }
            catch (Exception ex)
            {
                return new List<Status>();
            }
        }

        public async Task<bool> Add(User user, Status status)
        {
            try
            {
                return await _statusRepository.Add(user, status);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(User user, Status status)
        {
            try
            {
                return await _statusRepository.Update(user, status);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(User user, Status status)
        {
            try
            {
                return await _statusRepository.Delete(user, status);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

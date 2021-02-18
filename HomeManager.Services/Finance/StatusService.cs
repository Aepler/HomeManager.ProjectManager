using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Finance;
using HomeManager.Data.Repositories.Interfaces.Finance;

namespace HomeManager.Services.Finance
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

        public async Task<ICollection<Status>> GetByTypeId(User user, int typeId)
        {
            try
            {
                return await _statusRepository.GetByTypeId(user, typeId);
            }
            catch (Exception ex)
            {
                return new List<Status>();
            }
        }
        public async Task<ICollection<Status>> GetDefault()
        {
            try
            {
                return await _statusRepository.GetDefault();
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
                status.fk_UserId = user.Id;
                return await _statusRepository.Add(status);
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
                var realStatus = await _statusRepository.GetById(user, status.Id);
                if (realStatus != null && realStatus.fk_UserId == user.Id)
                {
                    status.fk_UserId = user.Id;
                    return await _statusRepository.Update(status);
                }
                return false;  
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
                var realStatus = await _statusRepository.GetById(user, status.Id);
                if (realStatus != null && realStatus.fk_UserId == user.Id)
                {
                    status.fk_UserId = user.Id;
                    status.Deleted = true;
                    status.DeletedOn = DateTime.Today;
                    return await _statusRepository.Delete(status);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> AddDefault(IList<string> userRoles, Status status)
        {
            try
            {
                if (userRoles.Contains("Admin"))
                {
                    status.fk_UserId = null;
                    return await _statusRepository.Add(status);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateDefault(IList<string> userRoles, Status status)
        {
            try
            {
                if (userRoles.Contains("Admin"))
                {
                    status.fk_UserId = null;
                    return await _statusRepository.Update(status);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteDefault(IList<string> userRoles, Status status)
        {
            try
            {
                if (userRoles.Contains("Admin"))
                {
                    status.fk_UserId = null;
                    status.Deleted = true;
                    status.DeletedOn = DateTime.Today;
                    return await _statusRepository.Update(status);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Services.Finance;
using HomeManager.Models.Interfaces.Repositories.Finance;
using Type = HomeManager.Models.Entities.Finance.Type;
using HomeManager.Models.Enums;

namespace HomeManager.Services.Finance
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<Status> GetById(User user, Guid id)
        {
            try
            {
                var status = _statusRepository.GetById(id);
                if ((status.fk_UserId == user.Id || status.fk_UserId == null) && !status.Deleted)
                {
                    return status;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Status>> GetAll(User user)
        {
            try
            {
                return _statusRepository.GetAll().Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Status>> GetByUser(User user)
        {
            try
            {
                return _statusRepository.GetAll().Where(x => x.fk_UserId == user.Id && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Status>> GetByEndPoint(User user, bool endPoint)
        {
            try
            {
                var statuses = await GetAll(user);
                return statuses.Where(x => x.EndPoint == endPoint).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Status>> GetByTypeId(User user, Type type)
        {
            try
            {
                var statuses = await GetAll(user);
                if (type.TransactionType == PaymentTransactionType.Both)
                {
                    return statuses.ToList();
                }
                else
                {
                    return statuses.Where(x => x.EndPoint == false || x.Id == type.fk_StatusId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ICollection<Status>> GetDefault()
        {
            try
            {
                return _statusRepository.GetAll().Where(x => x.fk_UserId == null && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<bool> Add(User user, Status status)
        {
            try
            {
                status.fk_UserId = user.Id;
                return _statusRepository.Add(status);
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
                var realStatus = await GetById(user, status.Id);
                if (realStatus != null && realStatus.fk_UserId == user.Id)
                {
                    status.fk_UserId = user.Id;
                    return _statusRepository.Update(status);
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
                var realStatus = await GetById(user, status.Id);
                if (realStatus != null && realStatus.fk_UserId == user.Id)
                {
                    status.fk_UserId = user.Id;
                    status.Deleted = true;
                    status.DeletedOn = DateTime.Today;
                    return _statusRepository.Delete(status);
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
                    return _statusRepository.Add(status);
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
                    var realStatuses = await GetDefault();
                    var realStatus = realStatuses.Where(x => x.Id == status.Id).FirstOrDefault();
                    if (realStatus != null && realStatus.fk_UserId == null)
                    {
                        status.fk_UserId = null;
                        return _statusRepository.Update(status);
                    }
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
                    var realStatuses = await GetDefault();
                    var realStatus = realStatuses.Where(x => x.Id == status.Id).FirstOrDefault();
                    if (realStatus != null && realStatus.fk_UserId == null)
                    {
                        status.fk_UserId = null;
                        status.Deleted = true;
                        status.DeletedOn = DateTime.Today;
                        return _statusRepository.Update(status);
                    }
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

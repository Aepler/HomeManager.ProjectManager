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

namespace HomeManager.Services.Finance
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepository _typeRepository;

        public TypeService(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public async Task<Type> GetById(User user, Guid id)
        {
            try
            {
                var type = _typeRepository.GetById(id);
                if ((type.fk_UserId == user.Id || type.fk_UserId == null) && !type.Deleted)
                {
                    return type;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Type>> GetAll(User user)
        {
            try
            {
                return _typeRepository.GetAll().Where(x => (x.fk_UserId == user.Id || x.fk_UserId == null) && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Type>> GetByUser(User user)
        {
            try
            {
                return _typeRepository.GetAll().Where(x => x.fk_UserId == user.Id && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Type>> GetDefault()
        {
            try
            {
                return _typeRepository.GetAll().Where(x => x.fk_UserId == null && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Add(User user, Type type)
        {
            try
            {
                type.fk_UserId = user.Id;
                return _typeRepository.Add(type);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(User user, Type type)
        {
            try
            {
                var realType = await GetById(user, type.Id);
                if (realType != null && realType.fk_UserId == user.Id)
                {
                    type.fk_UserId = user.Id;
                    return _typeRepository.Update(type);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(User user, Type type)
        {
            try
            {
                var realType = await GetById(user, type.Id);
                if (realType != null && realType.fk_UserId == user.Id)
                {
                    type.fk_UserId = user.Id;
                    type.Deleted = true;
                    type.DeletedOn = DateTime.Today;
                    return _typeRepository.Delete(type);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> AddDefault(IList<string> userRoles, Type type)
        {
            try
            {
                if (userRoles.Contains("Admin"))
                {
                    type.fk_UserId = null;
                    return _typeRepository.Add(type);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateDefault(IList<string> userRoles, Type type)
        {
            try
            {
                if (userRoles.Contains("Admin"))
                {
                    var realTypes = await GetDefault();
                    var realType = realTypes.Where(x => x.Id == type.Id).FirstOrDefault();
                    if (realType != null && realType.fk_UserId == null)
                    {
                        type.fk_UserId = null;
                        return _typeRepository.Update(type);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteDefault(IList<string> userRoles, Type type)
        {
            try
            {
                if (userRoles.Contains("Admin"))
                {
                    var realTypes = await GetDefault();
                    var realType = realTypes.Where(x => x.Id == type.Id).FirstOrDefault();
                    if (realType != null && realType.fk_UserId == null)
                    {
                        type.fk_UserId = null;
                        type.Deleted = true;
                        type.DeletedOn = DateTime.Today;
                        return _typeRepository.Update(type);
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

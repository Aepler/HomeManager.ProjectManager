using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Finance;
using HomeManager.Data.Repositories.Interfaces.Finance;
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

        public async Task<Type> GetById(User user, int id)
        {
            try
            {
                return await _typeRepository.GetById(user, id);
            }
            catch (Exception ex)
            {
                return new Type();
            }
        }

        public async Task<ICollection<Type>> GetAll(User user)
        {
            try
            {
                return await _typeRepository.GetAll(user);
            }
            catch (Exception ex)
            {
                return new List<Type>();
            }
        }

        public async Task<ICollection<Type>> GetByUser(User user)
        {
            try
            {
                return await _typeRepository.GetByUser(user);
            }
            catch (Exception ex)
            {
                return new List<Type>();
            }
        }

        public async Task<ICollection<Type>> GetDefault()
        {
            try
            {
                return await _typeRepository.GetDefault();
            }
            catch (Exception ex)
            {
                return new List<Type>();
            }
        }

        public async Task<bool> Add(User user, Type type)
        {
            try
            {
                type.fk_UserId = user.Id;
                return await _typeRepository.Add(type);
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
                var realType = await _typeRepository.GetById(user, type.Id);
                if (realType != null && realType.fk_UserId == user.Id)
                {
                    type.fk_UserId = user.Id;
                    return await _typeRepository.Update(type);
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
                var realType = await _typeRepository.GetById(user, type.Id);
                if (realType != null && realType.fk_UserId == user.Id)
                {
                    type.fk_UserId = user.Id;
                    type.Deleted = true;
                    type.DeletedOn = DateTime.Today;
                    return await _typeRepository.Delete(type);
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
                    return await _typeRepository.Add(type);
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
                    type.fk_UserId = null;
                    return await _typeRepository.Update(type);
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
                    type.fk_UserId = null;
                    type.Deleted = true;
                    type.DeletedOn = DateTime.Today;
                    return await _typeRepository.Update(type);
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

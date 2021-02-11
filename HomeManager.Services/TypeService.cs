using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Models.Interfaces;
using HomeManager.Data.Repositories.Interfaces;
using Type = HomeManager.Models.Type;

namespace HomeManager.Services
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

        public async Task<bool> Add(User user, Type type)
        {
            try
            {
                return await _typeRepository.Add(user, type);
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
                return await _typeRepository.Update(user, type);
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
                return await _typeRepository.Delete(user, type);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Services.Interfaces;
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

        public async Task<bool> Add(Type type)
        {
            try
            {
                return await _typeRepository.Add(type);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ICollection<Type>> GetAll()
        {
            try
            {
                return await _typeRepository.GetAll();
            }
            catch (Exception ex)
            {
                return new List<Type>();
            }
        }

        public async Task<Type> GetById(int id)
        {
            try
            {
                return await _typeRepository.GetById(id);
            }
            catch (Exception ex)
            {
                return new Type();
            }
        }

        public async Task<bool> Update(Type type)
        {
            try
            {
                return await _typeRepository.Update(type);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

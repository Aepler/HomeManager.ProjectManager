using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Models.Interfaces;
using HomeManager.Data.Repositories;
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

        public bool Add(Type type)
        {
            try
            {
                return _typeRepository.Add(type);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ICollection<Type> GetAll()
        {
            try
            {
                return _typeRepository.GetAll();
            }
            catch (Exception ex)
            {
                return new List<Type>();
            }
        }

        public Type GetById(int id)
        {
            try
            {
                return _typeRepository.GetById(id);
            }
            catch (Exception ex)
            {
                return new Type();
            }
        }

        public bool Update(Type type)
        {
            try
            {
                return _typeRepository.Update(type);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

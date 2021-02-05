using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using Type = HomeManager.Models.Type;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface ITypeRepository
    {
        Task<Type> GetById(int id);
        Task<ICollection<Type>> GetAll();
        Task<bool> Add(Type type);
        Task<bool> Update(Type type);
    }
}

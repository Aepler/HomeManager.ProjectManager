using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using Type = HomeManager.Models.Type;

namespace HomeManager.Models.Interfaces
{
    public interface ITypeService
    {
        Task<Type> GetById(User user, int id);
        Task<ICollection<Type>> GetAll(User user);
        Task<ICollection<Type>> GetByUser(User user);
        Task<bool> Add(User user, Type type);
        Task<bool> Update(User user, Type type);
        Task<bool> Delete(User user, Type type);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Entities;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.Data.Repositories.Interfaces.Finance
{
    public interface ITypeRepository
    {
        Task<Type> GetById(User user, int id);
        Task<ICollection<Type>> GetAll(User user);
        Task<ICollection<Type>> GetByUser(User user);
        Task<ICollection<Type>> GetDefault();
        Task<bool> Add(Type type);
        Task<bool> Update(Type type);
        Task<bool> Delete(Type type);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.Models.Interfaces.Finance
{
    public interface ITypeService
    {
        Task<Type> GetById(User user, int id);
        Task<ICollection<Type>> GetAll(User user);
        Task<ICollection<Type>> GetByUser(User user);
        Task<ICollection<Type>> GetDefault();
        Task<bool> Add(User user, Type type);
        Task<bool> Update(User user, Type type);
        Task<bool> Delete(User user, Type type);
        Task<bool> AddDefault(IList<string> userRoles, Type type);
        Task<bool> UpdateDefault(IList<string> userRoles, Type type);
        Task<bool> DeleteDefault(IList<string> userRoles, Type type);
    }
}

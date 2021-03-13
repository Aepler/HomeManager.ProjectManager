using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.Models.Interfaces.Services.Finance
{
    public interface ITypeService
    {
        Type GetById(User user, Guid id);
        ICollection<Type> GetAll(User user);
        ICollection<Type> GetByUser(User user);
        ICollection<Type> GetDefault();
        bool Add(User user, Type type);
        bool Update(User user, Type type);
        bool Delete(User user, Type type);
        bool AddDefault(IList<string> userRoles, Type type);
        bool UpdateDefault(IList<string> userRoles, Type type);
        bool DeleteDefault(IList<string> userRoles, Type type);
    }
}

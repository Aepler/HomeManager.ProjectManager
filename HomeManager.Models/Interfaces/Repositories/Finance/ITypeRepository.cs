using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.Models.Interfaces.Repositories.Finance
{
    public interface ITypeRepository
    {
        Type GetById(Guid id);
        IQueryable<Type> GetAll();
        bool Add(Type type);
        bool Update(Type type);
        bool Delete(Type type);
    }
}

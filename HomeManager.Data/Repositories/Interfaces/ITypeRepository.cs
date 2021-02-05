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
        Type GetById(int id);
        ICollection<Type> GetAll();
        bool Add(Type type);
        bool Update(Type type);
    }
}

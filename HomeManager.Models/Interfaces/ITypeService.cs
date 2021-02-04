using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Interfaces
{
    public interface ITypeService
    {
        Type GetById(int id);
        ICollection<Type> GetAll();
        bool Add(Type type);
        bool Update(Type type);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Repositories.Finance
{
    public interface IRepeatingRepository
    {
        Repeating GetById(Guid id);
        IQueryable<Repeating> GetAll();
        bool Add(Repeating repeating);
        bool Update(Repeating repeating);
        bool Delete(Repeating repeating);
    }
}

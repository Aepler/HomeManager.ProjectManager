using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Interfaces.Services.Finance
{
    public interface IRepeatingService
    {
        Repeating GetById(User user, Guid id);
        IEnumerable<Repeating> GetAll(User user);
        IEnumerable<Repeating> GetByType(User user, Guid typeId);
        IEnumerable<Repeating> GetByCategory(User user, Guid categoryId);
        bool Add(User user, Repeating repeating);
        bool Update(User user, Repeating repeating);
        bool Delete(User user, Repeating repeating);
    }
}

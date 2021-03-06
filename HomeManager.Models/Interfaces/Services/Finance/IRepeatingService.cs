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
        Task<Repeating> GetById(User user, Guid id);
        Task<ICollection<Repeating>> GetAll(User user);
        Task<ICollection<Repeating>> GetByType(User user, Guid typeId);
        Task<ICollection<Repeating>> GetByCategory(User user, Guid categoryId);
        Task<bool> Add(User user, Repeating repeating);
        Task<bool> Update(User user, Repeating repeating);
        Task<bool> Delete(User user, Repeating repeating);
    }
}

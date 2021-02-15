using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Entities;

namespace HomeManager.Data.Repositories.Interfaces.Finance
{
    public interface ICategoryRepository
    {
        Task<Category> GetById(User user, int id);
        Task<ICollection<Category>> GetAll(User user);
        Task<ICollection<Category>> GetByUser(User user);
        Task<ICollection<Category>> GetDefault();
        Task<bool> Add(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(Category category);
    }
}

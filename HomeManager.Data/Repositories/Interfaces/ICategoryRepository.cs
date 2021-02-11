using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetById(User user, int id);
        Task<ICollection<Category>> GetAll(User user);
        Task<ICollection<Category>> GetByUser(User user);
        Task<bool> Add(User user, Category category);
        Task<bool> Update(User user, Category category);
        Task<bool> Delete(User user, Category category);
    }
}

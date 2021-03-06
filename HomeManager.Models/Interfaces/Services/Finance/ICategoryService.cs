using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Services.Finance
{
    public interface ICategoryService
    {
        Task<Category> GetById(User user, Guid id);
        Task<ICollection<Category>> GetAll(User user);
        Task<ICollection<Category>> GetByUser(User user);
        Task<ICollection<Category>> GetDefault();
        Task<bool> Add(User user, Category category);
        Task<bool> Update(User user, Category category);
        Task<bool> Delete(User user, Category category);
        Task<bool> AddDefault(IList<string> userRoles, Category category);
        Task<bool> UpdateDefault(IList<string> userRoles, Category category);
        Task<bool> DeleteDefault(IList<string> userRoles, Category category);
    }
}

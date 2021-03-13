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
        Category GetById(User user, Guid id);
        ICollection<Category> GetAll(User user);
        ICollection<Category> GetByUser(User user);
        ICollection<Category> GetDefault();
        bool Add(User user, Category category);
        bool Update(User user, Category category);
        bool Delete(User user, Category category);
        bool AddDefault(IList<string> userRoles, Category category);
        bool UpdateDefault(IList<string> userRoles, Category category);
        bool DeleteDefault(IList<string> userRoles, Category category);
    }
}

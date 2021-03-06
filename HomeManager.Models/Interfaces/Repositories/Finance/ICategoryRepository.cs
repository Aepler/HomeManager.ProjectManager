using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Repositories.Finance
{
    public interface ICategoryRepository
    {
        Category GetById(Guid id);
        ICollection<Category> GetAll();
        bool Add(Category category);
        bool Update(Category category);
        bool Delete(Category category);
    }
}

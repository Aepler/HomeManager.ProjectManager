using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Repositories.Finance
{
    public interface ICategoryRepository
    {
        Category GetById(Guid id);
        IQueryable<Category> GetAll();
        bool Add(Category category);
        bool Update(Category category);
        bool Delete(Category category);
    }
}

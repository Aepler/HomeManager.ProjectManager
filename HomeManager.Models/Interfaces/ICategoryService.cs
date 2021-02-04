using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Interfaces
{
    public interface ICategoryService
    {
        Category GetById(int id);
        ICollection<Category> GetAll();
        bool Add(Category category);
        bool Update(Category category);
    }
}

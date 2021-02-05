using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetById(int id);
        Task<ICollection<Category>> GetAll();
        Task<bool> Add(Category category);
        Task<bool> Update(Category category);
    }
}

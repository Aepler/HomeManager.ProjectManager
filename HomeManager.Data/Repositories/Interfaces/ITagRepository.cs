using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag> GetById(int id);
        Task<Tag> GetByName (int id);
        Task<ICollection<Tag>> GetAll(User user);
        Task<ICollection<Tag>> GetByRecipe(Recipe recipe);
        Task<ICollection<Tag>> GetByIngredient(Ingredient ingredient);
        Task<bool> Add(User user, Tag recipe);
        Task<bool> Update(User user, Tag recipe);
        Task<bool> Delete(User user, Tag recipe);
    }
}

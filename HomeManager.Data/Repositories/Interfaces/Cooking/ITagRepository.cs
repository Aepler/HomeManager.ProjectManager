using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Cooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories.Interfaces.Cooking
{
    public interface ITagRepository
    {
        Task<Tag> GetById(User user, int id);
        Task<Tag> GetByName (User user, string name);
        Task<ICollection<Tag>> GetAll(User user);
        Task<ICollection<Tag>> GetByRecipe(User user, Recipe recipe);
        Task<ICollection<Tag>> GetByIngredient(User user, Ingredient ingredient);
        Task<bool> Add(Tag recipe);
        Task<bool> Update(Tag recipe);
        Task<bool> Delete(Tag recipe);
    }
}

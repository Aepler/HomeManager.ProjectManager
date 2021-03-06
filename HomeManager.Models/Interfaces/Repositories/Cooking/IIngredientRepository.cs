using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Cooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Interfaces.Repositories.Cooking
{
    public interface  IIngredientRepository
    {
        Task<Ingredient> GetById(User user, int id);
        Task<ICollection<Ingredient>> GetAll(User user);
        Task<ICollection<Ingredient>> GetByTag(User user, Tag tag);
        Task<ICollection<Ingredient>> GetByRecipe(User user, Recipe recipe);

        Task<bool> Add(Ingredient ingredient);
        Task<bool> Update(Ingredient ingredient);
        Task<bool> Delete(Ingredient ingredient);
    }
}

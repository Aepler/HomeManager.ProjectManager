using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Cooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories.Interfaces.Cooking
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetById(User user, int id);
        Task<ICollection<Recipe>> GetAllPublicRecipes(User user);
        Task<ICollection<Recipe>> GetAll(User user);
        Task<ICollection<Recipe>> GetByTag(User user, Tag tag);
        Task<ICollection<Recipe>> GetByIngredient(User user, Ingredient ingredient);
        Task<ICollection<Recipe>> GetByFavorites(User user);

        Task<bool> Add(Recipe recipe);
        Task<bool> Update(Recipe recipe);
        Task<bool> Delete(Recipe recipe);
    }
}

using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetById(User user, int id);
        Task<ICollection<Recipe>> GetAllPublicRecipes();
        Task<ICollection<Recipe>> GetAll(User user);
        Task<ICollection<Recipe>> GetByTag(Tag tag);
        Task<ICollection<Recipe>> GetByIngredient(Ingredient ingredient);
        Task<ICollection<Recipe>> GetByFavorites(Favorites favorites);


        Task<bool> Add(User user, Recipe recipe);
        Task<bool> Update(User user, Recipe recipe);
        Task<bool> Delete(User user, Recipe recipe);
    }
}

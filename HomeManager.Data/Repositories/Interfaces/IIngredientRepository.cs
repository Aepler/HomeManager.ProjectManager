using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface  IIngredientRepository
    {
        Task<Ingredient> GetById(User user, int id);
        Task<ICollection<Ingredient>> GetAll(User user);
        Task<ICollection<Ingredient>> GetByTag(Tag tag);
        Task<ICollection<Ingredient>> GetByRecipe(Recipe recipe);

        Task<bool> Add(User user, Ingredient ingredient);
        Task<bool> Update(User user, Ingredient ingredient);
        Task<bool> Delete(User user, Ingredient ingredient);
    }
}

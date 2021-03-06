using HomeManager.Models.Interfaces.Repositories.Cooking;
using HomeManager.Models.Entities.Cooking;
using HomeManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories.Cooking
{
    public class IngredientRepository : IIngredientRepository
    {
        public Task<bool> Add(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Ingredient>> GetAll(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Ingredient> GetById(User user, int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Ingredient>> GetByRecipe(User user, Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Ingredient>> GetByTag(User user, Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }
    }
}

using HomeManager.Data.Repositories.Interfaces;
using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        public Task<bool> Add(User user, Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(User user, Ingredient ingredient)
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

        public Task<ICollection<Ingredient>> GetByRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Ingredient>> GetByTag(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User user, Ingredient ingredient)
        {
            throw new NotImplementedException();
        }
    }
}

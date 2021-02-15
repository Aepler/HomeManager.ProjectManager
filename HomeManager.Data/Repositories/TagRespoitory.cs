using HomeManager.Data.Repositories.Interfaces;
using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories
{
    public class TagRespoitory : ITagRepository
    {
        public Task<bool> Add(User user, Tag recipe)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(User user, Tag recipe)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Tag>> GetAll(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Tag>> GetByIngredient(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetByName(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Tag>> GetByRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User user, Tag recipe)
        {
            throw new NotImplementedException();
        }
    }
}

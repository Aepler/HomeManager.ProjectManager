using HomeManager.Data.Repositories.Interfaces;
using HomeManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly HomeManagerContext _context;

        public RecipeRepository(HomeManagerContext context)
        {
            _context = context;
        }

        
        public async Task<ICollection<Recipe>> GetAll(User user) => await _context.Recipes.Where(r => r.fk_UserId == user.Id && r.Deleted == false).ToListAsync();

        public async Task<ICollection<Recipe>> GetAllPublicRecipes() => await _context.Recipes.Where(r => r.Public && !r.Deleted).ToListAsync();

        public async Task<Recipe> GetById(User user, int id) => await _context.Recipes.Where(r => r.fk_UserId == user.Id && r.Id == id && !r.Deleted).FirstOrDefaultAsync();


        public async Task<ICollection<Recipe>> GetByFavorites(User user)
        {
            //var result = await _context.Recipes.Where(r => r.Favorites)
            throw new NotImplementedException();
        }

        public async Task<ICollection<Recipe>> GetByIngredient(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Recipe>> GetByTag(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Add(User user, Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User user, Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(User user, Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Recipe>> GetByFavorites(Favorites favorites)
        {
            throw new NotImplementedException();
        }
    }
}

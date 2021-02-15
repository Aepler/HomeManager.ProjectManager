using HomeManager.Data.Repositories.Interfaces.Cooking;
using HomeManager.Models.Entities.Cooking;
using HomeManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories.Cooking
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly HomeManagerContext _context;

        public RecipeRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Recipe>> GetAll(User user)
        {
            return await _context.CookingRecipes
                .Where(r => r.fk_UserId == user.Id && r.Deleted == false)
                .Include(r => r.User)
                .ToListAsync();
        }
        
    

        public async Task<ICollection<Recipe>> GetAllPublicRecipes(User user)
        {
            return await _context.CookingRecipes
                .Where(r => r.Public && !r.Deleted)
                .Include(r => r.User)
                .ToListAsync();
        }


        public async Task<Recipe> GetById(User user, int id)
        {
            return await _context.CookingRecipes
                .Where(r => r.fk_UserId == user.Id && r.Id == id && !r.Deleted)
                .Include(r => r.User)
                .FirstOrDefaultAsync();
        }


        public async Task<ICollection<Recipe>> GetByFavorites(User user)
        {
            return await _context.CookingFavorites
                .Where(f => f.fk_UserId == user.Id)
                .Select(f => f.Recipe).Include(r => r.User)
                .ToListAsync();
        }


        public async Task<ICollection<Recipe>> GetByIngredient(User user, Ingredient ingredient) 
        {
            return await _context.CookingRecipes
                .Where(r => r.Ingredients.Contains(ingredient))
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<ICollection<Recipe>> GetByTag(User user, Tag tag)
        {
            return await _context.CookingRecipes
                .Where(r => r.Tags.Contains(tag))
                .Include(r => r.User)
                .ToListAsync();
        } 


        public async Task<bool> Add(Recipe recipe)
        {
            try
            {
                _context.CookingRecipes.Add(recipe);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ;
            }
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Recipe recipe)
        {
            try
            {
                _context.CookingRecipes.Update(recipe);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Recipe recipe)
        {
            try
            {
                _context.CookingRecipes.Remove(recipe);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            throw new NotImplementedException();
        }
    }
}

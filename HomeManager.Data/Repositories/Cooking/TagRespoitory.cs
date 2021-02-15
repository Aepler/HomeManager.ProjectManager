using HomeManager.Data.Repositories.Interfaces.Cooking;
using HomeManager.Models.Entities.Cooking;
using HomeManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Data.Repositories.Cooking
{
    public class TagRespoitory : ITagRepository
    {
        private readonly HomeManagerContext _context;

        public TagRespoitory(HomeManagerContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Tag>> GetAll(User user)
        {
            return await _context.CookingTags
                .Where(t => t.fk_UserId == user.Id && t.Deleted == false)
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<Tag> GetById(User user, int id)
        {
            return await _context.CookingTags
                .Where(t => t.fk_UserId == user.Id && t.Deleted == false)
                .Include(t => t.User)
                .FirstOrDefaultAsync();
        }
      
        public async Task<ICollection<Tag>> GetByIngredient(User user, Ingredient ingredient)
        {
            return await _context.CookingTags
                .Where(t => t.Ingredients.Contains(ingredient))
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<Tag> GetByName(User user, string name)
        {
            return await _context.CookingTags
                .Where(t => t.Name == name)
                .Where(t => t.fk_UserId == user.Id && t.Deleted == false)
                .Include(t => t.User)
                .FirstOrDefaultAsync();
        }



        public async Task<ICollection<Tag>> GetByRecipe(User user, Recipe recipe)
        {
            /*return await _context.CookingTags
                .Where(t => t.Ingredients.Contains(ingredient))
                .Where(t => t.fk_UserId == user.Id && t.Deleted == false)
                .Include(t => t.User)
                .ToListAsync();*/
            throw new NotImplementedException();
        }


        public async Task<bool> Add(Tag recipe)
        {
            try
            {
                _context.CookingTags.Add(recipe);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Tag recipe)
        {
            try
            {
                _context.CookingTags.Update(recipe);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Tag recipe)
        {
            try
            {
                _context.CookingTags.Remove(recipe);
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

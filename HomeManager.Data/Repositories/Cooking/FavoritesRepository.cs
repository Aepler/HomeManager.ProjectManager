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
    public class FavoritesRepository : IFavoritesRepository
    {
        public Task<bool> Add(Favorites favorites)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Favorites favorites)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Favorites>> GetAll(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Favorites> GetById(User user, int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Favorites favorites)
        {
            throw new NotImplementedException();
        }
    }
}

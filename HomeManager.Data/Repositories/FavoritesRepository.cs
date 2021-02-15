using HomeManager.Data.Repositories.Interfaces;
using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories
{
    public class FavoritesRepository : IFavoritesRepository
    {
        public Task<bool> Add(User user, Favorites favorites)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(User user, Favorites favorites)
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

        public Task<bool> Update(User user, Favorites favorites)
        {
            throw new NotImplementedException();
        }
    }
}

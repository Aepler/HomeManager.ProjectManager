using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface IFavoritesRepository
    {
        Task<Favorites> GetById(User user, int id);
        Task<ICollection<Favorites>> GetAll(User user);

        Task<bool> Add(User user, Favorites favorites);
        Task<bool> Update(User user, Favorites favorites);
        Task<bool> Delete(User user, Favorites favorites);
    }
}

using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Cooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Interfaces.Repositories.Cooking
{
    public interface IFavoritesRepository
    {
        Task<Favorites> GetById(User user, int id);
        Task<ICollection<Favorites>> GetAll(User user);

        Task<bool> Add(Favorites favorites);
        Task<bool> Update(Favorites favorites);
        Task<bool> Delete(Favorites favorites);
    }
}

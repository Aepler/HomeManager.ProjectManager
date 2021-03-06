using HomeManager.Models.Interfaces.Services.Finance;
using HomeManager.Models.Interfaces.Repositories.Finance;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Services.Finance
{
    public class RepeatingService : IRepeatingService
    {
        private readonly IRepeatingRepository _repeatingRepository;

        public RepeatingService(IRepeatingRepository repeatingRepository)
        {
            _repeatingRepository = repeatingRepository;
        }

        public async Task<Repeating> GetById(User user, Guid id)
        {
            try
            {
                var repeating = _repeatingRepository.GetById(id);
                if (repeating.fk_UserId == user.Id && !repeating.Deleted)
                {
                    return repeating;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Repeating>> GetAll(User user)
        {
            try
            {
                return _repeatingRepository.GetAll().Where(x => x.fk_UserId == user.Id && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Repeating>> GetByType(User user, Guid typeId)
        {
            try
            {
                var repeatings = await GetAll(user);
                return repeatings.Where(x => x.fk_TypeId == typeId).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Repeating>> GetByCategory(User user, Guid categoryId)
        {
            try
            {
                var repeatings = await GetAll(user);
                return repeatings.Where(x => x.fk_CategoryId == categoryId).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Add(User user, Repeating repeating)
        {
            try
            {
                repeating.fk_UserId = user.Id;
                return _repeatingRepository.Add(repeating);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(User user, Repeating repeating)
        {
            try
            {
                var realRepeating = await GetById(user, repeating.Id);
                if (realRepeating != null)
                {
                    repeating.fk_UserId = user.Id;
                    return _repeatingRepository.Update(repeating);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(User user, Repeating repeating)
        {
            try
            {
                var realRepeating = await GetById(user, repeating.Id);
                if (realRepeating != null)
                {
                    repeating.fk_UserId = user.Id;
                    repeating.Deleted = true;
                    repeating.DeletedOn = DateTime.Today;
                    return _repeatingRepository.Delete(repeating);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

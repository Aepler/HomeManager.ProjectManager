using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface IStatusRepository
    {
        Task<Status> GetById(User user, int id);
        Task<ICollection<Status>> GetAll(User user);
        Task<ICollection<Status>> GetByUser(User user);
        Task<ICollection<Status>> GetByEndPoint(User user, bool endPoint);
        Task<ICollection<Status>> GetPossibleStatus(User user, int id);
        Task<bool> Add(User user, Status status);
        Task<bool> Update(User user, Status status);
        Task<bool> Delete(User user, Status status);
    }
}

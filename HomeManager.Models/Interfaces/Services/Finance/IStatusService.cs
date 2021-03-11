using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.Models.Interfaces.Services.Finance
{
    public interface IStatusService
    {
        Task<Status> GetById(User user, Guid id);
        Task<ICollection<Status>> GetAll(User user);
        Task<ICollection<Status>> GetByUser(User user);
        Task<ICollection<Status>> GetByEndPoint(User user, bool endPoint);
        Task<ICollection<Status>> GetByTypeId(User user, Type type);
        Task<ICollection<Status>> GetDefault();
        Task<bool> Add(User user, Status status);
        Task<bool> Update(User user, Status status);
        Task<bool> Delete(User user, Status status);
        Task<bool> AddDefault(IList<string> userRoles, Status status);
        Task<bool> UpdateDefault(IList<string> userRoles, Status status);
        Task<bool> DeleteDefault(IList<string> userRoles, Status status);
    }
}

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
        Status GetById(User user, Guid id);
        IEnumerable<Status> GetAll(User user);
        IEnumerable<Status> GetByUser(User user);
        IEnumerable<Status> GetByEndPoint(User user, bool endPoint);
        IEnumerable<Status> GetByTypeId(User user, Type type);
        IEnumerable<Status> GetDefault();
        bool Add(User user, Status status);
        bool Update(User user, Status status);
        bool Delete(User user, Status status);
        bool AddDefault(IList<string> userRoles, Status status);
        bool UpdateDefault(IList<string> userRoles, Status status);
        bool DeleteDefault(IList<string> userRoles, Status status);
    }
}

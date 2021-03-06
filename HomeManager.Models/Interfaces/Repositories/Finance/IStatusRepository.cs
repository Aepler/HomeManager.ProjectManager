using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Repositories.Finance
{
    public interface IStatusRepository
    {
        Status GetById(Guid id);
        ICollection<Status> GetAll();
        bool Add(Status status);
        bool Update(Status status);
        bool Delete(Status status);
    }
}

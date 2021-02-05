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
        Status GetById(int id);
        ICollection<Status> GetAll();
        bool Add(Status status);
        bool Update(Status status);
    }
}

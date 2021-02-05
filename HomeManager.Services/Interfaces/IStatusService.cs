using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Services.Interfaces
{
    public interface IStatusService
    {
        Status GetById(int id);
        ICollection<Status> GetAll();
        bool Add(Status status);
        bool Update(Status status);
    }
}

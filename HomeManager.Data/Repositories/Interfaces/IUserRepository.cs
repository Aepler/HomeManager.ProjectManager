using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAll();
    }
}

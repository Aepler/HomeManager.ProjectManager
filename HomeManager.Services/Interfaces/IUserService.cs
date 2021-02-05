using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Services.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<User>> GetAll();
    }
}

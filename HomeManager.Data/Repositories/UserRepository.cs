using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Data.Repositories.Interfaces;
using HomeManager.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HomeManagerContext _context;

        public UserRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAll()
        {
            ICollection<User> users = await _context.Users.ToListAsync();
            return users;
        }
    }
}

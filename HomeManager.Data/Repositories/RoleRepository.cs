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
    public class RoleRepository : IRoleRepository
    {
        private readonly HomeManagerContext _context;

        public RoleRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Role>> GetAll()
        {
            ICollection<Role> roles = await _context.Roles.ToListAsync();
            return roles;
        }
    }
}

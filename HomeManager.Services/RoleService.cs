using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Data.Repositories.Interfaces;
using HomeManager.Models;
using HomeManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ICollection<Role>> GetAll()
        {
            try
            {
                return await _roleRepository.GetAll();
            }
            catch (Exception ex)
            {
                return new List<Role>();
            }
        }
    }
}

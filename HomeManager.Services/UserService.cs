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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICollection<User>> GetAll()
        {
            try
            {
                return await _userRepository.GetAll();
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }
    }
}

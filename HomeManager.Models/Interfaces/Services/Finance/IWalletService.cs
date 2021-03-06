using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models.Interfaces.Services.Finance
{
    public interface IWalletService
    {
        Task<Wallet> GetById(User user, Guid id);
        Task<ICollection<Wallet>> GetAll(User user);
        Task<bool> Add(User user, Wallet wallet);
        Task<bool> Update(User user, Wallet wallet);
        Task<bool> Delete(User user, Wallet wallet);
    }
}

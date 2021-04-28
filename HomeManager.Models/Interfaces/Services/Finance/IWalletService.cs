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
        Wallet GetById(User user, Guid id);
        IEnumerable<Wallet> GetAll(User user);
        bool Add(User user, Wallet wallet);
        bool Update(User user, Wallet wallet);
        bool Delete(User user, Wallet wallet);
    }
}

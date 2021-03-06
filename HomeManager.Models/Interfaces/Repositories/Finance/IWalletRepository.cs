using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Repositories.Finance
{
    public interface IWalletRepository
    {
        Wallet GetById(Guid id);
        ICollection<Wallet> GetAll();
        bool Add(Wallet wallet);
        bool Update(Wallet wallet);
        bool Delete(Wallet wallet);
    }
}

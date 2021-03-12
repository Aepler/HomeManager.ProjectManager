using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Repositories.Finance
{
    public interface IWalletRepository
    {
        Wallet GetById(Guid id);
        IQueryable<Wallet> GetAll();
        bool Add(Wallet wallet);
        bool Update(Wallet wallet);
        bool Delete(Wallet wallet);
    }
}

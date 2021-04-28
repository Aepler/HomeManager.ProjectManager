using HomeManager.Models.Interfaces.Services.Finance;
using HomeManager.Models.Interfaces.Repositories.Finance;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Services.Finance
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public Wallet GetById(User user, Guid id)
        {
            try
            {
                var wallet = _walletRepository.GetById(id);
                if (wallet.fk_UserId == user.Id && !wallet.Deleted)
                {
                    return wallet;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Wallet> GetAll(User user)
        {
            try
            {
                return _walletRepository.GetAll().Where(x => x.fk_UserId == user.Id && !x.Deleted);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Add(User user, Wallet wallet)
        {
            try
            {
                wallet.fk_UserId = user.Id;
                return _walletRepository.Add(wallet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(User user, Wallet wallet)
        {
            try
            {
                var realWallet = GetById(user, wallet.Id);
                if (realWallet != null)
                {
                    wallet.fk_UserId = user.Id;
                    return _walletRepository.Update(wallet);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(User user, Wallet wallet)
        {
            try
            {
                var realWallet = GetById(user, wallet.Id);
                if (realWallet != null)
                {
                    wallet.fk_UserId = user.Id;
                    wallet.Deleted = true;
                    wallet.DeletedOn = DateTime.Today;
                    return _walletRepository.Delete(wallet);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

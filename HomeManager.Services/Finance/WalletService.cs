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

        public async Task<Wallet> GetById(User user, Guid id)
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

        public async Task<ICollection<Wallet>> GetAll(User user)
        {
            try
            {
                return _walletRepository.GetAll().Where(x => x.fk_UserId == user.Id && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Add(User user, Wallet wallet)
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

        public async Task<bool> Update(User user, Wallet wallet)
        {
            try
            {
                var realWallet = await GetById(user, wallet.Id);
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

        public async Task<bool> Delete(User user, Wallet wallet)
        {
            try
            {
                var realWallet = await GetById(user, wallet.Id);
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

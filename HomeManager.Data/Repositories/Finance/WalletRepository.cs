﻿using HomeManager.Models.Interfaces.Repositories.Finance;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Repositories.Finance
{
    public class WalletRepository : IWalletRepository
    {
        private readonly HomeManagerDbContext _context;

        public WalletRepository(HomeManagerDbContext context)
        {
            _context = context;
        }

        public Wallet GetById(Guid id)
        {
            return _context.FinanceWallets.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<Wallet> GetAll()
        {
            return _context.FinanceWallets;
        }
        public bool Add(Wallet wallet)
        {
            try
            {
                _context.FinanceWallets.Add(wallet);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(Wallet wallet)
        {
            try
            {
                _context.FinanceWallets.Update(wallet);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(Wallet wallet)
        {
            try
            {
                _context.FinanceWallets.Update(wallet);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Repositories.Finance;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Data.Repositories.Finance
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HomeManagerContext _context;

        public PaymentRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public Payment GetById(Guid id)
        {
            return _context.FinancePayments.Where(x => x.Id == id).Include(x => x.Wallet).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).FirstOrDefault();
        }

        public IQueryable<Payment> GetAll()
        {
            return _context.FinancePayments.Include(x => x.Wallet).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status);
        }

        public bool Add(Payment payment)
        {
            try
            {
                _context.FinancePayments.Add(payment);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(Payment payment)
        {
            try
            {
                _context.FinancePayments.Update(payment);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(Payment payment)
        {
            try
            {
                _context.FinancePayments.Update(payment);
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

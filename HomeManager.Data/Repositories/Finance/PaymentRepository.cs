using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Data.Repositories.Interfaces.Finance;
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

        public async Task<Payment> GetById(User user, int id)
        {
            return await _context.FinancePayments.Where(x => x.fk_UserId == user.Id && x.Id == id && !x.Deleted).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Payment>> GetAll(User user)
        {
            return await _context.FinancePayments.Where(x => x.fk_UserId == user.Id && !x.Deleted).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
        }

        public async Task<ICollection<Payment>> GetByCategory(User user, int fk_CategoryId)
        {
            return await _context.FinancePayments.Where(x => x.fk_UserId == user.Id && x.fk_CategoryId == fk_CategoryId && !x.Deleted).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
        }

        public async Task<ICollection<Payment>> GetByDate(User user, DateTime dateTime)
        {
            return await _context.FinancePayments.Where(x => x.fk_UserId == user.Id && x.Date == dateTime && !x.Deleted).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
        }

        public async Task<ICollection<Payment>> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            return await _context.FinancePayments.Where(x => x.fk_UserId == user.Id && (x.Date >= dateTimeStart || x.Date <= dateTimeEnd) && !x.Deleted).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
        }

        public async Task<ICollection<Payment>> GetByStatus(User user, int fk_StatusId)
        {
            return await _context.FinancePayments.Where(x => x.fk_UserId == user.Id && x.fk_StatusId == fk_StatusId && !x.Deleted).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
        }

        public async Task<ICollection<Payment>> GetByType(User user, int fk_TypeId)
        {
            return await _context.FinancePayments.Where(x => x.fk_UserId == user.Id && x.fk_TypeId == fk_TypeId && !x.Deleted).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
        }



        public async Task<bool> Add(Payment payment)
        {
            try
            {
                _context.FinancePayments.Add(payment);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(Payment payment)
        {
            try
            {
                _context.FinancePayments.Update(payment);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(Payment payment)
        {
            try
            {
                _context.FinancePayments.Update(payment);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

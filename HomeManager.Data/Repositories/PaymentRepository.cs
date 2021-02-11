using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HomeManagerContext _context;

        public PaymentRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public async Task<Payments> GetById(User user, int id)
        {
            Payments payment = await _context.Payments.Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).Include(x => x.Payment_Template).Where(x => x.fk_UserId == user.Id && x.Id == id && x.Deleted == false).FirstOrDefaultAsync();
            return payment;
        }

        public async Task<ICollection<Payments>> GetAll(User user)
        {
            ICollection<Payments> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && x.Deleted == false).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).Include(x => x.Payment_Template).ToListAsync();
            return payments;
        }

        public async Task<ICollection<Payments>> GetByCategory(User user, int fk_CategoryId)
        {
            ICollection<Payments> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && x.fk_CategoryId == fk_CategoryId && x.Deleted == false).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).Include(x => x.Payment_Template).ToListAsync();
            return payments;
        }

        public async Task<ICollection<Payments>> GetByDate(User user, DateTime dateTime)
        {
            ICollection<Payments> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && x.Date == dateTime && x.Deleted == false).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).Include(x => x.Payment_Template).ToListAsync();
            return payments;
        }

        public async Task<ICollection<Payments>> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            ICollection<Payments> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && (x.Date >= dateTimeStart || x.Date <= dateTimeEnd) && x.Deleted == false).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).Include(x => x.Payment_Template).ToListAsync();
            return payments;
        }

        public async Task<ICollection<Payments>> GetByStatus(User user, int fk_StatusId)
        {
            ICollection<Payments> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && x.fk_StatusId == fk_StatusId && x.Deleted == false).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).Include(x => x.Payment_Template).ToListAsync();
            return payments;
        }

        public async Task<ICollection<Payments>> GetByType(User user, int fk_TypeId)
        {
            ICollection<Payments> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && x.fk_TypeId == fk_TypeId && x.Deleted == false).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).Include(x => x.Payment_Template).ToListAsync();
            return payments;
        }



        public async Task<bool> Add(User user, Payments payment)
        {
            try
            {
                payment.fk_UserId = user.Id;
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(User user, Payments payment)
        {
            try
            {
                var realPayment = await _context.Payments.AsNoTracking().FirstAsync(x => x.Id == payment.Id);
                if (realPayment != null && realPayment.fk_UserId == user.Id)
                {
                    payment.fk_UserId = user.Id;
                    _context.Payments.Update(payment);
                    await _context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(User user, Payments payment)
        {
            try
            {
                var realPayment = await _context.Payments.FindAsync(payment.Id);
                if (realPayment != null && realPayment.fk_UserId == user.Id)
                {
                    payment.Deleted = true;
                    payment.DeletedOn = DateTime.Today;
                    _context.Payments.Update(payment);
                    await _context.SaveChangesAsync();

                    return true;
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

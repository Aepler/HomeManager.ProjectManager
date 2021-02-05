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

        public async Task<Payment> GetById(User user, int id)
        {
            Payment payment = await _context.Payments.Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).Where(x => x.fk_UserId == user.Id && x.Id == id).FirstOrDefaultAsync();
            return payment;
        }

        public async Task<ICollection<Payment>> GetAll(User user)
        {
            ICollection<Payment> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
            return payments;
        }

        public async Task<ICollection<Payment>> GetByCategory(User user, int fk_CategoryId)
        {
            ICollection<Payment> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && x.fk_CategoryId == fk_CategoryId).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
            return payments;
        }

        public async Task<ICollection<Payment>> GetByDate(User user, DateTime dateTime)
        {
            ICollection<Payment> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && x.Date == dateTime).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
            return payments;
        }

        public async Task<ICollection<Payment>> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            ICollection<Payment> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && x.Date >= dateTimeStart && x.Date <= dateTimeEnd).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
            return payments;
        }

        public async Task<ICollection<Payment>> GetByStatus(User user, int fk_StatusId)
        {
            ICollection<Payment> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && x.fk_StatusId == fk_StatusId).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
            return payments;
        }

        public async Task<ICollection<Payment>> GetByType(User user, int fk_TypeId)
        {
            ICollection<Payment> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && x.fk_TypeId == fk_TypeId).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
            return payments;
        }

        public async Task<ICollection<Payment>> GetByUser(User user, string searchUser)
        {
            ICollection<Payment> payments = await _context.Payments.Where(x => x.fk_UserId == user.Id && x.fk_UserId == Guid.Parse(searchUser)).Include(x => x.Category).Include(x => x.Type).Include(x => x.Status).Include(x => x.User).ToListAsync();
            return payments;
        }



        public async Task<bool> Add(User user, Payment payment)
        {
            try
            {
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(User user, Payment payment)
        {
            try
            {
                _context.Payments.Update(payment);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

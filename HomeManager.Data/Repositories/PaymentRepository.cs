using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Data.Repositories.Interfaces;

namespace HomeManager.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HomeManagerContext _context;

        public PaymentRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public Payment GetById(int id)
        {
            Payment payment = _context.Payments.Find(id);
            return payment;
        }

        public ICollection<Payment> GetAll()
        {
            ICollection<Payment> payments = _context.Payments.ToList();
            return payments;
        }

        public ICollection<Payment> GetByCategory(int fk_CategoryId)
        {
            ICollection<Payment> payments = _context.Payments.Where(x => x.fk_CategoryId == fk_CategoryId).ToList();
            return payments;
        }

        public ICollection<Payment> GetByDate(DateTime dateTime)
        {
            ICollection<Payment> payments = _context.Payments.Where(x => x.Date == dateTime).ToList();
            return payments;
        }

        public ICollection<Payment> GetByDateRange(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            ICollection<Payment> payments = _context.Payments.Where(x => x.Date >= dateTimeStart && x.Date <= dateTimeEnd).ToList();
            return payments;
        }

        public ICollection<Payment> GetByStatus(int fk_StatusId)
        {
            ICollection<Payment> payments = _context.Payments.Where(x => x.fk_StatusId == fk_StatusId).ToList();
            return payments;
        }

        public ICollection<Payment> GetByType(int fk_TypeId)
        {
            ICollection<Payment> payments = _context.Payments.Where(x => x.fk_TypeId == fk_TypeId).ToList();
            return payments;
        }

        public ICollection<Payment> GetByUser(string user)
        {
            ICollection<Payment> payments = _context.Payments.Where(x => x.fk_UserId == Guid.Parse(user)).ToList();
            return payments;
        }



        public bool Add(Payment payment)
        {
            try
            {
                _context.Payments.Add(payment);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Payment payment)
        {
            try
            {
                _context.Payments.Update(payment);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

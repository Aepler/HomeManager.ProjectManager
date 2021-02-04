using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HomeManagerContext _context;

        public PaymentRepository(HomeManagerContext context)
        {
            _context = context;
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

        public ICollection<Payment> GetAll()
        {
            ICollection<Payment> payments = _context.Payments.ToList();
            return payments;
        }

        public ICollection<Payment> GetByCategory(int fk_CategoryId)
        {
            ICollection<Payment> payments = GetAll();
            return payments.Where(x => x.fk_CategoryId == fk_CategoryId).ToList();
        }

        public ICollection<Payment> GetByDate(DateTime dateTime)
        {
            ICollection<Payment> payments = GetAll();
            return payments.Where(x => x.Date == dateTime).ToList();
        }

        public ICollection<Payment> GetByDateRange(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            ICollection<Payment> payments = GetAll();
            return payments.Where(x => x.Date >= dateTimeStart && x.Date <= dateTimeEnd).ToList();
        }

        public Payment GetById(int id)
        {
            ICollection<Payment> payments = GetAll();
            return payments.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<Payment> GetByStatus(int fk_StatusId)
        {
            ICollection<Payment> payments = GetAll();
            return payments.Where(x => x.fk_StatusId == fk_StatusId).ToList();
        }

        public ICollection<Payment> GetByType(int fk_TypeId)
        {
            ICollection<Payment> payments = GetAll();
            return payments.Where(x => x.fk_TypeId == fk_TypeId).ToList();
        }

        public ICollection<Payment> GetByUser(string user)
        {
            ICollection<Payment> payments = GetAll();
            return payments.Where(x => x.User == user).ToList();
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

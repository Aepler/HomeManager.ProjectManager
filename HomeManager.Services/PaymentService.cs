using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Services.Interfaces;
using HomeManager.Data.Repositories.Interfaces;

namespace HomeManager.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public bool Add(Payment payment)
        {
            try
            {
                return _paymentRepository.Add(payment);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ICollection<Payment> GetAll()
        {
            try
            {
                return _paymentRepository.GetAll();
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public ICollection<Payment> GetAllPending()
        {
            try
            {
                ICollection<Payment> payments = GetAll().Where(x => x.fk_StatusId == 3 && x.fk_StatusId == 4 && x.Date > DateTime.Today).ToList();
                return payments;
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public ICollection<Payment> GetBalanceForDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public ICollection<Payment> GetBalanceToday()
        {
            try
            {
                decimal result = 0;

                ICollection<Payment> payments = GetAll();

                foreach (var x in payments)
                {
                    if (x.fk_TypeId == 1 || x.fk_TypeId == 4)
                    {
                        result += x.Amount;
                    }
                    else if(x.fk_TypeId == 2 || x.fk_TypeId == 3)
                    {
                        result -= x.Amount;
                    }
                }

                return payments;
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public ICollection<Payment> GetByCategory(int fk_CategoryId)
        {
            try
            {
                return _paymentRepository.GetByCategory(fk_CategoryId);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public ICollection<Payment> GetByDate(DateTime dateTime)
        {
            try
            {
                return _paymentRepository.GetByDate(dateTime);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public ICollection<Payment> GetByDateRange(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            try
            {
                return _paymentRepository.GetByDateRange(dateTimeStart, dateTimeEnd);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public Payment GetById(int id)
        {
            try
            {
                return _paymentRepository.GetById(id);
            }
            catch (Exception ex)
            {
                return new Payment();
            }
        }

        public ICollection<Payment> GetByStatus(int fk_StatusId)
        {
            try
            {
                return _paymentRepository.GetByStatus(fk_StatusId);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public ICollection<Payment> GetByType(int fk_TypeId)
        {
            try
            {
                return _paymentRepository.GetByType(fk_TypeId);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public ICollection<Payment> GetByUser(string user)
        {
            try
            {
                return _paymentRepository.GetByUser(user);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public ICollection<Payment> GetRealCompleted()
        {
            try
            {
                ICollection<Payment> payments = GetAll().Where(x => x.fk_StatusId == 1 && x.fk_StatusId == 2).ToList();
                return payments;
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public ICollection<Payment> GetRealPending()
        {
            try
            {
                ICollection<Payment> payments = GetAll().Where(x => x.fk_StatusId == 3 && x.Date > DateTime.Today).ToList();
                return payments;
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public bool Update(Payment payment)
        {
            try
            {
                return _paymentRepository.Update(payment);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

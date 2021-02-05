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

        public async Task<bool> Add(User user, Payment payment)
        {
            try
            {
                return await _paymentRepository.Add(user, payment);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ICollection<Payment>> GetAll(User user)
        {
            try
            {
                return await _paymentRepository.GetAll(user);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<ICollection<Payment>> GetAllPending(User user)
        {
            try
            {
                ICollection<Payment> payments = await _paymentRepository.GetAll(user);
                return payments;
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<ICollection<Payment>> GetBalanceForDate(User user, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Payment>> GetBalanceToday(User user)
        {
            try
            {
                decimal result = 0;

                ICollection<Payment> payments = await GetAll(user);

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

        public async Task<ICollection<Payment>> GetByCategory(User user, int fk_CategoryId)
        {
            try
            {
                return await _paymentRepository.GetByCategory(user, fk_CategoryId);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<ICollection<Payment>> GetByDate(User user, DateTime dateTime)
        {
            try
            {
                return await _paymentRepository.GetByDate(user, dateTime);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<ICollection<Payment>> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            try
            {
                return await _paymentRepository.GetByDateRange(user, dateTimeStart, dateTimeEnd);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<Payment> GetById(User user, int id)
        {
            try
            {
                return await _paymentRepository.GetById(user, id);
            }
            catch (Exception ex)
            {
                return new Payment();
            }
        }

        public async Task<ICollection<Payment>> GetByStatus(User user, int fk_StatusId)
        {
            try
            {
                return await _paymentRepository.GetByStatus(user, fk_StatusId);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<ICollection<Payment>> GetByType(User user, int fk_TypeId)
        {
            try
            {
                return await _paymentRepository.GetByType(user, fk_TypeId);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<ICollection<Payment>> GetByUser(User user, string searchUser)
        {
            try
            {
                return await _paymentRepository.GetByUser(user, searchUser);
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<ICollection<Payment>> GetRealCompleted(User user)
        {
            try
            {
                ICollection<Payment> payments = await _paymentRepository.GetAll(user);
                return payments.Where(x => x.fk_StatusId == 1 && x.fk_StatusId == 2).ToList();
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<ICollection<Payment>> GetRealPending(User user)
        {
            try
            {
                ICollection<Payment> payments = await _paymentRepository.GetAll(user);
                return payments.Where(x => x.fk_StatusId == 3 && x.Date > DateTime.Today).ToList();
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<bool> Update(User user, Payment payment)
        {
            try
            {
                return await _paymentRepository.Update(user, payment);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

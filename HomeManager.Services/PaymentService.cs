using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Models.Interfaces;
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

        public async Task<Payments> GetById(User user, int id)
        {
            try
            {
                return await _paymentRepository.GetById(user, id);
            }
            catch (Exception ex)
            {
                return new Payments();
            }
        }

        public async Task<ICollection<Payments>> GetAll(User user)
        {
            try
            {
                return await _paymentRepository.GetAll(user);
            }
            catch (Exception ex)
            {
                return new List<Payments>();
            }
        }

        public async Task<ICollection<Payments>> GetBalanceForDate(User user, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Payments>> GetBalanceToday(User user)
        {
            try
            {
                decimal result = 0;

                ICollection<Payments> payments = await GetAll(user);

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
                return new List<Payments>();
            }
        }

        public async Task<ICollection<Payments>> GetByCategory(User user, int fk_CategoryId)
        {
            try
            {
                return await _paymentRepository.GetByCategory(user, fk_CategoryId);
            }
            catch (Exception ex)
            {
                return new List<Payments>();
            }
        }

        public async Task<ICollection<Payments>> GetByDate(User user, DateTime dateTime)
        {
            try
            {
                return await _paymentRepository.GetByDate(user, dateTime);
            }
            catch (Exception ex)
            {
                return new List<Payments>();
            }
        }

        public async Task<ICollection<Payments>> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            try
            {
                return await _paymentRepository.GetByDateRange(user, dateTimeStart, dateTimeEnd);
            }
            catch (Exception ex)
            {
                return new List<Payments>();
            }
        }

        public async Task<ICollection<Payments>> GetByStatus(User user, int fk_StatusId)
        {
            try
            {
                return await _paymentRepository.GetByStatus(user, fk_StatusId);
            }
            catch (Exception ex)
            {
                return new List<Payments>();
            }
        }

        public async Task<ICollection<Payments>> GetByType(User user, int fk_TypeId)
        {
            try
            {
                return await _paymentRepository.GetByType(user, fk_TypeId);
            }
            catch (Exception ex)
            {
                return new List<Payments>();
            }
        }

        public async Task<ICollection<Payments>> GetCompleted(User user)
        {
            try
            {
                ICollection<Payments> payments = await _paymentRepository.GetAll(user);
                return payments.Where(x => x.Status.EndPoint == true && x.Date <= DateTime.Today).ToList();
            }
            catch (Exception ex)
            {
                return new List<Payments>();
            }
        }

        public async Task<ICollection<Payments>> GetPending(User user)
        {
            try
            {
                ICollection<Payments> payments = await _paymentRepository.GetAll(user);
                return payments.Where(x => x.Status.EndPoint == false && x.Date > DateTime.Today).ToList();
            }
            catch (Exception ex)
            {
                return new List<Payments>();
            }
        }

        public async Task<bool> Add(User user, Payments payment)
        {
            try
            {
                return await _paymentRepository.Add(user, payment);
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
                return await _paymentRepository.Update(user, payment);
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
                return await _paymentRepository.Delete(user, payment);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

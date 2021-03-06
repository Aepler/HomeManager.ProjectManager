using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Services.Finance;
using HomeManager.Models.Interfaces.Repositories.Finance;
using HomeManager.Models.Enums;

namespace HomeManager.Services.Finance
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> GetById(User user, Guid id)
        {
            try
            {
                var payment = _paymentRepository.GetById(id);
                if (payment.fk_UserId == user.Id && !payment.Deleted)
                {
                    return payment;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Payment>> GetAll(User user)
        {
            try
            {
                return _paymentRepository.GetAll().Where(x => x.fk_UserId == user.Id && !x.Deleted).ToList();
            }
            catch (Exception ex)
            {
                throw;
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
                    if (x.Type.TransactionType == PaymentTransactionType.Deposit || x.Type.TransactionType == PaymentTransactionType.Both)
                    {
                        result += x.Amount;
                    }
                    else if(x.Type.TransactionType == PaymentTransactionType.Debit)
                    {
                        result -= x.Amount;
                    }
                }

                return payments;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Payment>> GetByWallet(User user, Guid walletId)
        {
            try
            {
                var payments = await GetAll(user);
                return payments.Where(x => x.fk_WalletId == walletId).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Payment>> GetByCurrentWallet(User user)
        {
            try
            {
                var payments = await GetAll(user);
                return payments.Where(x => x.fk_WalletId == user.CurrentWallet).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Payment>> GetByCategory(User user, Guid categoryId)
        {
            try
            {
                var payments = await GetAll(user);
                return payments.Where(x => x.fk_CategoryId == categoryId).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Payment>> GetByDate(User user, DateTime dateTime)
        {
            try
            {
                var payments = await GetAll(user);
                return payments.Where(x => x.Date == dateTime).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Payment>> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            try
            {
                var payments = await GetAll(user);
                return payments.Where(x => x.Date >= dateTimeStart || x.Date <= dateTimeEnd).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Payment>> GetByStatus(User user, Guid statusId)
        {
            try
            {
                var payments = await GetAll(user);
                return payments.Where(x => x.fk_StatusId == statusId).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Payment>> GetByType(User user, Guid typeId)
        {
            try
            {
                var payments = await GetAll(user);
                return payments.Where(x => x.fk_TypeId == typeId).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Payment>> GetCompleted(User user)
        {
            try
            {
                ICollection<Payment> payments = await GetAll(user);
                return payments.Where(x => x.Status.EndPoint == true && x.Date <= DateTime.Today).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Payment>> GetPending(User user)
        {
            try
            {
                ICollection<Payment> payments = await GetAll(user);
                return payments.Where(x => x.Status.EndPoint == false && x.Date > DateTime.Today).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Add(User user, Payment payment)
        {
            try
            {
                payment.fk_UserId = user.Id;
                return _paymentRepository.Add(payment);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(User user, Payment payment)
        {
            try
            {
                var realPayment = await GetById(user, payment.Id);
                if (realPayment != null)
                {
                    payment.fk_UserId = user.Id;
                    return _paymentRepository.Update(payment);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(User user, Payment payment)
        {
            try
            {
                var realPayment = await GetById(user, payment.Id);
                if (realPayment != null)
                {
                    payment.fk_UserId = user.Id;
                    payment.Deleted = true;
                    payment.DeletedOn = DateTime.Today;
                    return _paymentRepository.Delete(payment);
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

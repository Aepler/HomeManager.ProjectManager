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

        public Payment GetById(User user, Guid id)
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

        public IEnumerable<Payment> GetAll(User user)
        {
            try
            {
                return _paymentRepository.GetAll().Where(x => x.fk_UserId == user.Id && !x.Deleted);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetBalanceToday(User user)
        {
            try
            {
                decimal result = 0;

                IEnumerable<Payment> payments = GetCompleted(user);

                foreach (var payment in payments)
                {
                    if (payment.Type.TransactionType == PaymentTransactionType.Deposit || payment.Type.TransactionType == PaymentTransactionType.Both)
                    {
                        result += payment.Amount;
                    }
                    else if (payment.Type.TransactionType == PaymentTransactionType.Debit)
                    {
                        result -= payment.Amount;
                    }
                }

                return payments;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetTotalBalanceToday(User user)
        {
            try
            {
                decimal result = 0;

                IEnumerable<Payment> payments = GetAllCompleted(user);

                foreach (var payment in payments)
                {
                    if (payment.Type.TransactionType == PaymentTransactionType.Deposit || payment.Type.TransactionType == PaymentTransactionType.Both)
                    {
                        result += payment.Amount;
                    }
                    else if (payment.Type.TransactionType == PaymentTransactionType.Debit)
                    {
                        result -= payment.Amount;
                    }
                }

                return payments;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetByWallet(User user, Guid walletId)
        {
            try
            {
                return GetAll(user).Where(x => x.fk_WalletId == walletId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetByCurrentWallet(User user)
        {
            try
            {
                return GetAll(user).Where(x => x.fk_WalletId == user.CurrentWallet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetByCategory(User user, Guid categoryId)
        {
            try
            {
                return GetAll(user).Where(x => x.fk_CategoryId == categoryId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetByDate(User user, DateTime dateTime)
        {
            try
            {
                return GetAll(user).Where(x => x.Date == dateTime);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            try
            {
                return GetAll(user).Where(x => x.Date >= dateTimeStart || x.Date <= dateTimeEnd);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetByStatus(User user, Guid statusId)
        {
            try
            {
                return GetAll(user).Where(x => x.fk_StatusId == statusId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetByType(User user, Guid typeId)
        {
            try
            {
                return GetAll(user).Where(x => x.fk_TypeId == typeId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetCompleted(User user)
        {
            try
            {
                return GetByCurrentWallet(user).Where(x => x.Status.EndPoint == true && x.Date <= DateTime.Today);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetAllCompleted(User user)
        {
            try
            {
                return GetAll(user).Where(x => x.Status.EndPoint == true && x.Date <= DateTime.Today);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Payment> GetPending(User user)
        {
            try
            {
                return GetAll(user).Where(x => x.Status.EndPoint == false && x.Date > DateTime.Today);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Add(User user, Payment payment)
        {
            try
            {
                if (user.CurrentWallet != null)
                {
                    payment.fk_UserId = user.Id;
                    payment.fk_WalletId = (Guid)user.CurrentWallet;
                    return _paymentRepository.Add(payment);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(User user, Payment payment)
        {
            try
            {
                var realPayment = GetById(user, payment.Id);
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

        public bool Delete(User user, Payment payment)
        {
            try
            {
                var realPayment = GetById(user, payment.Id);
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

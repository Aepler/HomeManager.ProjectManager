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

        public ICollection<Payment> GetAll(User user)
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

        public ICollection<Payment> GetBalanceToday(User user)
        {
            try
            {
                decimal result = 0;

                ICollection<Payment> payments = GetCompleted(user);

                foreach (var x in payments)
                {
                    if (x.Type.TransactionType == PaymentTransactionType.Deposit || x.Type.TransactionType == PaymentTransactionType.Both)
                    {
                        result += x.Amount;
                    }
                    else if (x.Type.TransactionType == PaymentTransactionType.Debit)
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

        public ICollection<Payment> GetTotalBalanceToday(User user)
        {
            try
            {
                decimal result = 0;

                ICollection<Payment> payments = GetAllCompleted(user);

                foreach (var x in payments)
                {
                    if (x.Type.TransactionType == PaymentTransactionType.Deposit || x.Type.TransactionType == PaymentTransactionType.Both)
                    {
                        result += x.Amount;
                    }
                    else if (x.Type.TransactionType == PaymentTransactionType.Debit)
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

        public ICollection<Payment> GetByWallet(User user, Guid walletId)
        {
            try
            {
                var payments = GetAll(user);
                return payments.Where(x => x.fk_WalletId == walletId).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ICollection<Payment> GetByCurrentWallet(User user)
        {
            try
            {
                var payments = GetAll(user);
                return payments.Where(x => x.fk_WalletId == user.CurrentWallet).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ICollection<Payment> GetByCategory(User user, Guid categoryId)
        {
            try
            {
                var payments = GetAll(user);
                return payments.Where(x => x.fk_CategoryId == categoryId).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ICollection<Payment> GetByDate(User user, DateTime dateTime)
        {
            try
            {
                var payments = GetAll(user);
                return payments.Where(x => x.Date == dateTime).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ICollection<Payment> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            try
            {
                var payments = GetAll(user);
                return payments.Where(x => x.Date >= dateTimeStart || x.Date <= dateTimeEnd).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ICollection<Payment> GetByStatus(User user, Guid statusId)
        {
            try
            {
                var payments = GetAll(user);
                return payments.Where(x => x.fk_StatusId == statusId).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ICollection<Payment> GetByType(User user, Guid typeId)
        {
            try
            {
                var payments = GetAll(user);
                return payments.Where(x => x.fk_TypeId == typeId).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ICollection<Payment> GetCompleted(User user)
        {
            try
            {
                ICollection<Payment> payments = GetByCurrentWallet(user);
                return payments.Where(x => x.Status.EndPoint == true && x.Date <= DateTime.Today).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ICollection<Payment> GetAllCompleted(User user)
        {
            try
            {
                ICollection<Payment> payments = GetAll(user);
                return payments.Where(x => x.Status.EndPoint == true && x.Date <= DateTime.Today).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ICollection<Payment> GetPending(User user)
        {
            try
            {
                ICollection<Payment> payments = GetAll(user);
                return payments.Where(x => x.Status.EndPoint == false && x.Date > DateTime.Today).ToList();
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Finance;
using HomeManager.Data.Repositories.Interfaces.Finance;

namespace HomeManager.Services.Finance
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
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

        public async Task<ICollection<Payment>> GetCompleted(User user)
        {
            try
            {
                ICollection<Payment> payments = await _paymentRepository.GetAll(user);
                return payments.Where(x => x.Status.EndPoint == true && x.Date <= DateTime.Today).ToList();
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<ICollection<Payment>> GetPending(User user)
        {
            try
            {
                ICollection<Payment> payments = await _paymentRepository.GetAll(user);
                return payments.Where(x => x.Status.EndPoint == false && x.Date > DateTime.Today).ToList();
            }
            catch (Exception ex)
            {
                return new List<Payment>();
            }
        }

        public async Task<bool> Add(User user, Payment payment)
        {
            try
            {
                payment.fk_UserId = user.Id;
                return await _paymentRepository.Add(payment);
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
                var realPayment = await _paymentRepository.GetById(user, payment.Id);
                if (realPayment != null && realPayment.fk_UserId == user.Id)
                {
                    payment.fk_UserId = user.Id;
                    return await _paymentRepository.Update(payment);
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
                var realPayment = await _paymentRepository.GetById(user, payment.Id);
                if (realPayment != null && realPayment.fk_UserId == user.Id)
                {
                    payment.fk_UserId = user.Id;
                    payment.Deleted = true;
                    payment.DeletedOn = DateTime.Today;
                    return await _paymentRepository.Delete(payment);
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
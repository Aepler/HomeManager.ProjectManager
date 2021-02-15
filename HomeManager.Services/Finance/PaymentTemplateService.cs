using System;
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
    public class PaymentTemplateService : IPaymentTemplateService
    {
        private readonly IPaymentTemplateRepository _paymentTemplateRepository;

        public PaymentTemplateService(IPaymentTemplateRepository paymentTemplateRepository)
        {
            _paymentTemplateRepository = paymentTemplateRepository;
        }

        public async Task<PaymentTemplate> GetById(User user, int id)
        {
            try
            {
                return await _paymentTemplateRepository.GetById(user, id);
            }
            catch (Exception ex)
            {
                return new PaymentTemplate();
            }
        }

        public async Task<ICollection<PaymentTemplate>> GetAll(User user)
        {
            try
            {
                return await _paymentTemplateRepository.GetAll(user);
            }
            catch (Exception ex)
            {
                return new List<PaymentTemplate>();
            }
        }

        public async Task<ICollection<PaymentTemplate>> GetByCategory(User user, int fk_CategoryId)
        {
            try
            {
                return await _paymentTemplateRepository.GetByCategory(user, fk_CategoryId);
            }
            catch (Exception ex)
            {
                return new List<PaymentTemplate>();
            }
        }

        public async Task<ICollection<PaymentTemplate>> GetByType(User user, int fk_TypeId)
        {
            try
            {
                return await _paymentTemplateRepository.GetByType(user, fk_TypeId);
            }
            catch (Exception ex)
            {
                return new List<PaymentTemplate>();
            }
        }

        public async Task<bool> Add(User user, PaymentTemplate paymentTemplate)
        {
            try
            {
                paymentTemplate.fk_UserId = user.Id;
                return await _paymentTemplateRepository.Add(paymentTemplate);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(User user, PaymentTemplate paymentTemplate)
        {
            try
            {
                var realPaymentTemplates = await _paymentTemplateRepository.GetById(user, paymentTemplate.Id);
                if (realPaymentTemplates != null && realPaymentTemplates.fk_UserId == user.Id)
                {
                    paymentTemplate.fk_UserId = user.Id;
                    return await _paymentTemplateRepository.Update(paymentTemplate);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(User user, PaymentTemplate paymentTemplate)
        {
            try
            {
                var realPaymentTemplates = await _paymentTemplateRepository.GetById(user, paymentTemplate.Id);
                if (realPaymentTemplates != null && realPaymentTemplates.fk_UserId == user.Id)
                {
                    paymentTemplate.fk_UserId = user.Id;
                    paymentTemplate.Deleted = true;
                    paymentTemplate.DeletedOn = DateTime.Today;
                    return await _paymentTemplateRepository.Delete(paymentTemplate);
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

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
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;

        public TemplateService(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<Template> GetById(User user, int id)
        {
            try
            {
                return await _templateRepository.GetById(user, id);
            }
            catch (Exception ex)
            {
                return new Template();
            }
        }

        public async Task<ICollection<Template>> GetAll(User user)
        {
            try
            {
                return await _templateRepository.GetAll(user);
            }
            catch (Exception ex)
            {
                return new List<Template>();
            }
        }

        public async Task<ICollection<Template>> GetByCategory(User user, int fk_CategoryId)
        {
            try
            {
                return await _templateRepository.GetByCategory(user, fk_CategoryId);
            }
            catch (Exception ex)
            {
                return new List<Template>();
            }
        }

        public async Task<ICollection<Template>> GetByType(User user, int fk_TypeId)
        {
            try
            {
                return await _templateRepository.GetByType(user, fk_TypeId);
            }
            catch (Exception ex)
            {
                return new List<Template>();
            }
        }

        public async Task<bool> Add(User user, Template paymentTemplate)
        {
            try
            {
                paymentTemplate.fk_UserId = user.Id;
                return await _templateRepository.Add(paymentTemplate);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(User user, Template paymentTemplate)
        {
            try
            {
                var realPaymentTemplates = await _templateRepository.GetById(user, paymentTemplate.Id);
                if (realPaymentTemplates != null && realPaymentTemplates.fk_UserId == user.Id)
                {
                    paymentTemplate.fk_UserId = user.Id;
                    return await _templateRepository.Update(paymentTemplate);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(User user, Template paymentTemplate)
        {
            try
            {
                var realPaymentTemplates = await _templateRepository.GetById(user, paymentTemplate.Id);
                if (realPaymentTemplates != null && realPaymentTemplates.fk_UserId == user.Id)
                {
                    paymentTemplate.fk_UserId = user.Id;
                    paymentTemplate.Deleted = true;
                    paymentTemplate.DeletedOn = DateTime.Today;
                    return await _templateRepository.Delete(paymentTemplate);
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

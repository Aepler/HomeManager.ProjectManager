using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Entities;

namespace HomeManager.Data.Repositories.Interfaces.Finance
{
    public interface IPaymentTemplateRepository
    {
        Task<PaymentTemplate> GetById(User user, int id);
        Task<ICollection<PaymentTemplate>> GetAll(User user);
        Task<ICollection<PaymentTemplate>> GetByType(User user, int fk_TypeId);
        Task<ICollection<PaymentTemplate>> GetByCategory(User user, int fk_CategoryId);
        Task<bool> Add(PaymentTemplate paymentTemplate);
        Task<bool> Update(PaymentTemplate paymentTemplate);
        Task<bool> Delete(PaymentTemplate paymentTemplate);
    }
}

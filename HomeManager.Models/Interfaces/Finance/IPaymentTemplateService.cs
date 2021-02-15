using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Finance
{
    public interface IPaymentTemplateService
    {
        Task<PaymentTemplate> GetById(User user, int id);
        Task<ICollection<PaymentTemplate>> GetAll(User user);
        Task<ICollection<PaymentTemplate>> GetByType(User user, int fk_TypeId);
        Task<ICollection<PaymentTemplate>> GetByCategory(User user, int fk_CategoryId);
        Task<bool> Add(User user, PaymentTemplate paymentTemplate);
        Task<bool> Update(User user, PaymentTemplate paymentTemplate);
        Task<bool> Delete(User user, PaymentTemplate paymentTemplate);
    }
}

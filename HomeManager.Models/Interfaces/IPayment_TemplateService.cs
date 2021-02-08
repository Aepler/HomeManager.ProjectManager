using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Models.Interfaces
{
    public interface IPayment_TemplateService
    {
        Task<Payment_Template> GetById(User user, int id);
        Task<ICollection<Payment_Template>> GetAll(User user);
        Task<ICollection<Payment_Template>> GetByType(User user, int fk_TypeId);
        Task<ICollection<Payment_Template>> GetByCategory(User user, int fk_CategoryId);
        Task<bool> Add(Payment_Template payment_Template);
        Task<bool> Update(Payment_Template payment_Template);
    }
}

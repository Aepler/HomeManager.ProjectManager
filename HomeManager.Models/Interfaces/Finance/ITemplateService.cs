using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Finance
{
    public interface ITemplateService
    {
        Task<Template> GetById(User user, int id);
        Task<ICollection<Template>> GetAll(User user);
        Task<ICollection<Template>> GetByType(User user, int fk_TypeId);
        Task<ICollection<Template>> GetByCategory(User user, int fk_CategoryId);
        Task<bool> Add(User user, Template paymentTemplate);
        Task<bool> Update(User user, Template paymentTemplate);
        Task<bool> Delete(User user, Template paymentTemplate);
    }
}

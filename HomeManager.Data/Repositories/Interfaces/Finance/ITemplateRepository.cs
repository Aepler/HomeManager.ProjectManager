using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Entities;

namespace HomeManager.Data.Repositories.Interfaces.Finance
{
    public interface ITemplateRepository
    {
        Task<Template> GetById(User user, int id);
        Task<ICollection<Template>> GetAll(User user);
        Task<ICollection<Template>> GetByType(User user, int fk_TypeId);
        Task<ICollection<Template>> GetByCategory(User user, int fk_CategoryId);
        Task<bool> Add(Template paymentTemplate);
        Task<bool> Update(Template paymentTemplate);
        Task<bool> Delete(Template paymentTemplate);
    }
}

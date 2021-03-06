using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Services.Finance
{
    public interface ITemplateService
    {
        Task<Template> GetById(User user, Guid id);
        Task<ICollection<Template>> GetAll(User user);
        Task<ICollection<Template>> GetByType(User user, Guid typeId);
        Task<ICollection<Template>> GetByCategory(User user, Guid categoryId);
        Task<bool> Add(User user, Template paymentTemplate);
        Task<bool> Update(User user, Template paymentTemplate);
        Task<bool> Delete(User user, Template paymentTemplate);
    }
}

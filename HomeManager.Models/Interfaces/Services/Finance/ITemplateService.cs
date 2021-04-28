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
        Template GetById(User user, Guid id);
        IEnumerable<Template> GetAll(User user);
        IEnumerable<Template> GetByType(User user, Guid typeId);
        IEnumerable<Template> GetByCategory(User user, Guid categoryId);
        bool Add(User user, Template paymentTemplate);
        bool Update(User user, Template paymentTemplate);
        bool Delete(User user, Template paymentTemplate);
    }
}

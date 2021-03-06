using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Repositories.Finance
{
    public interface ITemplateRepository
    {
        Template GetById(Guid id);
        ICollection<Template> GetAll();
        bool Add(Template paymentTemplate);
        bool Update(Template paymentTemplate);
        bool Delete(Template paymentTemplate);
    }
}

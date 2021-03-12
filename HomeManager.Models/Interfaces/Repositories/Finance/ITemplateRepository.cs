using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Repositories.Finance
{
    public interface ITemplateRepository
    {
        Template GetById(Guid id);
        IQueryable<Template> GetAll();
        bool Add(Template paymentTemplate);
        bool Update(Template paymentTemplate);
        bool Delete(Template paymentTemplate);
    }
}

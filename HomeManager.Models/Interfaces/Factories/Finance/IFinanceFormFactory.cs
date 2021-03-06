using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.Models.Interfaces.Factories.Finance
{
    public interface IFinanceFormFactory
    {
        Task<string> GetCreateForm(User user, Guid typeId);
        Task<string> GetCreateFromTemplateForm(User user, Guid templateId);
        Task<string> GetEditForm(User user, Guid paymentId);
    }
}

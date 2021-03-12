using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Repositories.Finance
{
    public interface IPaymentRepository
    {
        Payment GetById(Guid id);
        IQueryable<Payment> GetAll();
        bool Add(Payment payment);
        bool Update(Payment payment);
        bool Delete(Payment payment);
    }
}

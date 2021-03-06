using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Repositories.Finance
{
    public interface IPaymentRepository
    {
        Payment GetById(Guid id);
        ICollection<Payment> GetAll();
        bool Add(Payment payment);
        bool Update(Payment payment);
        bool Delete(Payment payment);
    }
}

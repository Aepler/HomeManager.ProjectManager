using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Data.Repositories
{
    public interface IPaymentRepository
    {
        Payment GetById(int id);
        IEnumerable<Payment> GetByType(int fk_TypeID);
        IEnumerable<Payment> GetByCategory(int fk_CategoryID);
        IEnumerable<Payment> GetByStatus(int fk_StatusID);
        IEnumerable<Payment> GetAll();
        void Add(Payment payment);
        void Update(Payment payment);
    }
}

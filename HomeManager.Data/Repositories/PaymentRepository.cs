using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HomeManagerContext _context;

        public void Add(Payment payment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> GetByCategory(int fk_CategoryID)
        {
            throw new NotImplementedException();
        }

        public Payment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> GetByStatus(int fk_StatusID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> GetByType(int fk_TypeID)
        {
            throw new NotImplementedException();
        }

        public void Update(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}

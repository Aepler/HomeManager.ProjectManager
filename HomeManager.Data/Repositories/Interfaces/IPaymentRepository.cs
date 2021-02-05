using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Payment GetById(int id);
        ICollection<Payment> GetAll();
        ICollection<Payment> GetByType(int fk_TypeId);
        ICollection<Payment> GetByCategory(int fk_CategoryId);
        ICollection<Payment> GetByStatus(int fk_StatusId);
        ICollection<Payment> GetByDate(DateTime dateTime);
        ICollection<Payment> GetByDateRange(DateTime dateTimeStart, DateTime dateTimeEnd);
        ICollection<Payment> GetByUser(string user);
        bool Add(Payment payment);
        bool Update(Payment payment);
    }
}

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
        Task<Payment> GetById(User user, int id);
        Task<ICollection<Payment>> GetAll(User user);
        Task<ICollection<Payment>> GetByType(User user, int fk_TypeId);
        Task<ICollection<Payment>> GetByCategory(User user, int fk_CategoryId);
        Task<ICollection<Payment>> GetByStatus(User user, int fk_StatusId);
        Task<ICollection<Payment>> GetByDate(User user, DateTime dateTime);
        Task<ICollection<Payment>> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd);
        Task<bool> Add(Payment payment);
        Task<bool> Update(Payment payment);
        Task<bool> Delete(Payment payment);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Services.Interfaces
{
    public interface IPaymentService
    {

        Task<Payment> GetById(User user, int id);
        Task<ICollection<Payment>> GetAll(User user);
        Task<ICollection<Payment>> GetByType(User user, int fk_TypeId);
        Task<ICollection<Payment>> GetByCategory(User user, int fk_CategoryId);
        Task<ICollection<Payment>> GetByStatus(User user, int fk_StatusId);
        Task<ICollection<Payment>> GetByDate(User user, DateTime dateTime);
        Task<ICollection<Payment>> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd);
        Task<ICollection<Payment>> GetByUser(User user, string searchUser);
        Task<ICollection<Payment>> GetRealCompleted(User user);
        Task<ICollection<Payment>> GetRealPending(User user);
        Task<ICollection<Payment>> GetAllPending(User user);
        Task<ICollection<Payment>> GetBalanceToday(User user);
        Task<ICollection<Payment>> GetBalanceForDate(User user, DateTime dateTime);
        Task<bool> Add(User user, Payment payment);
        Task<bool> Update(User user, Payment payment);
    }
}

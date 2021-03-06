using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;

namespace HomeManager.Models.Interfaces.Services.Finance
{
    public interface IPaymentService
    {

        Task<Payment> GetById(User user, Guid id);
        Task<ICollection<Payment>> GetAll(User user);
        Task<ICollection<Payment>> GetByWallet(User user, Guid walletId);
        Task<ICollection<Payment>> GetByCurrentWallet(User user);
        Task<ICollection<Payment>> GetByType(User user, Guid typeId);
        Task<ICollection<Payment>> GetByCategory(User user, Guid categoryId);
        Task<ICollection<Payment>> GetByStatus(User user, Guid statusId);
        Task<ICollection<Payment>> GetByDate(User user, DateTime dateTime);
        Task<ICollection<Payment>> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd);
        Task<ICollection<Payment>> GetCompleted(User user);
        Task<ICollection<Payment>> GetPending(User user);
        Task<ICollection<Payment>> GetBalanceToday(User user);
        Task<ICollection<Payment>> GetBalanceForDate(User user, DateTime dateTime);
        Task<bool> Add(User user, Payment payment);
        Task<bool> Update(User user, Payment payment);
        Task<bool> Delete(User user, Payment payment);
    }
}

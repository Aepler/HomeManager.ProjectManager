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

        Payment GetById(User user, Guid id);
        ICollection<Payment> GetAll(User user);
        ICollection<Payment> GetByWallet(User user, Guid walletId);
        ICollection<Payment> GetByCurrentWallet(User user);
        ICollection<Payment> GetByType(User user, Guid typeId);
        ICollection<Payment> GetByCategory(User user, Guid categoryId);
        ICollection<Payment> GetByStatus(User user, Guid statusId);
        ICollection<Payment> GetByDate(User user, DateTime dateTime);
        ICollection<Payment> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd);
        ICollection<Payment> GetCompleted(User user);
        ICollection<Payment> GetAllCompleted(User user);
        ICollection<Payment> GetPending(User user);
        ICollection<Payment> GetBalanceToday(User user);
        ICollection<Payment> GetTotalBalanceToday(User user);
        bool Add(User user, Payment payment);
        bool Update(User user, Payment payment);
        bool Delete(User user, Payment payment);
    }
}

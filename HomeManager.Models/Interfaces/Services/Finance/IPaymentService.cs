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
        IEnumerable<Payment> GetAll(User user);
        IEnumerable<Payment> GetByWallet(User user, Guid walletId);
        IEnumerable<Payment> GetByCurrentWallet(User user);
        IEnumerable<Payment> GetByType(User user, Guid typeId);
        IEnumerable<Payment> GetByCategory(User user, Guid categoryId);
        IEnumerable<Payment> GetByStatus(User user, Guid statusId);
        IEnumerable<Payment> GetByDate(User user, DateTime dateTime);
        IEnumerable<Payment> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd);
        IEnumerable<Payment> GetCompleted(User user);
        IEnumerable<Payment> GetAllCompleted(User user);
        IEnumerable<Payment> GetPending(User user);
        IEnumerable<Payment> GetBalanceToday(User user);
        IEnumerable<Payment> GetTotalBalanceToday(User user);
        bool Add(User user, Payment payment);
        bool Update(User user, Payment payment);
        bool Delete(User user, Payment payment);
    }
}

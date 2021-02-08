﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payments> GetById(User user, int id);
        Task<ICollection<Payments>> GetAll(User user);
        Task<ICollection<Payments>> GetByType(User user, int fk_TypeId);
        Task<ICollection<Payments>> GetByCategory(User user, int fk_CategoryId);
        Task<ICollection<Payments>> GetByStatus(User user, int fk_StatusId);
        Task<ICollection<Payments>> GetByDate(User user, DateTime dateTime);
        Task<ICollection<Payments>> GetByDateRange(User user, DateTime dateTimeStart, DateTime dateTimeEnd);
        Task<ICollection<Payments>> GetByUser(User user, string searchUser);
        Task<bool> Add(User user, Payments payment);
        Task<bool> Update(User user, Payments payment);
    }
}

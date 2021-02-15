﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Entities;

namespace HomeManager.Data.Repositories.Interfaces.Finance
{
    public interface IStatusRepository
    {
        Task<Status> GetById(User user, int id);
        Task<ICollection<Status>> GetAll(User user);
        Task<ICollection<Status>> GetByUser(User user);
        Task<ICollection<Status>> GetByEndPoint(User user, bool endPoint);
        Task<ICollection<Status>> GetPossibleStatus(User user, int id);
        Task<ICollection<Status>> GetDefault();
        Task<bool> Add(Status status);
        Task<bool> Update(Status status);
        Task<bool> Delete(Status status);
    }
}
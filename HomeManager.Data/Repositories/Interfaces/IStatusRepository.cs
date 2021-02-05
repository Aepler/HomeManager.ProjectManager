﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface IStatusRepository
    {
        Task<Status> GetById(int id);
        Task<ICollection<Status>> GetAll();
        Task<bool> Add(Status status);
        Task<bool> Update(Status status);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;

namespace HomeManager.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetById(int id);
        Task<ICollection<Category>> GetAll();
        Task<bool> Add(Category category);
        Task<bool> Update(Category category);
    }
}

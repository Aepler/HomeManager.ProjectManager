﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Models.Interfaces;
using HomeManager.Data.Repositories.Interfaces;

namespace HomeManager.Services
{
    public class Payment_TemplateService : IPayment_TemplateService
    {
        private readonly IPayment_TemplateRepository _payment_TemplateRepository;

        public Payment_TemplateService(IPayment_TemplateRepository payment_TemplateRepository)
        {
            _payment_TemplateRepository = payment_TemplateRepository;
        }

        public async Task<bool> Add(Payment_Template payment_Template)
        {
            try
            {
                return await _payment_TemplateRepository.Add(payment_Template);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ICollection<Payment_Template>> GetAll(User user)
        {
            try
            {
                return await _payment_TemplateRepository.GetAll(user);
            }
            catch (Exception ex)
            {
                return new List<Payment_Template>();
            }
        }

        public async Task<ICollection<Payment_Template>> GetByCategory(User user, int fk_CategoryId)
        {
            try
            {
                return await _payment_TemplateRepository.GetByCategory(user, fk_CategoryId);
            }
            catch (Exception ex)
            {
                return new List<Payment_Template>();
            }
        }

        public async Task<Payment_Template> GetById(User user, int id)
        {
            try
            {
                return await _payment_TemplateRepository.GetById(user, id);
            }
            catch (Exception ex)
            {
                return new Payment_Template();
            }
        }

        public async Task<ICollection<Payment_Template>> GetByType(User user, int fk_TypeId)
        {
            try
            {
                return await _payment_TemplateRepository.GetByType(user, fk_TypeId);
            }
            catch (Exception ex)
            {
                return new List<Payment_Template>();
            }
        }

        public async Task<bool> Update(Payment_Template payment_Template)
        {
            try
            {
                return await _payment_TemplateRepository.Update(payment_Template);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

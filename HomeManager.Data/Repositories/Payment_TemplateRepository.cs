using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Data.Repositories
{
    public class Payment_TemplateRepository : IPayment_TemplateRepository
    {
        private readonly HomeManagerContext _context;

        public Payment_TemplateRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public async Task<Payment_Template> GetById(int id)
        {
            Payment_Template payment_Template = await _context.Payment_Templates.Where(x => x.Id == id).FirstOrDefaultAsync();
            return payment_Template;
        }

        public async Task<ICollection<Payment_Template>> GetAll()
        {
            ICollection<Payment_Template> payment_Templates = await _context.Payment_Templates.ToListAsync();
            return payment_Templates;
        }

        public async Task<ICollection<Payment_Template>> GetByCategory(int fk_CategoryId)
        {
            ICollection<Payment_Template> payment_Templates = await _context.Payment_Templates.Where(x => x.fk_CategoryId == fk_CategoryId).ToListAsync();
            return payment_Templates;
        }

        public async Task<ICollection<Payment_Template>> GetByType(int fk_TypeId)
        {
            ICollection<Payment_Template> payment_Templates = await _context.Payment_Templates.Where(x => x.fk_TypeId == fk_TypeId).ToListAsync();
            return payment_Templates;
        }



        public async Task<bool> Add(Payment_Template payment_Template)
        {
            try
            {
                _context.Payment_Templates.Add(payment_Template);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(Payment_Template payment_Template)
        {
            try
            {
                _context.Payment_Templates.Update(payment_Template);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

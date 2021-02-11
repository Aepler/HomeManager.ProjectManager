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

        public async Task<Payment_Template> GetById(User user, int id)
        {
            Payment_Template payment_Template = await _context.Payment_Templates.Where(x => x.fk_UserId == user.Id && x.Id == id && x.Deleted == false).Include(x => x.Category).Include(x => x.Type).Include(x => x.User).FirstOrDefaultAsync();
            return payment_Template;
        }

        public async Task<ICollection<Payment_Template>> GetAll(User user)
        {
            ICollection<Payment_Template> payment_Templates = await _context.Payment_Templates.Include(x => x.Category).Include(x => x.Type).Include(x => x.User).Where(x => x.fk_UserId == user.Id && x.Deleted == false).ToListAsync();
            return payment_Templates;
        }

        public async Task<ICollection<Payment_Template>> GetByCategory(User user, int fk_CategoryId)
        {
            ICollection<Payment_Template> payment_Templates = await _context.Payment_Templates.Include(x => x.Category).Include(x => x.Type).Include(x => x.User).Where(x => x.fk_UserId == user.Id && x.fk_CategoryId == fk_CategoryId && x.Deleted == false).ToListAsync();
            return payment_Templates;
        }

        public async Task<ICollection<Payment_Template>> GetByType(User user, int fk_TypeId)
        {
            ICollection<Payment_Template> payment_Templates = await _context.Payment_Templates.Include(x => x.Category).Include(x => x.Type).Include(x => x.User).Where(x => x.fk_UserId == user.Id && x.fk_TypeId == fk_TypeId && x.Deleted == false).ToListAsync();
            return payment_Templates;
        }



        public async Task<bool> Add(User user, Payment_Template payment_Template)
        {
            try
            {
                payment_Template.fk_UserId = user.Id;
                _context.Payment_Templates.Add(payment_Template);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(User user, Payment_Template payment_Template)
        {
            try
            {
                var realPaymentTemplates = await _context.Payment_Templates.AsNoTracking().FirstAsync(x => x.Id == payment_Template.Id);
                if (realPaymentTemplates != null && realPaymentTemplates.fk_UserId == user.Id)
                {
                    payment_Template.fk_UserId = user.Id;
                    _context.Payment_Templates.Update(payment_Template);
                    await _context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(User user, Payment_Template payment_Template)
        {
            try
            {
                var realPaymentTemplates = await _context.Payment_Templates.FindAsync(payment_Template.Id);
                if (realPaymentTemplates != null && realPaymentTemplates.fk_UserId == user.Id)
                {
                    payment_Template.Deleted = true;
                    payment_Template.DeletedOn = DateTime.Today;
                    _context.Payment_Templates.Update(payment_Template);
                    await _context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Data.Repositories.Interfaces.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HomeManager.Data.Repositories.Finance
{
    public class PaymentTemplateRepository : IPaymentTemplateRepository
    {
        private readonly HomeManagerContext _context;

        public PaymentTemplateRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public async Task<PaymentTemplate> GetById(User user, int id)
        {
            return await _context.FinancePaymentTemplates.Where(x => x.fk_UserId == user.Id && x.Id == id && x.Deleted == false).Include(x => x.Category).Include(x => x.Type).Include(x => x.User).FirstOrDefaultAsync();
        }

        public async Task<ICollection<PaymentTemplate>> GetAll(User user)
        {
            return await _context.FinancePaymentTemplates.Include(x => x.Category).Include(x => x.Type).Include(x => x.User).Where(x => x.fk_UserId == user.Id && x.Deleted == false).ToListAsync();
        }

        public async Task<ICollection<PaymentTemplate>> GetByCategory(User user, int fk_CategoryId)
        {
            return await _context.FinancePaymentTemplates.Include(x => x.Category).Include(x => x.Type).Include(x => x.User).Where(x => x.fk_UserId == user.Id && x.fk_CategoryId == fk_CategoryId && x.Deleted == false).ToListAsync();
        }

        public async Task<ICollection<PaymentTemplate>> GetByType(User user, int fk_TypeId)
        {
            return await _context.FinancePaymentTemplates.Include(x => x.Category).Include(x => x.Type).Include(x => x.User).Where(x => x.fk_UserId == user.Id && x.fk_TypeId == fk_TypeId && x.Deleted == false).ToListAsync();
        }



        public async Task<bool> Add(PaymentTemplate paymentTemplate)
        {
            try
            {
                _context.FinancePaymentTemplates.Add(paymentTemplate);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(PaymentTemplate paymentTemplate)
        {
            try
            {
                _context.FinancePaymentTemplates.Update(paymentTemplate);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(PaymentTemplate paymentTemplate)
        {
            try
            {
                _context.FinancePaymentTemplates.Update(paymentTemplate);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

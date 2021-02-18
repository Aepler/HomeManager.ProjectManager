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
    public class TemplateRepository : ITemplateRepository
    {
        private readonly HomeManagerContext _context;

        public TemplateRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public async Task<Template> GetById(User user, int id)
        {
            return await _context.FinanceTemplates.Where(x => x.fk_UserId == user.Id && x.Id == id && !x.Deleted).Include(x => x.Category).Include(x => x.Type).Include(x => x.User).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Template>> GetAll(User user)
        {
            return await _context.FinanceTemplates.Where(x => x.fk_UserId == user.Id && !x.Deleted).Include(x => x.Category).Include(x => x.Type).Include(x => x.User).ToListAsync();
        }

        public async Task<ICollection<Template>> GetByCategory(User user, int fk_CategoryId)
        {
            return await _context.FinanceTemplates.Where(x => x.fk_UserId == user.Id && x.fk_CategoryId == fk_CategoryId && !x.Deleted).Include(x => x.Category).Include(x => x.Type).Include(x => x.User).ToListAsync();
        }

        public async Task<ICollection<Template>> GetByType(User user, int fk_TypeId)
        {
            return await _context.FinanceTemplates.Where(x => x.fk_UserId == user.Id && x.fk_TypeId == fk_TypeId && !x.Deleted).Include(x => x.Category).Include(x => x.Type).Include(x => x.User).ToListAsync();
        }



        public async Task<bool> Add(Template paymentTemplate)
        {
            try
            {
                _context.FinanceTemplates.Add(paymentTemplate);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(Template paymentTemplate)
        {
            try
            {
                _context.FinanceTemplates.Update(paymentTemplate);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(Template paymentTemplate)
        {
            try
            {
                _context.FinanceTemplates.Update(paymentTemplate);
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

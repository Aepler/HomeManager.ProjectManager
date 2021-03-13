using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Repositories.Finance;
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

        public Template GetById(Guid id)
        {
            return _context.FinanceTemplates.Where(x => x.Id == id).Include(x => x.Category).Include(x => x.Type).FirstOrDefault();
        }

        public IQueryable<Template> GetAll()
        {
            return _context.FinanceTemplates.Include(x => x.Category).Include(x => x.Type);
        }

        public bool Add(Template paymentTemplate)
        {
            try
            {
                _context.FinanceTemplates.Add(paymentTemplate);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(Template paymentTemplate)
        {
            try
            {
                _context.FinanceTemplates.Update(paymentTemplate);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(Template paymentTemplate)
        {
            try
            {
                _context.FinanceTemplates.Update(paymentTemplate);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

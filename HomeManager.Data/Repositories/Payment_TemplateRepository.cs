using HomeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Data.Repositories.Interfaces;

namespace HomeManager.Data.Repositories
{
    public class Payment_TemplateRepository : IPayment_TemplateRepository
    {
        private readonly HomeManagerContext _context;

        public Payment_TemplateRepository(HomeManagerContext context)
        {
            _context = context;
        }

        public Payment_Template GetById(int id)
        {
            Payment_Template payment_Template = _context.Payment_Templates.Find(id);
            return payment_Template;
        }

        public ICollection<Payment_Template> GetAll()
        {
            ICollection<Payment_Template> payment_Templates = _context.Payment_Templates.ToList();
            return payment_Templates;
        }

        public ICollection<Payment_Template> GetByCategory(int fk_CategoryId)
        {
            ICollection<Payment_Template> payment_Templates = _context.Payment_Templates.Where(x => x.fk_CategoryId == fk_CategoryId).ToList();
            return payment_Templates;
        }

        public ICollection<Payment_Template> GetByType(int fk_TypeId)
        {
            ICollection<Payment_Template> payment_Templates = _context.Payment_Templates.Where(x => x.fk_TypeId == fk_TypeId).ToList();
            return payment_Templates;
        }



        public bool Add(Payment_Template payment_Template)
        {
            try
            {
                _context.Payment_Templates.Add(payment_Template);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Payment_Template payment_Template)
        {
            try
            {
                _context.Payment_Templates.Update(payment_Template);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

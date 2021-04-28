using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Interfaces.Services.Finance;
using HomeManager.Models.Interfaces.Repositories.Finance;

namespace HomeManager.Services.Finance
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;

        public TemplateService(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public Template GetById(User user, Guid id)
        {
            try
            {
                var template = _templateRepository.GetById(id);
                if (template.fk_UserId == user.Id && !template.Deleted)
                {
                    return template;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Template> GetAll(User user)
        {
            try
            {
                return _templateRepository.GetAll().Where(x => x.fk_UserId == user.Id && !x.Deleted);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Template> GetByCategory(User user, Guid categoryId)
        {
            try
            {
                return GetAll(user).Where(x => x.fk_CategoryId == categoryId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Template> GetByType(User user, Guid typeId)
        {
            try
            {
                return GetAll(user).Where(x => x.fk_TypeId == typeId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Add(User user, Template template)
        {
            try
            {
                template.fk_UserId = user.Id;
                return _templateRepository.Add(template);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(User user, Template template)
        {
            try
            {
                var realtemplate = GetById(user, template.Id);
                if (realtemplate != null)
                {
                    template.fk_UserId = user.Id;
                    return _templateRepository.Update(template);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(User user, Template template)
        {
            try
            {
                var realtemplate = GetById(user, template.Id);
                if (realtemplate != null)
                {
                    template.fk_UserId = user.Id;
                    template.Deleted = true;
                    template.DeletedOn = DateTime.Today;
                    return _templateRepository.Delete(template);
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

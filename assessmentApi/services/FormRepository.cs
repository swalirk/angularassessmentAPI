using assessmentApi.Models;
using assessmentApi.services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace assessmentApi.services
{
    public class FormRepository:IFormInterface
    {
        private readonly formDbContext dbContext;
        public FormRepository(formDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        

        public async Task<Form> AddForm(Form form)
        {
            if (form != null)
            {
                form.Id = Guid.NewGuid();
                await dbContext.Forms.AddAsync(form);
                await dbContext.SaveChangesAsync();
                return form;
            }
            else
            {
                return null;
            }
        }
        public bool IsExists(Guid id)
        {
            return dbContext.Forms.Any(c => c.Id == id);
        }
        public bool DeleteForm(Guid id)
        {
            var form = dbContext.Forms.Find(id);
            dbContext.Remove(form);
            return dbContext.SaveChanges() > 0 ? true : false;
        }
        public ICollection<Form> GetAllForms()
        {
            return dbContext.Forms.ToList();

        }
        public async Task<bool> UpdateForm(Guid id, Form form)
        {
            dbContext.Forms.Entry(form).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Form GetAllFormsById(Guid id)
        {
            return dbContext.Forms.FirstOrDefault(f => f.Id == id);
        }
    }
}

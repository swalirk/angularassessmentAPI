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
       

        public async Task DeleteForm(Guid id)
        {
            var formToDelete = await dbContext.Forms.FirstOrDefaultAsync(f => f.Id == id);

            if (formToDelete != null)
            {
                dbContext.Forms.Remove(formToDelete);
                await dbContext.SaveChangesAsync();
            }
        }

       
    public async Task<ICollection<Form>> GetAllForms()
        {
          

            var forms = await dbContext.Forms.ToListAsync();
            return forms.Count > 0 ? forms : null;
        }
       

      

        public async Task<Form> GetAllFormsById(Guid id)
        {
         
            var form = await dbContext.Forms.FirstOrDefaultAsync(f => f.Id == id);
            return form != null ? form : null;
        }



        public async Task<bool> IsExists(Guid id)
        {
            return await dbContext.Forms.AnyAsync(f => f.Id == id);
        }

        public async Task<bool> UpdateForm(Guid id, Form form)
        {
            var existingForm = await dbContext.Forms.FirstOrDefaultAsync(f => f.Id == id);

            if (existingForm == null)
            {
                return false; 
            }
            dbContext.Entry(existingForm).CurrentValues.SetValues(form);

            await dbContext.SaveChangesAsync();

            return true; 
        }
    }
}

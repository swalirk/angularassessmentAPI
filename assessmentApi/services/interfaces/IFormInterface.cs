using assessmentApi.Models;

namespace assessmentApi.services.interfaces
{
    public interface IFormInterface
    {
        public Task<Form> AddForm(Form form);
        
        public Task<Form> GetAllFormsById(Guid id);
        public Task<bool> UpdateForm(Guid id, Form form);
        public Task DeleteForm(Guid id);
        public Task<bool> IsExists(Guid id);
     
        public  Task<ICollection<Form>> GetAllForms();
       
   

    }
}

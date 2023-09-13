using assessmentApi.Models;

namespace assessmentApi.services.interfaces
{
    public interface IFormInterface
    {
        public Task<Form> AddForm(Form form);
        public bool DeleteForm(Guid id);
        public bool IsExists(Guid id);
        public ICollection<Form> GetAllForms();
        public Form GetAllFormsById(Guid id);
        public  Task<bool> UpdateForm(Guid id, Form form);

    }
}

using assessmentApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace assessmentApi.services.interfaces
{
    public interface ITableInterface
    {

       


        public  Task<Aotable> GetTableById(Guid id);

        public  Task<ICollection<TableInfo>> GetTableNames();
      
    }
}

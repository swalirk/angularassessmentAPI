using assessmentApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace assessmentApi.services.interfaces
{
    public interface ITableInterface
    {
       
        public Aotable GetTableById(Guid id);

        

        
        public ICollection<TableInfo> GetTableNames();
    }
}

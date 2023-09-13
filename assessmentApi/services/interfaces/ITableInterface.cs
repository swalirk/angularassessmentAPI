using assessmentApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace assessmentApi.services.interfaces
{
    public interface ITableInterface
    {
       
        

        

        
        public ICollection<TableInfo> GetTableNames();
    }
}

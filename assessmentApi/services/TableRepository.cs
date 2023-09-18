using assessmentApi.Models;
using assessmentApi.services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assessmentApi.services
{
    public class TableRepository : ITableInterface
    {
        private readonly formDbContext dbContext;
        public TableRepository(formDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        




        public async Task<ICollection<TableInfo>> GetTableNames()
        {
           
            var tableInfoList = await dbContext.Aotables
                .Select(table => new TableInfo
                {
                    Id = table.Id,
                    Name = table.Name
                })
                .ToListAsync();

           
            return tableInfoList.Count > 0 ? tableInfoList : null;
        }
       

        public async Task<Aotable> GetTableById(Guid id)
        {
            
            var table = await dbContext.Aotables.FirstOrDefaultAsync(f => f.Id == id);
            return table != null ? table : null;
        }

    }
}

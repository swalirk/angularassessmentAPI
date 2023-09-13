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

        
        public ICollection<TableInfo> GetTableNames()
        {
            // Query the dbContext to get table names and IDs
            var tableInfoList = dbContext.Aotables
                .Select(table => new TableInfo
                {
                    Id = table.Id,
                    Name = table.Name
                })
                .ToList();

            return tableInfoList;
        }
    }
}

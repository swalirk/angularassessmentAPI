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

        public async Task<Aotable> AddTable(Aotable table)
        {
            if (table != null)
            {
                table.Id = Guid.NewGuid();
                await dbContext.Aotables.AddAsync(table);
                await dbContext.SaveChangesAsync();
                return table;
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<Aotable> GetAllCoverageFormRecords()
        {
            var Records = dbContext.Aotables
               .Where(r => r.Type == "coverage" || r.Type == "form")
               .ToList();
            return Records != null ? Records : Enumerable.Empty<Aotable>();
        }
        public IEnumerable<Aotable> GetAllTablesWithName(string searchWord)
        {

            var records = dbContext.Aotables
                .Where(a => a.Name.Contains(searchWord));
            return records != null ? records : Enumerable.Empty<Aotable>();
        }

       
        public Aotable GetTableById(Guid id)
        {
            return dbContext.Aotables.FirstOrDefault(vt => vt.Id == id);
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

using assessmentApi.Models;
using assessmentApi.services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assessmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableInterface tableInterface;

        public TableController(ITableInterface tableInterface)
        {
            this.tableInterface = tableInterface;

        }
        //[HttpGet("{id}")]

        //public IActionResult GetTableById(Guid id)
        //{
        //    try
        //    {
        //        var vehicleType = tableInterface.GetTableById(id);


        //        return Ok(vehicleType);


        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet]
        [Route("getAllTableNames")]
        public IActionResult GetAllTableNames()
        {
            try
            {
                var names = tableInterface.GetTableNames();
                return Ok(names);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //[HttpPost]
        //public async Task<IActionResult> AddTable([FromBody] Aotable table)
        //{

        //    try
        //    {
        //        if (table != null)
        //        {
        //            table.Id = new Guid();
        //            var newTable = await tableInterface.AddTable(table);
        //            if (newTable != null)
        //            {
        //                return Ok(newTable);
        //            }
        //            else
        //            {
        //                return BadRequest("Table Cannot be null");
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        


       
        

    }
}

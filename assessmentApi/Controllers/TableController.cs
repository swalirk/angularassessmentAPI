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
       


        [HttpGet]
        [Route("getAllTableNames")]
        public async Task<IActionResult> GetAllTableNames()
        {
            try
            {
                var names = await tableInterface.GetTableNames(); 
                if(names == null)
                {
                    return BadRequest("Data is Empty");
                }
                else
                { return Ok(names); 
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableById([FromRoute] Guid id)
        {
            try
            {
                var table = await tableInterface.GetTableById(id);
                if (table == null)
                {
                    return BadRequest("Data Not Found");
                }
                else
                {
                    return Ok(table);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}

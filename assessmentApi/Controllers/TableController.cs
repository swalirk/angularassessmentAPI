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

        [HttpGet("{id}")]

        public IActionResult GetTableById(Guid id)
        {
            try
            {
                var table = tableInterface.GetTableById(id);


                return Ok(table);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }










    }
}

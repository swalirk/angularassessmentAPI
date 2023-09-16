using assessmentApi.Models;
using assessmentApi.services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assessmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormInterface formInterface;

        public FormController(IFormInterface formInterface)
        {
            this.formInterface = formInterface;
        }

       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllFormsById([FromRoute] Guid id)
        {
            try
            {
                var form = await formInterface.GetAllFormsById(id);
                if (form == null)
                {
                    return BadRequest("Data Not Found");
                }
                else
                {
                    return Ok(form);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForm([FromRoute] Guid id, [FromBody] Form form)
        {
            try
            {
                if (id != form.Id)
                {
                    return BadRequest();
                }

                var isTrue = await formInterface.IsExists(id);

                if (isTrue)
                {
                    var success = await formInterface.UpdateForm(id, form);

                    if (success)
                    {
                        var data = new { status = "Success" };
                        return Ok(data);
                    }
                    else
                    {
                        return BadRequest("Update failed");
                    }
                }
                else
                {
                    return BadRequest("Id not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





       

        [HttpPost]
        public async Task<IActionResult> AddForm([FromBody] Form form)
        {

            try
            {
                if (form != null)
                {
                    
                    var newForm = await formInterface.AddForm(form);
                   
                        return Ok(newForm);
                   
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
               
                return BadRequest(ex.Message);
            }
        }
       


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForm([FromRoute]Guid id)
        {
            try
            {
                var isExists = await formInterface.IsExists(id);

                if (isExists)
                {
                    await formInterface.DeleteForm(id);

                    var data = new { status = "Deleted" };
                    return Ok(data);
                }
                else
                {
                    return BadRequest("Id not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]

        public async Task<IActionResult> GetAllForms()
        {
            try
            {
                var forms = await formInterface.GetAllForms();

                if (forms.Count==0)
                {
                    return BadRequest("Data Not Found");
                }
                else
                {
                    return Ok(forms);

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

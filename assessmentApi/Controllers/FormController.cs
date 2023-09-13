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

        public IActionResult GetAllFormsById(Guid id)
        {
            try
            {
                var form = formInterface.GetAllFormsById(id);


                return Ok(form);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateForm(Guid id, [FromBody] Form form)
        {
            try
            {
                if (id != form.Id)
                {
                    return BadRequest();
                }
                var istrue = formInterface.IsExists(id);
                if (istrue == true)
                {
                    var success = formInterface.UpdateForm(id, form);
                    var data = (new { status = "Success" });
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


        [HttpPost]
        public async Task<IActionResult> AddForm([FromBody] Form form)
        {

            try
            {
                if (form != null)
                {
                    form.Id = new Guid();
                    var newForm = await formInterface.AddForm(form);
                    if (newForm != null)
                    {
                        return Ok(newForm);
                    }
                    else
                    {
                        return BadRequest("Form Cannot be null");
                    }
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
        [HttpDelete]

        public ActionResult DeleteForm(Guid id)
        {
            try
            {
                if (formInterface.IsExists(id))
                {
                    formInterface.DeleteForm(id);
                    var data = (new { status = "Deleted" });
                    return Ok(data);
                }
                else
                {
                    return BadRequest("Something Went Wrong");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]

        public IActionResult GetAllForms()
        {
            try
            {
                var forms = formInterface.GetAllForms();

                if (forms.Count == 0)
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

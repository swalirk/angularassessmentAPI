using assessmentApi.Controllers;
using assessmentApi.Models;
using assessmentApi.services.interfaces;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing.Drawing2D;

namespace TestProject
{
    public class TestFormController
    {
        private readonly IFixture fixture;
        private readonly Mock<IFormInterface> formInterface;

        private readonly FormController formController;
      
        public TestFormController()
        {
            fixture = new Fixture();
            formInterface = new Mock<IFormInterface>();
            
            
            formController=new FormController(formInterface.Object);
            
        }
       

       



        [Fact]
        public async Task GetAllFormsById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var id = fixture.Create<Guid>();
           
            var form = fixture.Create<Form>(); 
            formInterface.Setup(repo => repo.GetAllFormsById(id)).ReturnsAsync(form);
           

            // Act
            var result = await formController.GetAllFormsById(id);

            // Assert
            result.Should().NotBeNull();
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            formInterface.Verify(repo => repo.GetAllFormsById(id).Result, Times.Once());
        }

        [Fact]
        public async Task GetAllFormsById_DataNotFound_ReturnsBadRequest()
        {
            // Arrange
            var id = fixture.Create<Guid>();
            
            formInterface.Setup(repo => repo.GetAllFormsById(id)).ReturnsAsync((Form)null); // Simulate data not found
            

            // Act
            var result = await formController.GetAllFormsById(id);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Data Not Found", badRequestResult.Value);
            formInterface.Verify(repo => repo.GetAllFormsById(id), Times.Once());
        }

        [Fact]
        public async Task GetAllFormsById_ExceptionThrown_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            var id = fixture.Create<Guid>();
            var errorMessage = "An error occurred";
           
            formInterface.Setup(repo => repo.GetAllFormsById(id)).ThrowsAsync(new Exception(errorMessage));
           

            // Act
            var result = await formController.GetAllFormsById(id);

            // Assert
            result?.Should().NotBeNull();
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
            formInterface.Verify(repo => repo.GetAllFormsById(id), Times.Once());
        }




        [Fact]
        public async Task AddForm_ValidData_ReturnsOkResult()
        {
            // Arrange
            var form = fixture.Create<Form>();
           var returnData= fixture.Create<Form>();
            formInterface.Setup(repo => repo.AddForm(form)).ReturnsAsync(returnData); 
           

            // Act
            var result = await formController.AddForm(form);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            var okObjectResult = result.As<OkObjectResult>();
            okObjectResult.Value.Should().BeEquivalentTo(returnData);
            formInterface.Verify(t => t.AddForm(form), Times.Once());
        }


        [Fact]
        public void AddForm_NullInput_ReturnsBadRequest()
        {
            //Arrange

            Form formData = null;
            formInterface.Setup(c => c.AddForm(formData)).ReturnsAsync((Form)null);

            // Act
            var result = formController.AddForm(formData);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            formInterface.Verify(t => t.AddForm(formData), Times.Never());
        }

        [Fact]

        public void AddForm_ExceptionThrown_ReturnsBadRequest()
        {
            // Arrange
            var form = fixture.Create<Form>();
            formInterface.Setup(repo => repo.AddForm(form))
                .ThrowsAsync(new Exception("Some error message"));


            // Act
            var result = formController.AddForm(form);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            formInterface.Verify(t => t.AddForm(form), Times.Once());

        }

        [Fact]
        public async Task DeleteForm_FormExists_ReturnsOkResult()
        {
            // Arrange
            var id=fixture.Create<Guid>();
            var form = fixture.Create<Form>();
            formInterface.Setup(repo => repo.IsExists(id)).ReturnsAsync(true);
            formInterface.Setup(repo => repo.DeleteForm(id)).Returns(Task.CompletedTask);



            // Act
            var result = await formController.DeleteForm(id);

            // Assert
            result.Should().NotBeNull();
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().BeEquivalentTo(new { status = "Deleted" });
            formInterface.Verify(x => x.DeleteForm(id), Times.Once());
            formInterface.Verify(x => x.DeleteForm(id), Times.Once());

        }

        [Fact]
        public async Task DeleteForm_FormDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            var id = fixture.Create<Guid>();
            formInterface.Setup(repo => repo.IsExists(id)).ReturnsAsync(false);

            // Act
            var result = await formController.DeleteForm(id);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be("Id not found");
            formInterface.Verify(repo => repo.IsExists(id),Times.Once());
            formInterface.Verify(x => x.DeleteForm(id), Times.Never);
        }



        [Fact]
        public async Task DeleteForm_ExceptionThrown_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            var id = Guid.NewGuid();
            var errorMessage = "An error occurred";
            formInterface.Setup(repo => repo.IsExists(id)).ReturnsAsync(true);
            formInterface.Setup(repo => repo.DeleteForm(id)).ThrowsAsync(new Exception(errorMessage));

            // Act
            var result = await formController.DeleteForm(id);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be(errorMessage);
            formInterface.Verify(repo => repo.IsExists(id), Times.Once());
            formInterface.Verify(x => x.DeleteForm(id), Times.Once);
        }



        [Fact]
        public async Task GetAllForms_FormsExist_ReturnsOkResult()
        {
            // Arrange

            List<Form> formList = fixture.CreateMany<Form>().ToList();
            formInterface.Setup(repo => repo.GetAllForms()).ReturnsAsync(formList);
           

            // Act
            var result = await formController.GetAllForms();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;

            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            var forms = okResult.Value as List<Form>;
            forms.Should().NotBeNull().And.NotBeEmpty();
            Assert.Equal(formList.Count, forms.Count());
            formInterface.Verify(repo => repo.GetAllForms(), Times.Once());

        }

        [Fact]
        public void GetAllForms_Return_BadRequest_WhenDatanotFound()
        {
            //Arrange


            formInterface.Setup(repo => repo.GetAllForms()).ReturnsAsync(new List<Form>());

            // Act
            var result = formController.GetAllForms();

            // Assert
            result.Should().NotBeNull();
            Assert.Equal("Data Not Found", ((string)((ObjectResult)result.Result).Value));
            formInterface.Verify(repo => repo.GetAllForms(), Times.Once());




        }

        [Fact]
        public async Task GetAllForms_Exception_ReturnsBadRequestWithExceptionMessage()
        {
            // Arrange
            
            formInterface.Setup(repo => repo.GetAllForms()).ThrowsAsync(new Exception("Custom exception message"));

           

            // Act
            var result = await formController.GetAllForms();

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Custom exception message", badRequestResult.Value);
            formInterface.Verify(repo => repo.GetAllForms(), Times.Once());
        }


        [Fact]
        public async Task UpdateForm_ValidForm_ReturnsOkResult()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var updateForm = fixture.Create<Form>();
            updateForm.Id = id;

            formInterface.Setup(v => v.IsExists(id)).ReturnsAsync(true);
            formInterface.Setup(repo => repo.UpdateForm(id, updateForm)).ReturnsAsync(true);

          

            // Act
            var result = await formController.UpdateForm(id, updateForm);

            // Assert
            result.Should().NotBeNull();
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            okResult.Value.Should().BeEquivalentTo(new { status = "Success" });
            formInterface.Verify(v => v.IsExists(id), Times.Once());
            formInterface.Verify(b => b.UpdateForm(id, updateForm), Times.Once());
        }

        [Fact]
        public void UpdateForm_ShouldReturnBadRequestResponse_WhenIdNotInDataBase()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            var updateForm = fixture.Create<Form>();

            updateForm.Id = id;
            formInterface.Setup(x => x.IsExists(id)).ReturnsAsync(false);
            formInterface.Setup(x => x.UpdateForm(id, updateForm)).ReturnsAsync(false);

            //Act
            var result = formController.UpdateForm(id, updateForm);
            //Assert
            result.Should().NotBeNull();
            Assert.Equal("Id not found", ((string)((BadRequestObjectResult)result.Result).Value));
            formInterface.Verify(v => v.IsExists(id), Times.Once());
            formInterface.Verify(x => x.UpdateForm(id, updateForm), Times.Never());

        }

        [Fact]
        public void UpdateForm_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var updateForm = fixture.Create<Form>();
            updateForm.Id = id;

            formInterface.Setup(v => v.IsExists(id)).ReturnsAsync(true);
            formInterface.Setup(b => b.UpdateForm(id, updateForm)).ThrowsAsync(new Exception("Something went wrong"));

            // Act
            var result = formController.UpdateForm(id, updateForm);

            // Assert
            result.Should().NotBeNull();
            Assert.Equal("Something went wrong", ((string)((BadRequestObjectResult)result.Result).Value));
            formInterface.Verify(v => v.IsExists(id), Times.Once());
            formInterface.Verify(b => b.UpdateForm(id, updateForm), Times.Once());
        }





    }
}

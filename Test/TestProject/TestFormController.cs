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
        public void AddForm_ValidInput_ReturnsOkResult()
        {
            // Arrange

            var form = fixture.Create<Form>();
            var returnData = fixture.Create<Form>();
            formInterface.Setup(c => c.AddForm(form)).ReturnsAsync(returnData);
            // Act
            var result = formController.AddForm(form);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            var okObjectResult = result.Result.As<OkObjectResult>();
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
        public void GetAllForms_Return_Forms()
        {
            // Arrange
            List<Form> formList = fixture.CreateMany<Form>().ToList();
            formInterface.Setup(c => c.GetAllForms()).Returns(formList);

            // Act
            var result = formController.GetAllForms();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;

            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            var forms = okResult.Value as List<Form>;
            forms.Should().NotBeNull().And.NotBeEmpty();
            Assert.Equal(formList.Count, forms.Count());

        }
        [Fact]
        public void GetAllForms_Return_BadRequest_WhenDatanotFound()
        {
            //Arrange
            var formList = fixture.CreateMany<Form>(0).ToList();

            formInterface.Setup(c => c.GetAllForms()).Returns(formList);

            // Act
            var result = formController.GetAllForms();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            var formlists = badRequestResult.Value as List<Form>;
            Assert.Null(formlists);
            result.Should().BeAssignableTo<BadRequestObjectResult>().Which.Value.Should().Be("Data Not Found");
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);


        }
        [Fact]
        public void GetAllForms_Exception_ReturnsBadRequestWithExceptionMessage()
        {


            // Arrange

            formInterface.Setup(c => c.GetAllForms()).Throws(new Exception("Something went wrong"));

            // Act
            var result = formController.GetAllForms();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("Something went wrong", badRequestResult.Value);
        }

        [Fact]
        public void DeleteForm_ValidId_ReturnsOkResult()
        {
            // Arrange

            var form = fixture.Create<Form>();
            formInterface.Setup(x => x.DeleteForm(form.Id)).Returns(true);
            formInterface.Setup(x => x.IsExists(form.Id)).Returns(true);


            // Act
            var result = formController.DeleteForm(form.Id);

            // Assert
            result.Should().NotBeNull();
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().BeEquivalentTo(new { status = "Deleted" });
            formInterface.Verify(x => x.DeleteForm(form.Id), Times.Once());
            formInterface.Verify(x => x.DeleteForm(form.Id), Times.Once());
        }
        [Fact]
        public void DeleteBrand_InvalidId_ReturnsBadRequestWithMessage()
        {
            // Arrange

            var form = fixture.Create<Form>();
            formInterface.Setup(x => x.DeleteForm(form.Id)).Returns(false);
            formInterface.Setup(x => x.IsExists(form.Id)).Returns(false);

            // Act
            var result = formController.DeleteForm(form.Id);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something Went Wrong", badRequestResult.Value);
            
            formInterface.Verify(x => x.DeleteForm(form.Id), Times.Never());
            formInterface.Verify(x => x.IsExists(form.Id), Times.Once());
        }
        [Fact]
        public void DeleteBrand_Exception_ReturnsBadRequestWithExceptionMessage()
        {
            // Arrange
            var form = fixture.Create<Form>();
            var exceptionMessage = "An error occurred.";
            formInterface.Setup(repo => repo.IsExists(form.Id)).Returns(true);
            formInterface.Setup(repo => repo.DeleteForm(form.Id))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = formController.DeleteForm(form.Id);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestResult.Value);
            formInterface.Verify(x => x.DeleteForm(form.Id), Times.Once());
            formInterface.Verify(x => x.DeleteForm(form.Id), Times.Once());
        }
        [Fact]
        public  void UpdateForm_ShouldReturnOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var updateForm = fixture.Create<Form>();
            updateForm.Id = id;
            formInterface.Setup(v => v.IsExists(id)).Returns(true);
            formInterface.Setup(b => b.UpdateForm(id, updateForm)).ReturnsAsync(true);

            // Act
            var result = formController.UpdateForm(id, updateForm);

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
        public void UpdateForm_InvalidData_ShouldReturnBadRequestResult()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var updateForm = fixture.Create<Form>();

            formInterface.Setup(c => c.UpdateForm(id, updateForm)).ReturnsAsync(false);

            // Act
            var result = formController.UpdateForm(id, updateForm);

            // Assert

            result.Should().NotBeNull();        
            result.Should().BeAssignableTo<BadRequestResult>();
            formInterface.Verify(v => v.IsExists(id), Times.Never());
            formInterface.Verify(b => b.UpdateForm(id, updateForm), Times.Never());
        }

        [Fact]
        public void UpdateForm_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var updateForm = fixture.Create<Form>();
            updateForm.Id = id;

            formInterface.Setup(v => v.IsExists(id)).Returns(true);
            formInterface.Setup(b => b.UpdateForm(id, updateForm)).Throws(new Exception("Something went wrong"));

            // Act
            var result =  formController.UpdateForm(id, updateForm);

            // Assert
            result.Should().NotBeNull();
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("Something went wrong", badRequestResult.Value);
            formInterface.Verify(v => v.IsExists(id), Times.Once());
            formInterface.Verify(b => b.UpdateForm(id, updateForm), Times.Once());
        }

        [Fact]
        public void UpdateVehicleType_ShouldReturnBadRequestResponse_WhenIdNotInDataBase()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            var updateForm = fixture.Create<Form>();

            updateForm.Id = id;
            formInterface.Setup(x => x.IsExists(id)).Returns(false);
            formInterface.Setup(x => x.UpdateForm(id, updateForm)).ReturnsAsync(false);

            //Act
            var result = formController.UpdateForm(id, updateForm);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>().Which.Value.Should().Be("Id not found");
            formInterface.Verify(v => v.IsExists(id), Times.Once());
            formInterface.Verify(x => x.UpdateForm(id, updateForm), Times.Never());

        }
        [Fact]
        public void GetAllFormsById_ValidId_ReturnsOkResult()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var form = fixture.Create<Form>();
            formInterface.Setup(x => x.GetAllFormsById(id)).Returns(form);

            // Act
            var result = formController.GetAllFormsById(id);

            // Assert
            result.Should().NotBeNull();
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().BeEquivalentTo(form); 
        }
        

        [Fact]
        public void GetAllFormsById_Exception_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            formInterface.Setup(x => x.GetAllFormsById(id)).Throws(new Exception("Test Exception"));

            // Act
            var result = formController.GetAllFormsById(id);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            badRequestResult.Value.Should().Be("Test Exception");
        }
        
    }
}
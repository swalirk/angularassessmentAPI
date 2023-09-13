using assessmentApi.Controllers;
using assessmentApi.Models;
using assessmentApi.services.interfaces;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class TestTableController
    {
        private readonly IFixture fixture;
        private readonly Mock<ITableInterface> tableInterface;

        private readonly TableController tableController;

        public TestTableController()
        {
            fixture = new Fixture();
            tableInterface = new Mock<ITableInterface>();
            tableController=new TableController(tableInterface.Object);
            
            
        }
        [Fact]
        public void GetAllTableNames_ReturnsOkResultWithTableNames()
        {
            // Arrange
            List<TableInfo> tableData = fixture.CreateMany<TableInfo>().ToList();

            tableInterface.Setup(c => c.GetTableNames()).Returns(tableData);

            // Act
            var result =  tableController.GetAllTableNames();

            // Assert
            
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);
            var tableNames = okResult.Value as List<TableInfo>;
            Assert.Equal(3, tableNames.Count); 

           
        }

        [Fact]
        public void GetAllTableNames_ReturnsBadRequestOnError()
        {
            // Arrange

            tableInterface.Setup(c => c.GetTableNames()).Throws(new Exception("Something went wrong"));

           
            // Act
            var result =  tableController.GetAllTableNames();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("Something went wrong", badRequestResult.Value);
        }
    }
}

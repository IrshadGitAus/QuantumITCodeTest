using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuantumITApp.API.Controllers;
using QuantumITApp.Core.Models;
using QuantumITApp.Core.Repositories;
using QuantumITApp.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuantumITApp.API.Test
{
    [TestClass]
    public class StudentsControllerTest
    {

        Mock<IStudentRepository> _mockStudentRepository;
        Mock<IQuantumITAppService> _mockService;
        Mock<IMapper> _mockMapper;
        [TestInitialize]
        public void Initialize()
        {
            _mockStudentRepository = new Mock<IStudentRepository>();
            _mockService = new Mock<IQuantumITAppService>();
            _mockMapper = new Mock<IMapper>();
        }
        [TestMethod]
        public void Should_Get_All_Students()
        {
            //Arrange 
            IEnumerable<StudentModel> students = new List<StudentModel>() {
             new StudentModel(){    Age =6, FirstName="Mike", GPA=1.04 , Id =1 , SurName="Hazle"},
             new StudentModel(){  Age =8, FirstName="Steph", GPA=3.04 , Id =2 , SurName="Viel"} };
            _mockService.Setup(t => t.GetAllStudentAsync()).Returns(Task.FromResult(students));
            
            //Act
            var studentController = new StudentsController(_mockService.Object);
            var studentsResult = studentController.GetStudents().Result as OkObjectResult;
            var actualResult = studentsResult.Value as IEnumerable<StudentModel>;

            //Assert
            actualResult.Should().HaveCount(2, "All students must be searched");
        }

        [TestMethod]
        public void Should_Get_Student_By_Id()
        {
            //Arrange 

            var student = new StudentModel() { Age = 6, FirstName = "Mike", GPA = 1.04, Id = 1, SurName = "Hazle" };
          
            _mockService.Setup(t => t.GetStudentByIDAsync(It.IsAny<int>())).Returns(Task.FromResult(student));

            //Act
            var studentController = new StudentsController(_mockService.Object);
            var studentsResult = studentController.GetStudent(1).Result as OkObjectResult;
            var actualResult = studentsResult.Value as StudentModel;

            //Assert
            actualResult.Id.Should().Be(1, "The student with ID should be searched");
        }
    }
}

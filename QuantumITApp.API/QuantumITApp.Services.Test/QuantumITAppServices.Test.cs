using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuantumITApp.Core.Entities;
using QuantumITApp.Core.Models;
using QuantumITApp.Core.Repositories;
using QuantumITApp.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuantumITApp.Services.Test
{
    [TestClass]
    public class QuantumITAppServiceTests
    {
        Mock<IStudentRepository> _mockStudentRepository;
        Mock<ISubjectRepository> _mockSubjectRepository;
        Mock<IMapper> _mockMapper;
        [TestInitialize]
        public void Initialize()
        {
            _mockStudentRepository = new Mock<IStudentRepository>();
            _mockSubjectRepository = new Mock<ISubjectRepository>();
            _mockMapper = new Mock<IMapper>();
        }
        [TestMethod]
        public void Should_Be_Valid_When_Surnames_Are_different()
        {
            //Arrange
            IEnumerable<Student> students = new List<Student>() {
             new Student(){  Age =6, FirstName="Mike", GPA=1.04 , Id =1 , SurName="Hazle"},
             new Student(){  Age =8, FirstName="Steph", GPA=3.04 , Id =2 , SurName="Viel"} };
            _mockStudentRepository.Setup(t => t.GetStudentsBySubjectIdAsync(It.IsAny<int>())).Returns(Task.FromResult(students));
            var inputModel = new StudentAddModel() { Age = 5, SurName = "King", FirstName = "James", GPA = 5.7, SubjectId = 12 };

            //Act
            var quantumITAppService = new QuantumITAppService(_mockSubjectRepository.Object, _mockStudentRepository.Object, _mockMapper.Object);
            var actualResult = quantumITAppService.ValidStudent(inputModel);

            //Assert
            actualResult.Result.Should().BeTrue();

        }
        [TestMethod]
        public void Should_Be_Invalid_When_Surnames_Are_Same()
        {
            //Arrange
            IEnumerable<Student> students = new List<Student>() {
             new Student(){  Age =6, FirstName="Mike", GPA=1.04 , Id =1 , SurName="Hazle"},
             new Student(){  Age =8, FirstName="Steph", GPA=3.04 , Id =2 , SurName="Viel"} };
            _mockStudentRepository.Setup(t => t.GetStudentsBySubjectIdAsync(It.IsAny<int>())).Returns(Task.FromResult(students));
            var inputModel = new StudentAddModel() { Age = 8, FirstName = "Steph", GPA = 3.04, SurName = "Viel", SubjectId = 12 };

            //Act
            var quantumITAppService = new QuantumITAppService(_mockSubjectRepository.Object, _mockStudentRepository.Object, _mockMapper.Object);
            var actualResult = quantumITAppService.ValidStudent(inputModel);

            //Assert
            actualResult.Result.Should().BeFalse();
        }
    }
}

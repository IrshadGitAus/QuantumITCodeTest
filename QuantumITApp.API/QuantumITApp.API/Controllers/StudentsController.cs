using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuantumITApp.Core.Entities;
using QuantumITApp.Core.Models;
using QuantumITApp.Core.Repositories;
using QuantumITApp.Core.Services;

namespace QuantumITApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IQuantumITAppService _quantumITAppService;
        private string _error;

        public StudentsController(IQuantumITAppService quantumITAppService)
        {
            _quantumITAppService = quantumITAppService;
            _error = "Sorry!! Student with this Last Name already exists in this class.";
        }

        // GET: api/Students
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _quantumITAppService.GetAllStudentAsync();
            return Ok(students);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _quantumITAppService.GetStudentByIDAsync(id);
            return Ok(student);
        }

        // POST: api/Subjects
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentAddModel studentToAdd)
        {
            var student = await _quantumITAppService.AddStudentAsync(studentToAdd);
            if (!student)
                return BadRequest(_error);
            return Ok(student);

        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentEditModel studentForUpdate)
        {
            var student = await _quantumITAppService.UpdateStudentAsync(id, studentForUpdate);
            if (!student)
                return BadRequest(_error);
            return Ok(student);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _quantumITAppService.DeleteStudentAsync(id);
            return Ok(student);
        }
    }
}
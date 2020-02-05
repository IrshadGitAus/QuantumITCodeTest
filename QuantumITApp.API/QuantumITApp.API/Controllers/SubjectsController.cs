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
    public class SubjectsController : ControllerBase
    {
        private readonly IQuantumITAppService _quantumITAppService;

        public SubjectsController(IQuantumITAppService quantumITAppService)
        {
            _quantumITAppService = quantumITAppService;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _quantumITAppService.GetAllSubjectAsync();
            return Ok(subjects);
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var subject = await _quantumITAppService.GetSubjectByIDAsync(id);
            return Ok(subject);
        }

        // GET: api/Subjects/5/students
        [HttpGet("{id}/students")]
        public async Task<IActionResult> GetSubjectStudents(int id)
        {
            var subjectStudents = await _quantumITAppService.GetSubjectStudentsAsync(id);
            return Ok(subjectStudents);
        }

        // POST: api/Subjects
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] SubjectEditModel subjectForUpdate)
        {
            var subject = await _quantumITAppService.AddSubjectAsync(subjectForUpdate);
            if (subject == null)
                return BadRequest("Subject with this name already exists!");
            return Ok(subject);


        }


        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectEditModel subjectForUpdate)
        {
            var subject = await _quantumITAppService.UpdateSubjectAsync(id, subjectForUpdate);
            if (!subject)
                return BadRequest("Subject with this name already exists!");
            return Ok(subject);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _quantumITAppService.DeleteSubjectAsync(id);
            return Ok(subject);
        }
    }
}

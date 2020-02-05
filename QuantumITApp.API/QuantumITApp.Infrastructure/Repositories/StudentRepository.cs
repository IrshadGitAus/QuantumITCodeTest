using Microsoft.EntityFrameworkCore;
using QuantumITApp.Core.Entities;
using QuantumITApp.Core.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumITApp.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly QuantumITDataContext _context;

        public StudentRepository(QuantumITDataContext context)
        {
            _context = context;
        }

        public async Task<Student> AddAsync(Student newStudent)
        {
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
            return newStudent;
        }

        public async Task<bool> AddSubjectStudentAsync(Student newStudent, int subjectId)
        {
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();

            var newSubjectStudent = new SubjectStudent { StudentId = newStudent.Id, SubjectId = subjectId };

            _context.SubjectStudent.Add(newSubjectStudent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetStudentsBySubjectIdAsync(int subjectId)
        {
            //st - student, ss - subjectstudent
            var students = from st in _context.Students
                   join ss in _context.SubjectStudent on st.Id equals ss.StudentId
                   where ss.SubjectId == subjectId
                   orderby st.FirstName
                   select st;

            return await students.ToListAsync<Student>();
        }

        public async Task<bool> UpdateAsync(int id, Student student)
        {
            if (!await StudentExists(id))
                return false;

            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            
            if (!await StudentExists(id))
                return false;

            // first, delete from SubjectStudent table
            var toRemoveSubjectStudents = _context.SubjectStudent.Where(x => x.StudentId == id);
            foreach (var subjectstudent in toRemoveSubjectStudents)
            {
                _context.SubjectStudent.Remove(subjectstudent);
            }

            var toRemove = _context.Students.Find(id);
            _context.Students.Remove(toRemove);

            await _context.SaveChangesAsync();
            return true;

        }

        private async Task<bool> StudentExists(int id)
        {
            return await _context.Students.AnyAsync(c => c.Id == id);
        }

    }
}

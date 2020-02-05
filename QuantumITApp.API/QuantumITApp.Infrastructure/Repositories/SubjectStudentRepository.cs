using Microsoft.EntityFrameworkCore;
using QuantumITApp.Core.Entities;
using QuantumITApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumITApp.Infrastructure.Repositories
{
    public class SubjectStudentRepository : ISubjectStudentRepository
    {
        private readonly QuantumITDataContext _context;

        public SubjectStudentRepository(QuantumITDataContext context)
        {
            _context = context;
        }

        public async Task<SubjectStudent> AddAsync(SubjectStudent newSubjectStudent)
        {
            _context.SubjectStudent.Add(newSubjectStudent);
            await _context.SaveChangesAsync();
            return newSubjectStudent;
        }

        public async Task<IEnumerable<SubjectStudent>> GetAllAsync()
        {
            return await _context.SubjectStudent.ToListAsync();
        }

        public async Task<IEnumerable<SubjectStudent>> GetByStudentIdAsync(int id)
        {
            return await _context.SubjectStudent.Where(a => a.StudentId == id).ToListAsync();
        }

        public async Task<IEnumerable<SubjectStudent>> GetBySubjectIdAsync(int id)
        {
            return await _context.SubjectStudent.Where(a => a.SubjectId == id).ToListAsync();
        }

        public async Task<bool> UpdateAsync(SubjectStudent subjectStudent)
        {
            if (!await SubjectStudentExists(subjectStudent.Id))
                return false;
            _context.SubjectStudent.Update(subjectStudent);
            await _context.SaveChangesAsync();
            return true;
        }
                

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await SubjectStudentExists(id))
                return false;
                       
            var toRemove = _context.SubjectStudent.Find(id);
            _context.SubjectStudent.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        
        private async Task<bool> SubjectStudentExists(int id)
        {
            return await _context.SubjectStudent.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> DeleteStudentFromSubjectAsync(int subjectId, int studentId)
        {
            // Just delete the student from the selected subject. DO NOT delete the student altogether
            var toRemoveSubjectStudents = _context.SubjectStudent.Where((x => x.StudentId == studentId));
           
            foreach (var subjectstudent in toRemoveSubjectStudents)
            {
                if(subjectstudent.SubjectId == subjectId)
                    _context.SubjectStudent.Remove(subjectstudent);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

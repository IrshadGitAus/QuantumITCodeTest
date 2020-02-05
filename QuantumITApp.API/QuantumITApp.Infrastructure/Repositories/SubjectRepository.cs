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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly QuantumITDataContext _context;

        public SubjectRepository(QuantumITDataContext context)
        {
            _context = context;
        }

        public async Task<Subject> AddAsync(Subject newSubject)
        {
            if (await SubjectNameExists(newSubject.Name))
                return null;
            _context.Subjects.Add(newSubject);
            await _context.SaveChangesAsync();
            return newSubject;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject> GetByIdAsync(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, Subject subject)
        {
            if (!await SubjectExists(id))
                return false;

            var subjectCurrent = await GetByIdAsync(id);
            if (subjectCurrent.Name.ToLower() == subject.Name.ToLower())
            {
                _context.Subjects.Update(subject);
                await _context.SaveChangesAsync();
                return true;

            }
            else
            {
                if (await SubjectNameExists(subject.Name))
                    return false;

                _context.Subjects.Update(subject);
                await _context.SaveChangesAsync();
                return true;
            }            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await SubjectExists(id))
                return false;

            // first, delete from SubjectStudent table
            var toRemoveSubjectStudents = _context.SubjectStudent.Where(x => x.SubjectId == id);
            foreach (var subjectstudent in toRemoveSubjectStudents)
            {
                _context.SubjectStudent.Remove(subjectstudent);
            }

            var toRemove = _context.Subjects.Find(id);
            _context.Subjects.Remove(toRemove);

            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> SubjectExists(int id)
        {
            return await _context.Subjects.AnyAsync(c => c.Id == id);
        }

        private async Task<bool> SubjectNameExists(string name)
        {
            return await _context.Subjects.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
        }

    }
}

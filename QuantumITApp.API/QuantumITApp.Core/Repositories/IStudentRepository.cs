using QuantumITApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuantumITApp.Core.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<Student> AddAsync(Student newStudent);
        Task<bool> UpdateAsync(int id, Student student);
        Task<bool> DeleteAsync(int id);
        Task<bool> AddSubjectStudentAsync(Student newStudent, int subjectId);
        Task<IEnumerable<Student>> GetStudentsBySubjectIdAsync(int subjectId);
    }
}

using QuantumITApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuantumITApp.Core.Repositories
{
    public interface ISubjectStudentRepository
    {
        Task<IEnumerable<SubjectStudent>> GetAllAsync();
        Task<IEnumerable<SubjectStudent>> GetByStudentIdAsync(int id);
        Task<IEnumerable<SubjectStudent>> GetBySubjectIdAsync(int id);
        Task<SubjectStudent> AddAsync(SubjectStudent newSubjectStudent);
        Task<bool> UpdateAsync(SubjectStudent subjectStudent);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteStudentFromSubjectAsync(int subjectId, int studentId);

    }
}

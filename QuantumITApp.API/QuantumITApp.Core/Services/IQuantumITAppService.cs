using QuantumITApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuantumITApp.Core.Services
{
    public interface IQuantumITAppService
    {
        Task<IEnumerable<SubjectModel>> GetAllSubjectAsync();
        Task<SubjectModel> GetSubjectByIDAsync(int id);
        Task<SubjectModel> AddSubjectAsync(SubjectEditModel subject);
        Task<bool> UpdateSubjectAsync(int id, SubjectEditModel subject);
        Task<bool> DeleteSubjectAsync(int id);

        //-----------------------

        Task<IEnumerable<StudentModel>> GetAllStudentAsync();
        Task<StudentModel> GetStudentByIDAsync(int id);
        Task<bool> AddStudentAsync(StudentAddModel subject);
        Task<bool> UpdateStudentAsync(int id, StudentEditModel subject);
        Task<bool> DeleteStudentAsync(int id);
        Task<SubjectStudentModel> GetSubjectStudentsAsync(int subjectId);
    }
}

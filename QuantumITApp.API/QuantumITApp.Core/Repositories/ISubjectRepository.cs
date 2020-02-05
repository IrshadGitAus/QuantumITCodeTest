using QuantumITApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuantumITApp.Core.Repositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<Subject> GetByIdAsync(int id);
        Task<Subject> AddAsync(Subject newSubject);
        Task<bool> UpdateAsync(int id, Subject subject);
        Task<bool> DeleteAsync(int id);
    }
}

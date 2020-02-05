using AutoMapper;
using QuantumITApp.Core.Entities;
using QuantumITApp.Core.Models;
using QuantumITApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumITApp.Core.Services
{
    public class QuantumITAppService : IQuantumITAppService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public QuantumITAppService(ISubjectRepository subjectRepository, IStudentRepository studentRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
               

        public async Task<SubjectModel> AddSubjectAsync(SubjectEditModel subjectForUpdate)
        {
            var subject = _mapper.Map<Subject>(subjectForUpdate);

            subject = await _subjectRepository.AddAsync(subject);

            return _mapper.Map<SubjectModel>(subject);
        }

        public async Task<IEnumerable<SubjectModel>> GetAllSubjectAsync()
        {
            var subjects = await _subjectRepository.GetAllAsync();

            var subjectsToReturn = _mapper.Map<IEnumerable<SubjectModel>>(subjects);

            return subjectsToReturn;
        }

        public async Task<SubjectModel> GetSubjectByIDAsync(int id)
        {           
            var subject = await _subjectRepository.GetByIdAsync(id);

            var subjectToReturn = _mapper.Map<SubjectModel>(subject);

            return subjectToReturn;
        }

        public async Task<bool> UpdateSubjectAsync(int id, SubjectEditModel subjectForUpdate)
        {
            //var subject = await _subjectRepository.GetByIdAsync(id);

            var subject = _mapper.Map<Subject>(subjectForUpdate);

            return await _subjectRepository.UpdateAsync(id, subject);     
        }


        public async Task<bool> DeleteSubjectAsync(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);

            if (subject != null)
            {
                return await _subjectRepository.DeleteAsync(id);                    
            }

            return false;
        }

        public async Task<IEnumerable<StudentModel>> GetAllStudentAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            var studentsToReturn = _mapper.Map<IEnumerable<StudentModel>>(students);

            return studentsToReturn;
        }

        public async Task<StudentModel> GetStudentByIDAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            var studentToReturn = _mapper.Map<StudentModel>(student);

            return studentToReturn;
        }

        public async Task<bool> AddStudentAsync(StudentAddModel studentToAdd)
        {
            var student = _mapper.Map<Student>(studentToAdd);

            if(await ValidStudent(studentToAdd))
                return await _studentRepository.AddSubjectStudentAsync(student, studentToAdd.SubjectId);

            return false;
        }

        public async Task<bool> ValidStudent(StudentAddModel studentToAdd)
        {
            var students = await _studentRepository.GetStudentsBySubjectIdAsync(studentToAdd.SubjectId);

            var studentFound = students.FirstOrDefault(st => st.SurName.ToLower().Trim() == studentToAdd.SurName.ToLower().Trim());
            if (studentFound == null)
                return true;
            return false;
        }

       
        public async Task<bool> UpdateStudentAsync(int id, StudentEditModel studentForUpdate)
        {
           //var student = await _studentRepository.GetByIdAsync(id);

            var subjectId = studentForUpdate.SubjectId;
           
            if (await ValidStudentEdit(id, studentForUpdate))
            {
                var student = _mapper.Map<Student>(studentForUpdate);
                return await _studentRepository.UpdateAsync(id, student);
            }
               

            return false;
        }

        public async Task<bool> ValidStudentEdit(int id, StudentEditModel studentToUpdate)
        {
           var students = await _studentRepository.GetStudentsBySubjectIdAsync(studentToUpdate.SubjectId);

            var studentsToCheck = students.SkipWhile(st => st.Id == id);

           var studentFound = studentsToCheck.FirstOrDefault(st => st.SurName.ToLower().Trim() == studentToUpdate.SurName.ToLower().Trim());
           
           if (studentFound == null)
                return true;
            return false;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student != null)
            {
                return await _studentRepository.DeleteAsync(id);
            }

            return false;
        }


        public async Task<SubjectStudentModel> GetSubjectStudentsAsync(int subjectId)
        {
            var subject = await _subjectRepository.GetByIdAsync(subjectId);

            var students = await _studentRepository.GetStudentsBySubjectIdAsync(subjectId);

            var studentsToReturn = _mapper.Map<IEnumerable<StudentModel>>(students);

            var subjectStudent = new SubjectStudentModel
            {
                SubjectId = subject.Id,
                SubjectName = subject.Name,
                Students = studentsToReturn,
            };

            return subjectStudent;           

        }

    }
}

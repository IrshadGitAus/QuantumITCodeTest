using AutoMapper;
using QuantumITApp.Core.Entities;
using QuantumITApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumITApp.Core.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SubjectEditModel, Subject>();
            CreateMap<Subject, SubjectEditModel>();

            CreateMap<SubjectModel, Subject>();
            CreateMap<Subject, SubjectModel>();

            CreateMap<StudentEditModel, Student>();
            CreateMap<Student, StudentEditModel>();

            CreateMap<StudentAddModel, Student>();
            CreateMap<Student, StudentAddModel>();

            CreateMap<Student, StudentModel>();

            CreateMap<StudentEditModel, StudentAddModel>();
        }           
            
    }
}

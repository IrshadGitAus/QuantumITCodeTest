using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumITApp.Core.Models
{
    public class StudentEditModel
    {
        public int SubjectId { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
        public double GPA { get; set; }
    }
}

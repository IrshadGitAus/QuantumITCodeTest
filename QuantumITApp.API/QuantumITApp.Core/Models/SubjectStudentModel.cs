using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumITApp.Core.Models
{
    public class SubjectStudentModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public IEnumerable<StudentModel> Students { get; set; }
    }
}

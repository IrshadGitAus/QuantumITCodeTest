using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuantumITApp.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        [Required]
        public string SurName { get; set; }

        public int Age { get; set; }
        public double GPA { get; set; }
    }
}

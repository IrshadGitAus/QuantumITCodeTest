using Microsoft.EntityFrameworkCore;
using QuantumITApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumITApp.Infrastructure
{
    public class QuantumITDataContext : DbContext
    {
        public QuantumITDataContext(DbContextOptions<QuantumITDataContext> options) : base(options)
        {

        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SubjectStudent> SubjectStudent { get; set; }
    }
}

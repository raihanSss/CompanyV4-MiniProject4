using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyV4.Domain.Models;

namespace CompanyV4.Infrastructure
{
    public partial class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
        {

        }

        public DbSet<Department> Departements { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<WorksOn> Worksons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorksOn>()
                .HasKey(w => new { w.EmpNo, w.ProjNo, w.DateWorked });

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithOne()
                .HasForeignKey<Department>(d => d.MgrEmpNo);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DeptNo);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Department)
                .WithMany(d => d.Projects)
                .HasForeignKey(p => p.DeptNo);

            modelBuilder.Entity<WorksOn>()
                .HasOne(w => w.Employee)
                .WithMany(e => e.WorksOns)
                .HasForeignKey(w => w.EmpNo);

            modelBuilder.Entity<WorksOn>()
                .HasOne(w => w.Project)
                .WithMany(p => p.WorksOns)
                .HasForeignKey(w => w.ProjNo);
        }
    }

}


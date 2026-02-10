using Microsoft.EntityFrameworkCore;
using Montcrest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montcrest.DAL.Context
{
    public class MontcrestDbContext:DbContext
    {
        public MontcrestDbContext(DbContextOptions<MontcrestDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Technology> Technologies { get; set; }

        public DbSet<JobApplication> JobApplications { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobApplication>()
                .HasOne(j => j.User)
                .WithMany(u => u.JobApplications)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobApplication>()
                .HasOne(j => j.Technology)
                .WithMany(t => t.JobApplications)
                .HasForeignKey(t => t.TechnologyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
    .HasOne(e => e.User)
    .WithMany()
    .HasForeignKey(e => e.UserId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Manager>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany(m => m.Employees)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveApplication>()
                .HasOne(l => l.Employee)
                .WithMany(e => e.LeaveApplications)
                .HasForeignKey(l => l.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveApplication>()
                .HasOne(l => l.Manager)
                .WithMany(m => m.LeaveApplications)
                .HasForeignKey(l => l.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<int>();

            modelBuilder.Entity<JobApplication>()
                .Property(j => j.Status)
                .HasConversion<int>();

            modelBuilder.Entity<JobApplication>()
                .Property(e=>e.ExamResult)
                .HasConversion<int>();


            modelBuilder.Entity<Technology>().HasData(
                new Technology { Id = 1, Name = ".NET" },
                new Technology { Id = 2, Name = "Java" },
                new Technology { Id = 3, Name = "Python" },
                new Technology { Id = 4, Name = "React" },
                new Technology { Id = 5, Name = "Angular" }
                );

            

        }
    }
}

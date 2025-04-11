using EduTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.DataAccess.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
      

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                  .HasOne(s => s.Teacher)
                  .WithMany(t => t.Students)
                  .HasForeignKey(s => s.TeacherId)
                  .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Student>()
                 .HasOne(s => s.Group)
                 .WithMany(t => t.Students)
                 .HasForeignKey(s => s.GroupId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Test>()
                .HasOne(s => s.Student)
                .WithMany(t=>t.Tests)
                .HasForeignKey(s=>s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Group>()
                .HasOne(t=>t.Teacher)
                .WithMany(t => t.Groups)
                .HasForeignKey(t=>t.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TestResult>()
               .HasOne(tr => tr.Student)  
               .WithMany(s => s.TestResults)  
               .HasForeignKey(tr => tr.StudentId) 
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TestResult>()
               .HasOne(tr => tr.Test)  
               .WithMany(t => t.TestResults)  
               .HasForeignKey(tr => tr.TestId) 
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Test>()
               .HasOne(t => t.Teacher) 
               .WithMany(tch => tch.Tests)  
               .HasForeignKey(t => t.TeacherId)  
               .OnDelete(DeleteBehavior.Cascade);


        }


    }
}

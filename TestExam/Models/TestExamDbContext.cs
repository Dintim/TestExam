using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestExam.Models
{
    public class TestExamDbContext:DbContext
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().HasMany(p => p.Questions).WithRequired(p => p.Test).HasForeignKey(p => p.TestId);
            modelBuilder.Entity<Question>().HasMany(p => p.Answers).WithRequired(p => p.Question).HasForeignKey(p => p.QuestionId);
            modelBuilder.Entity<Test>().HasMany(p => p.Registrations).WithRequired(p => p.Test).HasForeignKey(p => p.TestsId);
            modelBuilder.Entity<User>().HasMany(p => p.Registrations).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
            base.OnModelCreating(modelBuilder);
        }

        static TestExamDbContext()
        {
            Database.SetInitializer<TestExamDbContext>(new TestExamDbContextInitializer());
        }

        public TestExamDbContext():base("TestExamConnectionString")
        {
        }
    }
}
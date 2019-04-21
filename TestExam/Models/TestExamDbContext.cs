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
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().HasMany(p => p.Questions).WithRequired(p => p.Test).HasForeignKey(p => p.TestId);
            modelBuilder.Entity<Question>().HasMany(p => p.Answers).WithRequired(p => p.Question).HasForeignKey(p => p.QuestionId);
            modelBuilder.Entity<Test>().HasMany(p => p.Registrations).WithRequired(p => p.Test).HasForeignKey(p => p.TestsId);
            modelBuilder.Entity<User>().HasMany(p => p.Registrations).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Answer>().HasMany(p => p.Results).WithRequired(p => p.Answer).HasForeignKey(p => p.AnswerId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Registration>().HasMany(p => p.Results).WithRequired(p => p.Registration).HasForeignKey(p => p.RegistrationId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Question>().HasMany(p => p.Results).WithRequired(p => p.Question).HasForeignKey(p => p.QuestionId).WillCascadeOnDelete(false);
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
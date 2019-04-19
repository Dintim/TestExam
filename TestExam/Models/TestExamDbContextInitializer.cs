using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestExam.Models
{
    public class TestExamDbContextInitializer: DropCreateDatabaseAlways<TestExamDbContext>
    {
        protected override void Seed(TestExamDbContext dbContext)
        {
            Test test1 = new Test { TestName = "ООП на С++" };
            Test test2 = new Test { TestName = "Работа с Entity Framework" };

            dbContext.Tests.Add(test1);
            dbContext.Tests.Add(test2);
            dbContext.SaveChanges();
        }
    }
}
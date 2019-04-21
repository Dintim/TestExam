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
            Test test1 = new Test { TestName = "Общие вопросы по C#" };
            dbContext.Tests.Add(test1);
            Test test2 = new Test { TestName = "ADO.NET и Entity Framework" };
            dbContext.Tests.Add(test2);
            dbContext.SaveChanges();

            Question question1 = new Question { QuestionNumber = 1, QuestionText = "Чему будет равен с, если int a = 0; int c = a— ?", TestId = 1 };
            dbContext.Questions.Add(question1);
            dbContext.SaveChanges();
            List<Answer> answers1 = new List<Answer>()
            {
                new Answer { AnswerText = "Null", IsCorrect = false, QuestionId = 1 },
                new Answer { AnswerText = "-1", IsCorrect = false, QuestionId = 1 },
                new Answer { AnswerText = "0", IsCorrect = true, QuestionId = 1 },
                new Answer { AnswerText = "1", IsCorrect = false, QuestionId = 1 }
            };
            dbContext.Answers.AddRange(answers1);
            dbContext.SaveChanges();
            

            Question question2 = new Question { QuestionNumber = 2, QuestionText = "Чему будет равен с, если int a = 0; int c = —a ?", TestId = 1 };
            dbContext.Questions.Add(question2);
            dbContext.SaveChanges();
            List<Answer> answers2 = new List<Answer>()
            {
                new Answer { AnswerText = "Null", IsCorrect = false, QuestionId = 2 },
                new Answer { AnswerText = "-1", IsCorrect = true, QuestionId = 2 },
                new Answer { AnswerText = "0", IsCorrect = false, QuestionId = 2 },
                new Answer { AnswerText = "1", IsCorrect = false, QuestionId = 2 }
            };
            dbContext.Answers.AddRange(answers2);
            dbContext.SaveChanges();
            

            Question question3 = new Question { QuestionNumber = 3, QuestionText = "Как называется оператор «?:» ?", TestId = 1 };
            dbContext.Questions.Add(question3);
            dbContext.SaveChanges();
            List<Answer> answers3 = new List<Answer>()
            {
                new Answer { AnswerText = "Вопросительный", IsCorrect = false, QuestionId = 3 },
                new Answer { AnswerText = "Прямой оператор", IsCorrect = false, QuestionId = 3 },
                new Answer { AnswerText = "Тернарный оператор", IsCorrect = true, QuestionId = 3 }
            };
            dbContext.Answers.AddRange(answers3);
            dbContext.SaveChanges();
            

            Question question4 = new Question { QuestionNumber = 4, QuestionText = "Что такое массив?", TestId = 1 };
            dbContext.Questions.Add(question4);
            dbContext.SaveChanges();
            List<Answer> answers4 = new List<Answer>()
            {
                new Answer { AnswerText = "Набор однотипных данных, которые располагаются в памяти последовательно друг за другом", IsCorrect = true, QuestionId = 4 },
                new Answer { AnswerText = "Набор текстовых значений в формате Unicode, которые расположены в случайном порядке", IsCorrect = false, QuestionId = 4 },
                new Answer { AnswerText = "Набор данных типа int (32-бит целое)", IsCorrect = false, QuestionId = 4 },
                new Answer { AnswerText = "Переменная", IsCorrect = false, QuestionId = 4 }
            };
            dbContext.Answers.AddRange(answers4);
            dbContext.SaveChanges();
            

            Question question5 = new Question { QuestionNumber = 5, QuestionText = "Что такое Куча?", TestId = 1 };
            dbContext.Questions.Add(question5);
            dbContext.SaveChanges();
            List<Answer> answers5 = new List<Answer>()
            {
                new Answer { AnswerText = "Структура данных", IsCorrect = false, QuestionId = 5 },
                new Answer { AnswerText = "Именованная область памяти", IsCorrect = false, QuestionId = 5 },
                new Answer { AnswerText = "Область динамической памяти", IsCorrect = true, QuestionId = 5 },
                new Answer { AnswerText = "Куча переменных", IsCorrect = false, QuestionId = 5 }
            };
            dbContext.Answers.AddRange(answers5);
            dbContext.SaveChanges();            


            Question question6 = new Question { QuestionNumber = 1, QuestionText = "Какой объект используется в ADO.NET для подключения в базе данных?", TestId = 2 };
            dbContext.Questions.Add(question6);
            dbContext.SaveChanges();
            List<Answer> answers6 = new List<Answer>()
            {
                new Answer { AnswerText = "SqlConnection", IsCorrect = true, QuestionId = 6 },
                new Answer { AnswerText = "ConnectWithDB", IsCorrect = false, QuestionId = 6 },
                new Answer { AnswerText = "Connection.Open", IsCorrect = false, QuestionId = 6 },
                new Answer { AnswerText = "SqlCommand", IsCorrect = false, QuestionId = 6 }
            };
            dbContext.Answers.AddRange(answers6);
            dbContext.SaveChanges();
            

            Question question7 = new Question { QuestionNumber = 2, QuestionText = "Какой из методов SqlCommand может добавить объект в таблицу?", TestId = 2 };
            dbContext.Questions.Add(question7);
            dbContext.SaveChanges();
            List<Answer> answers7 = new List<Answer>()
            {
                new Answer { AnswerText = "ExecuteReader", IsCorrect = false, QuestionId = 7 },
                new Answer { AnswerText = "ExecuteNonQuery", IsCorrect = true, QuestionId = 7 },
                new Answer { AnswerText = "ExecuteScalar", IsCorrect = false, QuestionId = 7 }
            };
            dbContext.Answers.AddRange(answers7);
            dbContext.SaveChanges();
            

            Question question8 = new Question { QuestionNumber = 3, QuestionText = "Что означает Code First в рамках работы в Entity Framework?", TestId = 2 };
            dbContext.Questions.Add(question8);
            dbContext.SaveChanges();
            List<Answer> answers8 = new List<Answer>()
            {
                new Answer { AnswerText = "Сначала пишется код, а потом по нему создается база данных", IsCorrect = true, QuestionId = 8 },
                new Answer { AnswerText = "Сначала делается модель, а потом по ней создается база данных", IsCorrect = false, QuestionId = 8 },
                new Answer { AnswerText = "Разработчик пишет приложение для уже существующей базы данных", IsCorrect = false, QuestionId = 8 }
            };
            dbContext.Answers.AddRange(answers8);
            dbContext.SaveChanges();
            

            Question question9 = new Question { QuestionNumber = 4, QuestionText = "Что позволяют делать миграции?", TestId = 2 };
            dbContext.Questions.Add(question9);
            dbContext.SaveChanges();
            List<Answer> answers9 = new List<Answer>()
            {
                new Answer { AnswerText = "Добавлять приложение в удаленный репозиторий", IsCorrect = false, QuestionId = 9 },
                new Answer { AnswerText = "Пересылать данные другим разработчикам", IsCorrect = false, QuestionId = 9 },
                new Answer { AnswerText = "Вносить изменения в базу данных при изменениях моделей и контекста данных", IsCorrect = true, QuestionId = 9 }
            };
            dbContext.Answers.AddRange(answers9);
            dbContext.SaveChanges();
            

            Question question10 = new Question { QuestionNumber = 5, QuestionText = "Какой из методов LINQ to Entities является агрегатной операцией?", TestId = 2 };
            dbContext.Questions.Add(question10);
            dbContext.SaveChanges();
            List<Answer> answers10 = new List<Answer>()
            {
                new Answer { AnswerText = "OrderBy", IsCorrect = false, QuestionId = 10 },
                new Answer { AnswerText = "Average", IsCorrect = true, QuestionId = 10 },
                new Answer { AnswerText = "Join", IsCorrect = false, QuestionId = 10 },
                new Answer { AnswerText = "ThenBy", IsCorrect = false, QuestionId = 10 }
            };
            dbContext.Answers.AddRange(answers10);
            dbContext.SaveChanges();            


            
        }
    }
}
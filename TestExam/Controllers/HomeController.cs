using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestExam.Models;
using TestExam.ViewModels;

namespace TestExam.Controllers
{
    public class HomeController : Controller
    {
        TestExamDbContext _dbContext = new TestExamDbContext();

        public ActionResult Index()
        {
            //Initial();
            ViewBag.Tests = _dbContext.Tests.Select(p => new { p.Id, p.TestName }).ToList();

            SessionViewModel _model = null;
            if (Session["SessionViewModel"] == null)
                _model = new SessionViewModel();
            else
                _model = (SessionViewModel)Session["SessionViewModel"];

            return View(_model);

            //var tests = _dbContext.Tests;
            //ViewBag.Tests = tests;
            //return View();
        }

        //[HttpGet]
        public ActionResult PassTest(int testId)
        {
            var test = _dbContext.Tests.SingleOrDefault(p => p.Id == testId);
            //ViewBag.testName = test.TestName;
            return View(test);

            //if (testId == null)
            //    return View("Error");
            //else
            //{
            //    var test = _dbContext.Tests.SingleOrDefault(p => p.Id == testId);
            //    ViewBag.testName = test.TestName;
            //    return View();
            //}             
        }

        //[HttpPost]
        //public ActionResult PassTest(Test test)
        //{

        //    return View();
        //}

        public ActionResult Instruction(SessionViewModel model)
        {
            if (model != null)
            {                
                _dbContext.Database.CreateIfNotExists();

                var test = _dbContext.Tests.Where(p => p.Id == model.TestId).FirstOrDefault();
                var testQuestions = _dbContext.Questions.Where(p => p.TestId == model.TestId).ToList();
                if (test!=null)
                {
                    ViewBag.TestName = test.TestName;
                    ViewBag.QuestionCount = testQuestions.Count;
                }
            }
            return View();
        }

        public ActionResult Register(SessionViewModel model)
        {
            if (model != null)
                Session["SessionViewModel"] = model;            

            if (model==null || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Email) || model.TestId < 1)
            {
                TempData["message"] = "Неправильно внесены данные при регистрации. Попробуйте снова";
                return RedirectToAction("Index");
            }

            var context = new TestExamDbContext();
            User user = context.Users.SingleOrDefault(p => p.UserName == model.UserName && p.Email == model.Email);
            if (user==null)
            {
                user = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                context.Users.Add(user);
                context.SaveChanges();
            }

            Registration registration = context.Registrations.SingleOrDefault(p => p.UserId == user.Id && p.TestsId == model.TestId);
            if (registration!=null)
            {
                this.Session["TOKEN"] = registration.Token;
            }

            Test test = context.Tests.SingleOrDefault(p => p.Id == model.TestId);
            if (test != null)
            {
                Registration newRegistration = new Registration()
                {
                    Token = Guid.NewGuid(),
                    UserId = user.Id,
                    TestsId = model.TestId
                };
                context.Registrations.Add(newRegistration);
                context.SaveChanges();
                this.Session["TOKEN"] = newRegistration.Token;
            }

            return RedirectToAction("EvalPage", new {@token=Session["TOKEN"] });
        }


        public ActionResult EvalPage(Guid ? token, int? quesNum)
        {
            if (token == null)
            {
                TempData["message"] = "Неверный токен, ошибка регистрации. Попробуйте снова";
                return RedirectToAction("Index");
            }

            if (quesNum.GetValueOrDefault() < 1)
                quesNum = 1;

            var context = new TestExamDbContext();
            var registration = context.Registrations.SingleOrDefault(p => p.Token == token);
            if (registration == null)
            {
                TempData["message"] = "Неверный токен, ошибка регистрации. Попробуйте снова";
                return RedirectToAction("Index");
            }

            var testQuestionId = context.Questions
                .Where(p => p.TestId == registration.TestsId && p.QuestionNumber == quesNum).Select(p => p.Id).FirstOrDefault();
            if (testQuestionId > 0)
            {
                var model = context.Questions.Where(p => p.Id == testQuestionId).Select(p=>new QuestionViewModel()
                {
                    QuestionNumber=p.QuestionNumber,
                    QuestionText=p.QuestionText,
                    TestId=p.TestId,
                    TestName=p.Test.TestName,
                    Options=p.Answers.Select(x=> new AnswerViewModel()
                    {
                        AnswerId=x.Id,
                        AnswerText=x.AnswerText,
                        IsCorrect=x.IsCorrect
                    }).ToList()
                }).FirstOrDefault();

                model.TotalQuestionInSet = context.Questions.Where(p => p.TestId == registration.TestsId).Count();

                return View(model);
            }

            return View("Error");

            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        private void Initial()
        {
            TestExamDbContext dbContext = new TestExamDbContext();
            dbContext.Database.CreateIfNotExists();

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
            //Answer answer11 = new Answer { AnswerText = "Null", IsCorrect = false, QuestionId = 1 };
            //Answer answer12 = new Answer { AnswerText = "-1", IsCorrect = false, QuestionId = 1 };
            //Answer answer13 = new Answer { AnswerText = "0", IsCorrect = true, QuestionId = 1 };
            //Answer answer14 = new Answer { AnswerText = "1", IsCorrect = false, QuestionId = 1 };

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
            //Answer answer21 = new Answer { AnswerText = "Null", IsCorrect = false, QuestionId = 2 };
            //Answer answer22 = new Answer { AnswerText = "-1", IsCorrect = true, QuestionId = 2 };
            //Answer answer23 = new Answer { AnswerText = "0", IsCorrect = false, QuestionId = 2 };
            //Answer answer24 = new Answer { AnswerText = "1", IsCorrect = false, QuestionId = 2 };

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
            //Answer answer31 = new Answer { AnswerText = "Вопросительный", IsCorrect = false, QuestionId = 3 };
            //Answer answer32 = new Answer { AnswerText = "Прямой оператор", IsCorrect = false, QuestionId = 3 };
            //Answer answer33 = new Answer { AnswerText = "Тернарный оператор", IsCorrect = true, QuestionId = 3 };

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
            //Answer answer41 = new Answer { AnswerText = "Набор однотипных данных, которые располагаются в памяти последовательно друг за другом", IsCorrect = true, QuestionId = 4 };
            //Answer answer42 = new Answer { AnswerText = "Набор текстовых значений в формате Unicode, которые расположены в случайном порядке", IsCorrect = false, QuestionId = 4 };
            //Answer answer43 = new Answer { AnswerText = "Набор данных типа int (32-бит целое)", IsCorrect = false, QuestionId = 4 };
            //Answer answer44 = new Answer { AnswerText = "Переменная", IsCorrect = false, QuestionId = 4 };

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
            //Answer answer51 = new Answer { AnswerText = "Структура данных", IsCorrect = false, QuestionId = 5 };
            //Answer answer52 = new Answer { AnswerText = "Именованная область памяти", IsCorrect = false, QuestionId = 5 };
            //Answer answer53 = new Answer { AnswerText = "Область динамической памяти", IsCorrect = true, QuestionId = 5 };
            //Answer answer54 = new Answer { AnswerText = "Куча переменных", IsCorrect = false, QuestionId = 5 };


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
            //Answer answer61 = new Answer { AnswerText = "SqlConnection", IsCorrect = true, QuestionId = 6 };
            //Answer answer62 = new Answer { AnswerText = "ConnectWithDB", IsCorrect = false, QuestionId = 6 };
            //Answer answer63 = new Answer { AnswerText = "Connection.Open", IsCorrect = false, QuestionId = 6 };
            //Answer answer64 = new Answer { AnswerText = "SqlCommand", IsCorrect = false, QuestionId = 6 };

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
            //Answer answer71 = new Answer { AnswerText = "ExecuteReader", IsCorrect = false, QuestionId = 7 };
            //Answer answer72 = new Answer { AnswerText = "ExecuteNonQuery", IsCorrect = true, QuestionId = 7 };
            //Answer answer73 = new Answer { AnswerText = "ExecuteScalar", IsCorrect = false, QuestionId = 7 };

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
            //Answer answer81 = new Answer { AnswerText = "Сначала пишется код, а потом по нему создается база данных", IsCorrect = true, QuestionId = 8 };
            //Answer answer82 = new Answer { AnswerText = "Сначала делается модель, а потом по ней создается база данных", IsCorrect = false, QuestionId = 8 };
            //Answer answer83 = new Answer { AnswerText = "Разработчик пишет приложение для уже существующей базы данных", IsCorrect = false, QuestionId = 8 };

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
            //Answer answer91 = new Answer { AnswerText = "Добавлять приложение в удаленный репозиторий", IsCorrect = false, QuestionId = 9 };
            //Answer answer92 = new Answer { AnswerText = "Пересылать данные другим разработчикам", IsCorrect = false, QuestionId = 9 };
            //Answer answer93 = new Answer { AnswerText = "Вносить изменения в базу данных при изменениях моделей и контекста данных", IsCorrect = true, QuestionId = 9 };

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
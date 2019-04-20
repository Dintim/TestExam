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
            _dbContext.Database.CreateIfNotExists();
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


        public ActionResult EvalPage(SessionViewModel model,int ? quesNumber)
        {            
            _dbContext.Database.CreateIfNotExists();

            if (quesNumber < 1)
                quesNumber = 1;

            var testQuestionId = _dbContext.Questions.Where(p => p.TestId == model.TestId && p.QuestionNumber == quesNumber)
                .Select(p => p.Id).FirstOrDefault();

            if (testQuestionId > 0)
            {
                var questionModel = _dbContext.Questions.Include("Answer").Where(p => p.Id == testQuestionId).Select(p => new TestViewModel()
                {
                    QuestionNumber = p.QuestionNumber,
                    QuestionText = p.QuestionText,
                    TestId = p.TestId,
                    TestName = p.Test.TestName,
                    Answers = p.Answers.Select(x => new QXAModel()
                    {
                        AnswerId = x.Id,
                        AnswerText = x.AnswerText,
                        IsCorrect = x.IsCorrect
                    }).ToList()
                }).FirstOrDefault();

                return View(questionModel);
            }
            else
                return View("Error");
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

            if (model==null || string.IsNullOrEmpty(model.UserName) || model.TestId < 1)
            {
                TempData["message"] = "Неправильно внесены данные при регистрации. Попробуйте снова";
                return RedirectToAction("Index");
            }

            var context = new TestExamDbContext();

            return View();
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
    }
}
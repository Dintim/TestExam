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
        public ActionResult Index()
        {
            TestExamDbContext _dbContext = new TestExamDbContext();
            _dbContext.Database.CreateIfNotExists();
            ViewBag.Tests = _dbContext.Tests.Select(p => new { p.Id, p.TestName }).ToList();

            SessionViewModel _model = null;
            if (Session["SessionViewModel"] == null)
                _model = new SessionViewModel();
            else
                _model = (SessionViewModel)Session["SessionViewModel"];

            return View(_model);
        }


        public ActionResult Instraction(SessionViewModel model)
        {
            if (model != null)
            {
                TestExamDbContext _dbContext = new TestExamDbContext();
                _dbContext.Database.CreateIfNotExists();

                var test = _dbContext.Tests.Where(p => p.Id == model.TestId).FirstOrDefault();
                if (test!=null)
                {
                    ViewBag.TestName = test.TestName;
                    ViewBag.QuestionCount = test.Questions.Count;
                }
            }
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
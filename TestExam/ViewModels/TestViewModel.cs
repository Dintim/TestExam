using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestExam.ViewModels
{
    public class TestViewModel
    {
        public int TotalQuestionInSet { get; set; }
        public int QuestionNumber { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string QuestionText { get; set; }        
        public List<QXAModel> Answers { get; set; }
    }

    public class QXAModel
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
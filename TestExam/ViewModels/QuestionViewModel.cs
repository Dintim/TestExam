using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestExam.ViewModels
{
    public class QuestionViewModel
    {
        public int TotalQuestionInSet { get; set; }
        public int QuestionNumber { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string QuestionText { get; set; }        
        public List<AnswerViewModel> Options { get; set; }
        public int Result { get; set; }
    }

    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }        
    }
}
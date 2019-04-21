using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestExam.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public ICollection<Result> Results { get; set; }
    }
}
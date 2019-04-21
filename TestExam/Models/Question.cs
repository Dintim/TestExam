using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestExam.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<Result> Results { get; set; }
    }
}
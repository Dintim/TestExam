using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestExam.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int RegistrationId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int Score { get; set; }

        public Registration Registration { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}
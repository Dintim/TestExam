using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestExam.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string TestName { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
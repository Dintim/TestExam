using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestExam.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public Guid Token { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int TestsId { get; set; }
        public Test Test { get; set; }
    }
}
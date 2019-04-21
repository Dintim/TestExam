using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestExam.ViewModels
{
    public class UserChoiceViewModel
    {
        public int ChoiceId { get; set; }
        public string IsChecked { get; set; }
    }

    public class UserAnswerViewModel
    {
        public int TestId { get; set; }
        public int QuestionNum { get; set; }
        public Guid Token { get; set; }
        public UserChoiceViewModel UserChoice { get; set; }
        //public int AnswerId { get; set; }
        public string Direction { get; set; }

        //public int UserSelectedId
        //{
        //    get
        //    {
        //        if (UserChoices == null)
        //            return 0;
        //        else
        //            return UserChoices == null ? 0 : UserChoices.Where(p => p.IsChecked == "on" || "true")
        //    }
        //}
    }
}
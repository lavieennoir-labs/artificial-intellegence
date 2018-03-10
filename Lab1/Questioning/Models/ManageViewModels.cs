using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Questioning.Models
{
    public class IndexViewModel
    {
        public IList<QuestioningDataViewModel> PassedQuestionings { get; set; }
    }

    public class QuestioningViewModel
    {
        public IList<UserLoginInfo> Logins { get; set; }
        public bool BrowserRemembered { get; set; }

        public string Question { get; set; }
        public int QuestionId { get; set; }
        public bool IsLast { get; set; }
        public bool IsFirst { get; set; }
        public int QuestionCount { get; set; }
        public string Rank { get; set; }
        public IList<Answer> Answers { get; set; }
        public int SelectedAnswer { get; set; }
    }
}
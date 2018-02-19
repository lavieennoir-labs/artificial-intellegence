﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Questioning.Models
{
    public class IndexViewModel
    {
        //public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        //public string PhoneNumber { get; set; }
        //public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class QuestioningViewModel
    {
        //public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        //public string PhoneNumber { get; set; }
        //public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }
}
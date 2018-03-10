using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Questioning.Models;
using Questioning.Models.ProductionRules;

namespace Questioning.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Home/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.Error ? "Сталась помилка." : "";
            

            var passedQuestionings = new List<QuestioningDataViewModel>();

            var username = User.Identity.GetUserName();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var questionCount = db.Questions.Count();
                //select all questionings for current user
                var questionings = db.QuestioningResults.Join(
                    db.Users, 
                    qr => qr.Id.ToString(), 
                    u => u.Id, 
                    (qr, u) => new
                    {
                        qr.Id,
                        u.UserName,
                        qr.QuestioningNum,
                        qr.Question,
                        qr.SelectedAnswer
                    }).GroupBy(qr => qr.Id);

                foreach(var questioining in questionings)
                {
                    Dictionary<string, RankDataViewModel> ranks = new Dictionary<string, RankDataViewModel>();
                    foreach (var question in questioining)
                    {
                        if (ranks.ContainsKey(question.Question.Rank.Text))
                        {
                            ranks[question.Question.Rank.Text].Questions.Add(new AnsweredQuestionViewModel
                            {
                                Question = question.Question.Text,
                                Answer = question.SelectedAnswer.Text,
                                AnswerMark = question.SelectedAnswer.Mark
                            });
                            ranks[question.Question.Rank.Text].Total += question.SelectedAnswer.Mark;
                        }
                        else
                            ranks.Add(question.Question.Rank.Text, new RankDataViewModel
                            {
                                Total = question.SelectedAnswer.Mark,
                                Questions = new List<AnsweredQuestionViewModel>
                                {
                                    new AnsweredQuestionViewModel
                                    {
                                        Question = question.Question.Text,
                                        Answer = question.SelectedAnswer.Text,
                                        AnswerMark = question.SelectedAnswer.Mark
                                    }
                                }
                            });
                    }

                    QuestioningDataViewModel questioningData = new QuestioningDataViewModel
                    {
                        TotalQuestionCount = questionCount,
                        AnsweredQuestionCount = questioining.Count(),
                        QuestioningId = questioining.Key,
                        Rank = ranks
                    };

                    passedQuestionings.Add(questioningData);
                }
            }

            var model = new IndexViewModel
            {
                PassedQuestionings = passedQuestionings
            };

            return View(model);
        }

        int? CurrentQuestioning = null;

        //
        // GET: /Home/Questioning
        /// <param name="pqId">previous question id</param>
        /// <param name="paId">previous selected answer id</param>
        public async Task<ActionResult> Questioning(ManageMessageId? message, string questionId, int? pqId, int? paId)
        {
            QuestioningViewModel model;
            var userId = User.Identity.GetUserId();

            //update answer
            if (CurrentQuestioning != null && pqId != null && paId != null)
            {
                using (var db = new ApplicationDbContext())
                {

                }
            }


            ViewBag.StatusMessage =
                message == ManageMessageId.Error ? "Сталась помилка."
                : "";

            try {
                model = new QuestioningViewModel
                {
                    Logins = await UserManager.GetLoginsAsync(userId),
                    BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
                };

                //get current question
                var que = new Questioning.Models.Questioning();
                int qId;
                if(questionId == null)
                    qId = que.getFirstUnansweredQuestionId(userId);
                else if (!Int32.TryParse(questionId, out qId))
                    qId = que.getFirstUnansweredQuestionId(userId);
                //current question
                Question q = que.GetQuestion(qId);
                model.Question = q.Text;
                model.QuestionId = q.Id;
                model.Rank = q.Rank.Text;
                model.Answers = q.GetAnswerVariants.Select(a => a.Answer).ToList();
                model.Answers.Shuffle();    //shuffle answer variants
                model.SelectedAnswer = model.Answers.First().Id;
                model.QuestionCount = que.GetQuestionCount();
                model.IsFirst = q.Id == 1;
                model.IsLast = q.Id == model.QuestionCount;

            }
            catch(Exception e)
            {
                AuthenticationManager.SignOut();
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        //
        // GET: /Home/Reslut
        public async Task<ActionResult> Reslut(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.Error ? "Сталась помилка."
                : "";
            
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}
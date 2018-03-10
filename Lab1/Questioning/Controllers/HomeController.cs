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
            
            var passedQuestionings = new Questioning.Models.Questioning().
                GetQuestioningData(User.Identity.GetUserId());

            var model = new IndexViewModel
            {
                PassedQuestionings = passedQuestionings
            };

            return View(model);
        }

        int CurrentQuestioning = -1;

        //
        // GET: /Home/Questioning
        /// <param name="pqId">previous question id</param>
        /// <param name="paId">previous selected answer id</param>
        public async Task<ActionResult> Questioning(ManageMessageId? message, string questionId, int? pqId, int? paId)
        {
            QuestioningViewModel model;
            var userId = User.Identity.GetUserId();
            
            ViewBag.StatusMessage =
                message == ManageMessageId.Error ? "Сталась помилка."
                : "";
                 
            try {
                var que = new Questioning.Models.Questioning();
                CurrentQuestioning = que.GetCurrentQuestioningId(userId);

                //create questioning
                if(CurrentQuestioning == -1)
                {
                    var questioningNum = que.GetLastQuestioningId(userId);
                    if (questioningNum == -1)
                        questioningNum = 1;
                    else
                        questioningNum++;
                    using (var db = new ApplicationDbContext())
                    {
                        var results = db.QuestioningResults;
                        var questions = db.Questions;
                        foreach(var question in questions)
                            results.Add(new QuestioningResult
                            {
                                UserId = userId,
                                QuestioningNum = questioningNum,
                                QuestionId = question.Id,
                                SelectedAnswerId = null
                            });
                        db.SaveChanges();
                    }
                    CurrentQuestioning = que.GetCurrentQuestioningId(userId);
                }
                //update answer
                if (pqId != null && paId != null)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        var curentResult = db.QuestioningResults.Where(qr =>
                            qr.UserId == userId &&
                            qr.QuestioningNum == CurrentQuestioning &&
                            qr.QuestionId == pqId);
                        curentResult.First().SelectedAnswerId = (int)paId;
                        db.SaveChanges();
                    }
                }

                model = new QuestioningViewModel
                {
                    Logins = await UserManager.GetLoginsAsync(userId),
                    BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
                };

                //get current question
                int qId;
                if(questionId == null)
                    qId = que.getFirstUnansweredQuestionId(userId);
                else if (!Int32.TryParse(questionId, out qId))
                    qId = que.getFirstUnansweredQuestionId(userId);
                //current question
                Question q = que.GetQuestion(qId);
                var answer = que.GetCurrentQuestioningAnswer(userId, q.Id);
                
                model.Question = q.Text;
                model.QuestionId = q.Id;
                model.Rank = q.Rank.Text;
                model.Answers = q.GetAnswerVariants.Select(a => a.Answer).ToList();
                //model.Answers.Shuffle();    //shuffle answer variants
                model.SelectedAnswer = answer == -1 ? model.Answers.First().Id : answer;
                model.QuestionCount = que.GetQuestionCount();
                model.AnswerCount = que.GetAnsweredQuestionsCount(userId, CurrentQuestioning);
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
        // GET: /Home/Result
        public async Task<ActionResult> Result(ManageMessageId? message, int? questioningNum, string questionId, int? pqId, int? paId)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.Error ? "Сталась помилка."
                : "";

            var userId = User.Identity.GetUserId();
            var que = new Questioning.Models.Questioning();
            CurrentQuestioning = que.GetCurrentQuestioningId(userId);

            //handle invalid redirect
            if (CurrentQuestioning == -1 && questioningNum == null)
            {
                return RedirectToAction("Index");
            }
            //update answer
            if (pqId != null && paId != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    var curentResult = db.QuestioningResults.Where(qr =>
                        qr.UserId == userId &&
                        qr.QuestioningNum == (questioningNum ?? CurrentQuestioning) &&
                        qr.QuestionId == pqId);
                    curentResult.First().SelectedAnswerId = (int)paId;
                    db.SaveChanges();
                }
            }

            //handle result
            ResultViewModel model = new ResultViewModel
            {
                QuestioningData = que.GetQuestioningData(userId, (questioningNum ?? CurrentQuestioning)).First()
            };

            return View(model);
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
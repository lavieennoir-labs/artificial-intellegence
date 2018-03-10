using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Questioning.Models
{
    //encapsulation of db context usage
    public class Questioning
    {
        public List<QuestioningDataViewModel> GetQuestioningData(string userId, int questioningNum = -1)
        {
            var passedQuestionings = new List<QuestioningDataViewModel>();         

            IEnumerable<IGrouping<int, QuestioningResult>> questionings;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if(questioningNum <= 0)
                    //select all questionings for current user
                    questionings = db.QuestioningResults.AsNoTracking().
                        Where(qr => qr.UserId == userId).ToList().
                        GroupBy(qr => qr.QuestioningNum);
                else
                    questionings = db.QuestioningResults.AsNoTracking().
                        Where(qr => qr.UserId == userId && qr.QuestioningNum == questioningNum).ToList().
                        GroupBy(qr => qr.QuestioningNum);

                foreach (var questioining in questionings)
                {
                    Dictionary<string, RankDataViewModel> ranks = new Dictionary<string, RankDataViewModel>();
                    foreach (var question in questioining)
                    {
                        string rank = GetQuestionRank(question.QuestionId, db);
                        Answer answer = new Answer { Text = "", Mark = 0 };
                        if (question.SelectedAnswerId != null)
                            answer = db.Answers.AsNoTracking().Where(a => a.Id == question.SelectedAnswerId).First();

                        if (ranks.ContainsKey(rank))
                        {

                            ranks[rank].Questions.Add(new AnsweredQuestionViewModel
                            {
                                Question = db.Questions.AsNoTracking().Where(q => q.Id == question.QuestionId).First().Text,
                                Answer = answer.Text,
                                AnswerMark = answer.Mark
                            });
                            ranks[rank].Total += answer.Mark;
                        }
                        else
                            ranks.Add(rank, new RankDataViewModel
                            {
                                Total = answer.Mark,
                                Questions = new List<AnsweredQuestionViewModel>
                                {
                                    new AnsweredQuestionViewModel
                                    {
                                        Question = db.Questions.AsNoTracking().Where(q => q.Id == question.QuestionId).First().Text,
                                        Answer = answer.Text,
                                        AnswerMark = answer.Mark,
                                    }
                                }
                            });
                    }

                    QuestioningDataViewModel questioningData = new QuestioningDataViewModel
                    {
                        TotalQuestionCount = GetQuestionCount(),
                        AnsweredQuestionCount = questioining.Where(qr => qr.SelectedAnswerId != null).Count(),
                        QuestioningId = questioining.Key,
                        Rank = ranks,

                    };

                    passedQuestionings.Add(questioningData);
                }
            }
            return passedQuestionings;
        }

        public int GetAnsweredQuestionsCount(string userId, int questioningNum)
        {
            int count;
            using(var db = new ApplicationDbContext())
            {
                count = db.QuestioningResults.Where(
                    qr => qr.UserId == userId
                    && qr.QuestioningNum == questioningNum
                    && qr.SelectedAnswerId != null).Count();
            }
            return count;
        }

        public string GetQuestionRank(int questionId, ApplicationDbContext db)
        {
            string rank;
            rank = db.Questions.Where(q => q.Id == questionId).
                Join(
                    db.Ranks,
                    q => q.RankId,
                    r => r.Id,
                    (q, r) => r.Text
                ).First();
            return rank;
        }

        public string GetQuestionRank(int questionId)
        {
            string rank;
            using (var db = new ApplicationDbContext())
            {
                rank = db.Questions.Where(q => q.Id == questionId).
                    Join(
                        db.Ranks,
                        q => q.RankId,
                        r => r.Id,
                        (q, r) => r.Text
                    ).First();
            }
            return rank;
        }

        public int GetCurrentQuestioningAnswer(string userId, int questionId)
        {
            int lastQuestioningNum = GetCurrentQuestioningId(userId);
            if (lastQuestioningNum == -1)
                return -1;
            int? answer;
            using (var db = new ApplicationDbContext())
            {
                answer = db.QuestioningResults.Where(
                    qr => qr.UserId == userId
                    && qr.QuestioningNum == lastQuestioningNum
                    && qr.QuestionId == questionId).First().SelectedAnswerId;
            }
            return answer ?? -1;
        }

        public int GetQuestionCount(ApplicationDbContext db)
        {
            int count = 0;
                count = db.Questions.Count();
            return count;
        }
        public int GetQuestionCount()
        {
            int count = 0;
            using (var db = new ApplicationDbContext())
            {
                count = db.Questions.Count();
            }
            return count;
        }

        ///returns -1 if no anaswered questioins found
        public int getFirstUnansweredQuestionId(string userId)
        {
            int? id = null;
            int? lastQuestioningNum = GetCurrentQuestioningId(userId);
            if (lastQuestioningNum == -1) return -1;

            using (var db = new ApplicationDbContext())
            {
                var seq = db.QuestioningResults.Where(
                    qr => qr.UserId == userId
                    && qr.QuestioningNum == lastQuestioningNum
                    && qr.SelectedAnswerId == null);
                if (seq.Count() == 0)
                    id = null;
                else
                    id = seq.First().QuestionId;
            }
            return id ?? -1;
        }

        public int GetLastQuestioningId(string userId)
        {
            int? lastQuestioningNum = null;
            using (var db = new ApplicationDbContext())
            {
                var userResluts = db.QuestioningResults.Where(qr => qr.UserId == userId);
                lastQuestioningNum = userResluts.Max<QuestioningResult, int?>(qr => qr.QuestioningNum);
            }
            return lastQuestioningNum ?? -1;
        }

        public int GetCurrentQuestioningId(string userId)
        {
            int? lastQuestioningNum = null;
            using (var db = new ApplicationDbContext())
            {
                var userResluts = db.QuestioningResults.Where(qr => qr.UserId == userId && qr.SelectedAnswer == null);
                if (userResluts.Count() == 0)
                    lastQuestioningNum = null;
                else
                    lastQuestioningNum = userResluts.Max<QuestioningResult, int?>(qr => qr.QuestioningNum);
            }
            return lastQuestioningNum ?? -1;
        }

        public Question GetQuestion(int id)
        {
            Question que = null;
            using (var db = new ApplicationDbContext())
            {
                if (id <= 0 || id > db.Questions.Count())
                    que = db.Questions.First();
                else
                    que = db.Questions.AsNoTracking().Where(q => q.Id == id).First();
                que.GetAnswerVariants = db.AnswerVariants.AsNoTracking().Where(av => av.QuestionId == que.Id).ToList();

                que.Rank = db.Ranks.Where(r => r.Id == que.RankId).First();
                foreach(var answerVariant in que.GetAnswerVariants)
                    answerVariant.Answer = db.Answers.Where(a => a.Id == answerVariant.AnswerId).First();
            }

            return que;
        }
    }


    public class Rank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }


        [ForeignKey("RankId")]
        public ICollection<Question> Questions { get; set; }
    }

    public class QuestioningResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public int QuestioningNum { get; set; }
        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        
        public int? SelectedAnswerId { get; set; }
        public Answer SelectedAnswer { get; set; }
    }

    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int RankId { get; set; }
        public Rank Rank { get; set; }

        [ForeignKey("QuestionId")]
        public ICollection<QuestioningResult> QuestioningResults { get; set; }
        [ForeignKey("QuestionId")]
        public ICollection<AnswerVariant> GetAnswerVariants { get; set; }
    }

    public class AnswerVariant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        [Required]
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }

    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int Mark { get; set; }

        [ForeignKey("SelectedAnswerId")]
        public ICollection<QuestioningResult> QuestioningResults { get; set; }
        [ForeignKey("AnswerId")]
        public ICollection<AnswerVariant> AnswerVariants { get; set; }
    }
}
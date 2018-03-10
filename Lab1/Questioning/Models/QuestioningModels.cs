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
            int? lastQuestioningNum = null;
            using (var db = new ApplicationDbContext())
            {
                var userResluts = db.QuestioningResults.Where(qr => qr.UserId == userId);
                lastQuestioningNum = userResluts.Max<QuestioningResult, int?>(qr => qr.QuestioningNum);
                if (lastQuestioningNum == null) return -1;
                id = userResluts.Where(
                    qr => qr.QuestioningNum == lastQuestioningNum 
                    && qr.SelectedAnswer == null).First().QuestionId;
            }
            return id ?? -1;
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
        
        public int SelectedAnswerId { get; set; }
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
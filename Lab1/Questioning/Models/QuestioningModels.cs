using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Questioning.Models
{
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

    public class QuestioningToUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int QuestioningResultId { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }

    public class QuestioningResult
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        [Required]
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
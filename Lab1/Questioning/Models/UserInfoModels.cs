using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questioning.Models
{
    public class AnsweredQuestionViewModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int AnswerMark { get; set; }
    }

    /// <summary>
    /// Represents statistical data about certain questioning
    /// </summary>
    public class QuestioningDataViewModel
    {
        /// <summary>
        /// The key is Rank name and the value is an instance of RankData class
        /// </summary>
        public IDictionary<string, RankDataViewModel> Rank { get; set; }

        public int AnsweredQuestionCount { get; set; }
        public int TotalQuestionCount { get; set; }

        public int QuestioningId { get; set; }

        public bool IsComplited { get => AnsweredQuestionCount == TotalQuestionCount; }
        public string Result { get
            {
                var maxValue = Rank.Max(r => r.Value.Total / (double)(r.Value.Questions.Count() * 5));
                return //string.Join(", або ",
                    Rank.Where(r => Math.Abs(r.Value.Total / (double)(r.Value.Questions.Count() * 5) - maxValue) < 0.01).
                        //Select(r => r.Key)).ToLowerInvariant();
                        First().Key.ToLowerInvariant();
            }
        }
    }

    public class RankDataViewModel
    {
        public int Total { get; set; }

        public IList<AnsweredQuestionViewModel> Questions { get; set; }
    }
}
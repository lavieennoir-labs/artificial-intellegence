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
    }

    public class RankDataViewModel
    {
        public int Total { get; set; }

        public IList<AnsweredQuestionViewModel> Questions { get; set; }
    }
}
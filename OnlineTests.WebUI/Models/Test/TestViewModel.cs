using OnlineTests.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTests.WebUI.Models
{
    public class TestViewModel
    {
        public int TestID { get; set; }
        public Test Test { get; set; }
        public bool SaveRatings { get; set; }
        public int Point { get; set; }
        public int CurrentQuestionNumber { get; set; }
        public QuestionViewModel CurrentQuestion { get; set; }
        public IList<AnswerViewModel> CurrentAnswers { get; set; }
    }
}
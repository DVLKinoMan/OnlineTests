using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTests.WebUI.Models
{
    public class QuestionViewModel
    {
        public string Text { get; set; }
        public int Point { get; set; }
        public int TimeForAnswer { get; set; }
    }
}
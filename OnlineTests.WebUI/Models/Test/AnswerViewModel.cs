using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTests.WebUI.Models
{
    public class AnswerViewModel
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}
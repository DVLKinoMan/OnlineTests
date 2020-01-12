using OnlineTests.WebUI.Models;
using OnlineTests.Domain.Abstract;
using OnlineTests.Domain.Entities;
using OnlineTests.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnlineTests.WebUI.Controllers
{
    [RoleAuthorization("User")]
    public class TestController : Controller
    {
        private OnlineTestsRepository repository;

        public TestController(OnlineTestsRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index(int testID)
        {
            return View(new TestViewModel
            {
                TestID = testID,
                Test = repository.getTestByID(testID),
                SaveRatings = true,
                Point = 0,
                CurrentQuestionNumber = 0
            });
        }

        [NoCache]
        [HttpPost]
        public ActionResult Index(TestViewModel t, string TimeEnd)
        {
            for (int i = ModelState.Keys.Count - 1; i >= 0; i--)
            {
                string k = ModelState.Keys.ElementAt(i);
                if (k != "TestID" && k != "SaveRatings")
                    ModelState.Remove(k);
            }
            t.Test = repository.getTestByID(t.TestID);
            if (t.CurrentAnswers != null && t.CurrentQuestionNumber != 0 && TimeEnd!="true")
                if (isRightAnswer(t.CurrentAnswers))
                    t.Point += t.CurrentQuestion.Point;
            if (t.CurrentQuestionNumber >= t.Test.validQuestions.Count())
            {
                UserResult res = new UserResult
                {
                    PointsEarned = t.Point,
                    MaxPoint = t.Test.maxPoint,
                    TestTakenTime = DateTime.Now,
                    TestId = t.Test.ID,
                    UserId = repository.getUserByName(Session["User"].ToString()).ID,
                    Test = t.Test,
                    User = repository.getUserByName(Session["User"].ToString())
                };
                if (t.SaveRatings == true)
                    repository.addUserResult(res);
                return View("Result", res);
            }

            t.CurrentQuestionNumber++;
            Question q = t.Test.validQuestions.ToList().ElementAt(t.CurrentQuestionNumber-1);
            t.CurrentQuestion = new QuestionViewModel { Text = q.Text, Point = q.Point, TimeForAnswer = q.TimeForAnswer }; 

            List<AnswerViewModel> ans = new List<AnswerViewModel>();
            while (ans.Count() != q.Answers.Count())
            {
                Random r = new Random();
                int k = r.Next(q.Answers.Count);
                Answer a = q.Answers.ElementAt(k);
                if (!ans.Exists(answ => answ.ID == a.ID))
                    ans.Add(new AnswerViewModel { ID = a.ID, Text = a.Text, Checked = false });
            }
            t.CurrentAnswers = ans;

            return View("NextQuestion", t);
        }

        private bool isRightAnswer(IEnumerable<AnswerViewModel> answers)
        {
            foreach (AnswerViewModel ans in answers)
            {
                if (repository.getAnswerByID(ans.ID).IsRight == "No" && ans.Checked == true)
                    return false;
                if(ans.Checked==false && repository.getAnswerByID(ans.ID).IsRight=="Yes")
                    return false;
            }
            return true;
        }

        public static string GetDateTimeString(DateTime fromThis, DateTime toThis, TimeSpan maxTimeHadPassed)
        {
            if (fromThis > toThis)
                throw new ArgumentException();

            var passed = toThis.Subtract(fromThis);
            if (maxTimeHadPassed < passed)
                return fromThis.ToString("dd/MM/yyyy HH:mm");

            var builder = new StringBuilder();
            if (passed.Days > 0)
                builder.Append($"{passed.Days} d ");

            passed = passed.Subtract(new TimeSpan(passed.Days, 0, 0, 0));
            if (passed.Hours > 0)
                builder.Append($"{passed.Hours} h ");

            passed = passed.Subtract(new TimeSpan(0, passed.Hours, 0, 0));
            if (passed.Minutes > 0)
                builder.Append($"{passed.Minutes} min ");

            builder.Append("ago");
            return builder.ToString();
        }
    }
}
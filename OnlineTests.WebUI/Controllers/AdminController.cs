using OnlineTests.Domain.Abstract;
using OnlineTests.WebUI.Models;
using OnlineTests.Domain.Entities;
using OnlineTests.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTests.WebUI.Controllers
{
    [RoleAuthorization("Admin")]
    public class AdminController : Controller
    {
        private OnlineTestsRepository repository;
        private int pageSize = 4;

        public AdminController(OnlineTestsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult AdminIndex(string SearchedTestName=null)
        {
            ViewBag.NavBarActive = "ტესტები";
            if (SearchedTestName != null && SearchedTestName != "")
                return View(repository.SelectTestsByName(SearchedTestName));
            return View(repository.Tests.Where(t=>t.IsActive=="Yes"));
        }

        [HttpPost]
        public ActionResult AdminIndex(int testID)
        {
            repository.deactivateTest(testID);
            TempData["Message"] = string.Format("ტესტის წაშლა მოხდა წარმატებით", repository.getTestByID(testID));
            return RedirectToAction("AdminIndex");
        }

        [HttpGet]
        public ActionResult ControlUsers(string SearchedUserName=null)
        {
            ViewBag.NavBarActive = "მომხმარებლები";
            if (SearchedUserName != null && SearchedUserName!="")
            {
                return View(repository.SelectUsersByName(SearchedUserName));
            }
            else
            {
                return View(repository.Users);
            }
        }

        [HttpPost]
        public ActionResult ControlUsers(int userID)
        {
            repository.deactivateUser(userID);
            TempData["Message"] = string.Format("მომხმარებლის წაშლა მოხდა წარმატებით",repository.getUserByID(userID).Name);
            return RedirectToAction("ControlUsers");
        }

        public ViewResult EditUser(string username)
        {
            ViewBag.NavBarActive = "მომხმარებლები";
            var us = repository.getUserByName(username);
            if (us != null)
                return View(us);
            else return View();
        }

        [HttpPost]
        public ActionResult EditUser(User model)
        {
            ViewBag.NavBarActive = "მომხმარებლები";
            if (ModelState.IsValid)
            {
                try
                {
                    repository.Add_Edit_User(model);
                    TempData["Message"] = string.Format("მომხმარებელი {0}-ის რედაქტირება მოხდა წარმატებულად",model.Name);
                    return RedirectToAction("DeactivateUser");
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "ბაზაში ცვლილებისას დაფიქსირდა შეცდომა! შესაძლებელია მომხმარებელი შესაბამისი სახელით ან ელ-ფოსტით უკვე არსებობს";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public ViewResult Edit_Create_Test(int? testID = null)
        {
            ViewBag.NavBarActive = "ტესტები";
            if (testID != null)
            {
                var test = repository.getTestByID((int)testID);
                if (test != null)
                {
                    ViewBag.Categories = repository.getAllCategoriesSelectList(test.CategoryId);
                    ViewBag.ActionName = "ტესტის რედაქტირება";
                    return View(test);
                }
            }
            ViewBag.Categories = repository.getAllCategoriesSelectList();
            ViewBag.ActionName = "ახალი ტესტის შექმნა";
            return View(new Test());
        }

        [HttpPost]
        public ActionResult Edit_Create_Test(Test test)
        {
            ViewBag.Categories = repository.getAllCategoriesSelectList(test.CategoryId);
            ViewBag.NavBarActive = "ტესტები";

            Test t = repository.getTestByID(test.ID);
            if (t!=null)
                ViewBag.ActionName = "ტესტის რედაქტირება";
            else ViewBag.ActionName = "ახალი ტესტის შექმნა";

            if (ModelState.IsValid)
            {
                try
                {
                    repository.Add_Edit_Test(test);
                    ViewBag.ActionName = "ტესტის რედაქტირება";
                    TempData["SuccessMessage"] = string.Format("ტესტი {0}-ის რედაქტირება განხორციელდა წარმატებით", test.Name);
                    return View(repository.getTestByID(test.ID));
                }
                catch (Exception exc)
                {
                    TempData["ErrorMessage"] = string.Format("ტესტი {0}-ის რედაქტირებისას დაფიქსირდა შეცდომა. ExceptionMessage: {1}", test.Name, exc.Message);
                    return View(test);
                }
            }
            return View(test);
        }

        public ActionResult Edit_Create_Question(int? testID, int? questionID = null)
        {
            ViewBag.NavBarActive = "ტესტები";
            if (questionID != null)
            {
                var q = repository.getQuestionByID((int)questionID);
                if (q != null)
                {
                    ViewBag.ActionName = "კითხვის რედაქტირება";
                    return View(q);
                }
            }
            if (testID != null)
            {
                ViewBag.ActionName = "კითხვის დამატება";
                return View(new Question() { TestId = (int)testID, Test = repository.getTestByID((int)testID) });
            }
            TempData["ErrorMessage"] = "შეუძლებელია კითვის დამატება! შექმენით ტესტი!";
            return View("Edit_Create_Test");
        }

        [HttpPost]
        public ActionResult Edit_Create_Question(Question question)
        {
            ViewBag.NavBarActive = "ტესტები";
            ViewBag.ActionName = "კითხვის დამატება";
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.ActionName = "კითხვის რედაქტირება";
                    Question q = repository.getQuestionByID(question.ID);
                    repository.Add_Edit_Question(question);
                    if (q == null)
                    {
                        TempData["SuccessMessage"] = "კითხვის რედაქტირება განხორციელდა წარმატებულად. დაამატეთ სავარაუდო პასუხები";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "კითხვის რედაქტირება განხორციელდა წარმატებულად. ";
                        if (q.Answers.Count != 0)
                        {
                            if (q.Answers.Count >= 2 && (q.Answers.Count(a => a.IsRight == "Yes") < q.Answers.Count) && q.Answers.Count(a=>a.IsRight=="Yes")>=1)
                                return RedirectToAction("Edit_Create_Test", new { testID = q.TestId });
                            else TempData["ErrorMessage"] = "სავარაუდო პასუხების რაოდენობა უნდა იყოს არანაკლებ 2, სწორი პასუხების რაოდენობა მინიმუმ ერთი და ნაკლები სავარაუდო პასუხებზე";
                        }
                        else TempData["SuccessMessage"] += "დაამატეთ სავარაუდო პასუხები";
                        return View(repository.getQuestionByID(question.ID));
                    }
                }
                catch (Exception exc)
                {
                    TempData["ErrorMessage"] = "კითხვის რედატირებისას განხორციელდა შეცდომა. ExceptionMessage:" + exc.Message;
                }
            }
            return View(question);
        }

        public PartialViewResult QuestionSummary(Question question, int testID)
        {
            ViewBag.QuestionNumber = (repository.Questions.Where(q => q.TestId == testID).OrderBy(q => q.ID).ToList().IndexOf(repository.getQuestionByID(question.ID))+1).ToString();
            ViewBag.ValidQuestion = repository.getTestByID(testID).validQuestions.Any(q=>q.ID==question.ID).ToString();
            return PartialView("AdminQuestionSummary", question);
        }

        public ActionResult Edit_Create_Answer(int questionID, int? answerID = null)
        {
            ViewBag.NavBarActive = "ტესტები";
            if (answerID != null)
            {
                ViewBag.ActionName = "პასუხის რედაქტირება";
                return View(repository.getAnswerByID((int)answerID));
            }
            ViewBag.ActionName = "პასუხის დამატება";
            return View(new Answer() { QuestionId = questionID, Question = repository.getQuestionByID(questionID) });
        }

        [HttpPost]
        public ActionResult Edit_Create_Answer(Answer answer)
        {
            ViewBag.NavBarActive = "ტესტები";
            ViewBag.ActionName = "პასუხის დამატება";
            if(ModelState.IsValid)
            {
                try
                {
                    repository.Add_Edit_Answer(answer);
                    TempData["SuccessMessage"] = string.Format("პასუხი {0}-ის რედაქტირება განხორციელდა წარმატებულად", answer.Text);
                    return RedirectToAction("Edit_Create_Question", new {  questionID = answer.QuestionId });
                }
                catch (Exception exc)
                {
                    TempData["ErrorMessage"] = string.Format("პასუხი {0}-ის რედაქტირებისას განხორციელდა შეცდომა. Exception: {1}", answer.Text,exc.Message);
                }
           }
            return View(repository.getAnswerByID(answer.ID));
        }

        public ActionResult deleteAnswer(int answerID,string returnUrl)
        {
            Answer a = repository.getAnswerByID(answerID);
            try
            {
                repository.deleteAnswer(a);
                TempData["SuccessMessage"] = "პასუხი წარმატებით წაიშალა ბაზიდან";
            }
            catch(Exception exc)
            {
                TempData["ErrorMessage"] = string.Format("პასუხის წაშლისას დაფიქსირდა შეცდომა. EcxeptionMessage={0}", exc.Message);
            }

            return Redirect(returnUrl);
        }

        public ActionResult deleteQuestion(int questionID, string returnUrl)
        {
            try
            {
                repository.deleteQuestion(repository.getQuestionByID(questionID));
                TempData["SuccessMessage"] = "კითხვა წარმატებით წაიშალა ბაზიდან";
            }

            catch (Exception exc)
            {
                TempData["ErrorMessage"] = string.Format("კითხვის წაშლისას დაფიქსირდა შეცდომა. EcxeptionMessage={0}", exc.Message);
            }

            return Redirect(returnUrl);
        }

        public ActionResult LogOut()
        {
            Session["Admin"] = null;
            return RedirectToAction("LoginAdmin", "Account");
        }
	}
}
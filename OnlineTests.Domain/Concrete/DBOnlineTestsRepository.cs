using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTests.Domain.Entities;
using OnlineTests.Domain.Abstract;
using System.Web.Mvc;

namespace OnlineTests.Domain.Concrete
{
    public class DBOnlineTestsRepository:OnlineTestsRepository
    {
        private OnlineTestsEntities context = new OnlineTestsEntities();

        public IEnumerable<Admin> Admins
        {
            get { return context.Admins; }
        }

        public IEnumerable<User> Users
        {
            get {  return context.Users; }
        }

        public IEnumerable<Test> Tests
        {
            get { return context.Tests; }
        }

        public IEnumerable<Question> Questions
        {
            get { return context.Questions; }
        }

        public IEnumerable<Answer> Answers
        {
            get { return context.Answers; }
        }

        public IEnumerable<Category> Categories
        {
            get { return context.Categories; }
        }

        public IEnumerable<UserResult> UserResults
        {
            get { return context.UserResults; }
        }
        #region Admins
        public Admin getAdminByName(string name)
        {
            return (from admin in Admins where admin.Name == name select admin).SingleOrDefault();
        }
        #endregion

        #region Users
        public User getUserByName(string username)
        {
            return (from user in Users where user.Name == username select user).SingleOrDefault();
        }

        public User getUserByEmail(string email)
        {
            return (from user in Users where user.Email == email select user).SingleOrDefault();
        }

        public User getUserByID(int userID)
        {
            return (from user in Users where user.ID == userID select user).SingleOrDefault();
        }

        public void deactivateUser(int userID)
        {
            getUserByID(userID).IsActive = "No";
            context.SaveChanges();
        }
        
        private void addUser(User newUser)
        {
             context.Users.Add(newUser);
             context.SaveChanges();
        }

        public void Add_Edit_User(User user)
        {
            User us = getUserByID(user.ID);
            if (us != null)
            {
                us.Name = user.Name;
                us.Email = user.Email;
                us.Password = user.Password;
                us.IsActive = user.IsActive;
                context.SaveChanges();
            }
            else addUser(user);
        }

        public IEnumerable<User> SelectUsersByName(string username)
        {
            return context.Users.Where(u => u.Name.Contains(username));
        }
        #endregion

        #region Tests
        public Test getTestByID(int testID)
        {
            return (from test in Tests where test.ID == testID select test).SingleOrDefault();
        }

        public Test getTestByName(string testname)
        {
            return (from test in Tests where test.Name == testname select test).SingleOrDefault();
        }

        public void deactivateTest(int testID)
        {
            getTestByID(testID).IsActive = "No";
            context.SaveChanges();
        }

        private void addTest(Test test)
        {
            test.Category = getCategoryByID(test.CategoryId);
            context.Tests.Add(test);
            context.SaveChanges();
        }

        public void Add_Edit_Test(Test test)
        {
            Test t=getTestByID(test.ID);
            if (t != null)
            {
                t.Name = test.Name;
                t.Level = test.Level;
                t.IsActive = test.IsActive;
                t.CategoryId = test.CategoryId;
                context.SaveChanges();
            }
            else addTest(test);
        }

        public IEnumerable<Test> SelectTestsByName(string testname)
        {
            return  context.Tests.Where(t => t.IsActive=="Yes" && t.Name.Contains(testname));
        }
        #endregion

        #region Categories
        public IEnumerable<SelectListItem> getAllCategoriesSelectList(int? categoryid=null)
        {
            if (categoryid != null)
                return context.Categories.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name, Selected = (c.ID == categoryid ? true : false) });
            else return context.Categories.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
        }

        public Category getCategoryByID(int categoryID)
        {
            return context.Categories.SingleOrDefault(c => c.ID == categoryID);
        }
        #endregion

        #region Questions
        private void addQuestion(Question question)
        {
            context.Questions.Add(question);
            context.SaveChanges();
        }

        public void Add_Edit_Question(Question question)
        {
            Question q = getQuestionByID(question.ID);
            if (q != null)
            {
                q.Point = question.Point;
                q.Text = question.Text;
                q.TimeForAnswer = question.TimeForAnswer;
                context.SaveChanges();
            }
            else addQuestion(question);
        }

        public Question getQuestionByID(int questionID)
        {
            return context.Questions.Where(q => q.ID == questionID).SingleOrDefault();
        }

        public void deleteQuestion(Question question)
        {
            context.Answers.RemoveRange(context.Answers.Where(a => a.QuestionId == question.ID));
            context.SaveChanges();
            context.Questions.Remove(question);
            context.SaveChanges();
        }
        #endregion

        #region Answers
        public Answer getAnswerByID(int answerID)
        {
            return context.Answers.Where(a => a.ID == answerID).SingleOrDefault();
        }

        private void addAnswer(Answer answer)
        {
            getQuestionByID(answer.QuestionId).Answers.Add(answer);
            context.SaveChanges();
        }

        public void Add_Edit_Answer(Answer answer)
        {
            Answer a = getAnswerByID(answer.ID);
            if (a != null)
            {
                a.IsRight = answer.IsRight;
                a.QuestionId = answer.QuestionId;
                a.Question = getQuestionByID(answer.QuestionId);
                a.Text = answer.Text;
            }
            else addAnswer(answer);
            context.SaveChanges();
        }

        public void deleteAnswer(Answer answer)
        {
            context.Answers.Remove(getAnswerByID(answer.ID));
            context.SaveChanges();
        }
        #endregion

        #region UserResults
        public void addUserResult(UserResult result)
        {
            context.UserResults.Add(result);
            context.SaveChanges();
        }
        #endregion
    }
}

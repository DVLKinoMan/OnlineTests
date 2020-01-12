using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTests.Domain.Entities;
using System.Web.Mvc;

namespace OnlineTests.Domain.Abstract
{
    public interface OnlineTestsRepository
    {
        IEnumerable<Admin> Admins { get; }
        IEnumerable<User> Users { get; }
        IEnumerable<Test> Tests { get; }
        IEnumerable<Question> Questions { get; }
        IEnumerable<Answer> Answers { get; }
        IEnumerable<Category> Categories { get; }
        IEnumerable<UserResult> UserResults { get; }

        User getUserByName(string username);
        User getUserByEmail(string email);
        User getUserByID(int userID);
        void Add_Edit_User(User user);
        void deactivateUser(int userID);
        IEnumerable<User> SelectUsersByName(string username);

        Admin getAdminByName(string name);

        Test getTestByID(int testID);
        Test getTestByName(string testname);
        void deactivateTest(int testID);
        void Add_Edit_Test(Test test);
        IEnumerable<Test> SelectTestsByName(string testname);

        IEnumerable<SelectListItem> getAllCategoriesSelectList(int? categoryid=null);
        Category getCategoryByID(int categoryID);

        void Add_Edit_Question(Question question);
        Question getQuestionByID(int questionID);
        void deleteQuestion(Question question);

        void Add_Edit_Answer(Answer answer);
        Answer getAnswerByID(int answerID);
        void deleteAnswer(Answer answer);

        void addUserResult(UserResult result);
    }
}

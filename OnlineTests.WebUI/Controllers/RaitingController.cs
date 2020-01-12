using OnlineTests.WebUI.Infrastructure;
using OnlineTests.Domain.Abstract;
using OnlineTests.Domain.Entities;
using OnlineTests.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTests.WebUI.Controllers
{
    [RoleAuthorization("User")]
    public class RaitingController : Controller
    {
        OnlineTestsRepository repository;

        public RaitingController(OnlineTestsRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            foreach (User u in repository.Users)
                users.Add(new UserViewModel { Name = u.Name, UserID = u.ID, UserResults = u.UserResults });
            return View(users.OrderByDescending(u=>u.Score));
        }
        
        public ActionResult SearchTestRaitings(string tname)
        {
            List<TestRaitingViewModel> tests=new List<TestRaitingViewModel>();
            foreach(Test t in repository.SelectTestsByName(tname))
                tests.Add(new TestRaitingViewModel{ Test=t, UserResutls=t.UserResults });
            return View(tests);
        }
	}
}
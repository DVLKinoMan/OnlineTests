using OnlineTests.Domain.Abstract;
using OnlineTests.WebUI.Infrastructure;
using OnlineTests.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTests.WebUI.Controllers
{
    [RoleAuthorization("User")]
    public class TestsController : Controller
    {
        OnlineTestsRepository repository;
        int pageSize = 4;

        public TestsController(OnlineTestsRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult List(string category=null, int page = 1, int? level = null)
        {
            TestsListViewModel tests = new TestsListViewModel
            {
                Tests = repository.Tests.Where(t => t.IsActive=="Yes" && (category == null || category=="" || t.Category.Name == category) && (level==null || t.Level==(int) level)).OrderBy(t=>t.ID).Skip(pageSize * (page - 1)).Take(pageSize),
                CurrentCategory = category,
                CurrentLevel=level,
                PagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = pageSize, TotalItems = repository.Tests.Where(t => t.IsActive == "Yes" && (category == null || category == "" || t.Category.Name == category) && (level == null || t.Level == (int)level)).Count() }
            };
            return View(tests);
        }

        public ActionResult SearchTest(string testname)
        {
            TestsListViewModel tests = new TestsListViewModel
            {
                Tests = repository.SelectTestsByName(testname),
                CurrentCategory = null,
                CurrentLevel = null,
                PagingInfo = new PagingInfo { CurrentPage = 1, ItemsPerPage = repository.SelectTestsByName(testname).Count(), TotalItems = repository.SelectTestsByName(testname).Count() }
            };
            return View("List",tests);
        }

        public ActionResult LogOut()
        {
            Session["User"] = null;
            return RedirectToAction("LoginUser", "Account");
        }
	}
}
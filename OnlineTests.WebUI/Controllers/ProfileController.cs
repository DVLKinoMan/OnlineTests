using OnlineTests.WebUI.Infrastructure;
using OnlineTests.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTests.WebUI.Controllers
{
    [RoleAuthorization("User")]
    public class ProfileController : Controller
    {
        OnlineTestsRepository repository;

        public ProfileController(OnlineTestsRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index( int? UserID )
        {
            if (UserID != null)
            {
                ViewBag.Username = repository.getUserByID((int)UserID).Name.ToString();
                return View(repository.UserResults.Where(u => u.UserId == UserID).OrderByDescending(r => r.TestTakenTime));
            }
            ViewBag.Username = Session["User"].ToString();
            return View(repository.UserResults.Where(u => u.UserId == repository.getUserByName(Session["User"].ToString()).ID).OrderByDescending(r=>r.TestTakenTime));
        }
	}
}
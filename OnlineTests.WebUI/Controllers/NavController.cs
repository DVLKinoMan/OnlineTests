using OnlineTests.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTests.WebUI.Controllers
{
    public class NavController : Controller
    {
        OnlineTestsRepository repository;

        public NavController(OnlineTestsRepository repository)
        {
            this.repository = repository;
        }

        public PartialViewResult Menu(string category=null)
        {
            ViewBag.SelectedCategory = category;
            return PartialView(repository.Categories.Select(c=>c.Name).Distinct().OrderBy(t=>t));
        }
	}
}
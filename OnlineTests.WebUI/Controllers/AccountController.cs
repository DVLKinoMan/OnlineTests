using OnlineTests.WebUI.Models;
using OnlineTests.Domain.Abstract;
using OnlineTests.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTests.WebUI.Controllers
{
    public class AccountController : Controller
    {
        OnlineTestsRepository repository;

        public AccountController(OnlineTestsRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult LoginUser(string username=null, string message=null )
        {
            LoginViewModel user = new LoginViewModel();
            user.Role = Role.მომხმარებელი;
            user.Username = username;
            ViewBag.Message = message;
            return View("Login",user);
        }

        [HttpPost]
        public ActionResult LoginUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User us = repository.getUserByName(model.Username);
                if (us!=null && us.Password == model.Password && us.IsActive == "Yes")
                {
                    HttpContext.Session["User"] = us.Name;
                    return RedirectToAction("List", "Tests");
                }
                else
                {
                    ModelState.AddModelError("Username", "არასწორი მომხმარებლის სახელი ან პაროლი ან მომხმარებელი არ არის გააქტიურებული");
                    ModelState.AddModelError("Password", "არასწორი მომხმარებლის სახელი ან პაროლი ან მომხმარებელი არ არის გააქტიურებული");
                    return View("Login", model);
                }
            }
            else
            {
                return View("Login", model);
            }
        }

        public ViewResult LoginAdmin()
        {
            LoginViewModel admin = new LoginViewModel();
            admin.Role = Role.ადმინისტრატორი;
            return View("Login", admin);
        }

        [HttpPost]
        public ActionResult LoginAdmin(LoginViewModel model)
        {
            model.Role = Role.ადმინისტრატორი;
            if (ModelState.IsValid)
            {
                Admin admin = repository.getAdminByName(model.Username);
                if (admin!=null && admin.Password == model.Password)
                {
                    HttpContext.Session["Admin"] = admin.Name;
                    return RedirectToAction("AdminIndex", "Admin");
                }
                else
                {
                    ModelState.AddModelError("Username", "არასწორი პაროლი ან მომხმარებლის სახელი");
                    ModelState.AddModelError("Password", "არასწორი პაროლი ან მომხმარებლის სახელი");
                    return View("Login", model);
                }
            }
            else
            {
                return View("Login", model);
            }
        }

        public ViewResult RegisterUser()
        {
            RegisterViewModel user = new RegisterViewModel();
            return View("Register",user);
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (repository.getUserByName(model.Username)!=null)
                    ModelState.AddModelError("Username", "ასეთი მომხმარებელი უკვე არსებობს. გთხოვთ აირჩიოთ სხვა სახელი");
                if (repository.getUserByEmail(model.Email)!=null)
                    ModelState.AddModelError("Email", "მომხმარებელი ასეთი ელ-ფოსტით დარეგისტრირებულია. გთხოვთ სცადოთ სხვა ელ-ფოსტა");
                if (model.Password != model.ConfirmPassword)
                    ModelState.AddModelError("ConfirmPassword", "დამადასტურებელი პაროლი არ ემთხვევა პაროლს");
                if (ModelState.IsValid)
                {
                    User newUser = new User { Name = model.Username, Email = model.Email, Password = model.Password, IsActive="Yes" };
                    repository.Add_Edit_User(newUser);
                    return RedirectToAction("LoginUser", new { username = model.Username, message = "თქვენ წარმატებით გაიარეთ რეგისტრაცია. გთხოვთ გაიაროთ ავტორიზაცია" });
                }
                else
                {
                    return View("Register", model);
                }
            }
            else
            {
                return View("Register", model);
            }
        }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineTests.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "LoginUser",
                url: "Account/Login/User",
                defaults: new { controller = "Account", action = "LoginUser" }
            );

            routes.MapRoute(
                name: "LoginAdmin",
                url: "Account/Login/Admin",
                defaults: new { controller = "Account", action = "LoginAdmin" }
            );

            routes.MapRoute(
                name: "RegisterUser",
                url: "Account/Register",
                defaults: new { controller = "Account", action = "RegisterUser" }
            );

            routes.MapRoute(
                name: "AdminControlTests",
                url: "Admin/Control/Tests",
                defaults: new { controller = "Admin", action = "AdminIndex" }
                );

            routes.MapRoute(
                name: "AdminControlUsers",
                url: "Admin/Control/Users",
                defaults: new { controller = "Admin", action = "ControlUsers" }
                );

            routes.MapRoute(
                name: "AdminEditUser",
                url: "Admin/Control/Users/Edit",
                defaults: new { controller = "Admin", action = "EditUser" }
                );

            routes.MapRoute(
                null,
                "Admin/Contol/Tests/Edit_Create",
                new { controller = "Admin", action = "Edit_Create_Test"}
                );

            routes.MapRoute(
                name: "AdminEditCreateQuestion",
                url: "Admin/Contol/Tests/Edit_Create/Question",
                defaults: new { controller = "Admin", action = "Edit_Create_Question" }
                );

            routes.MapRoute(
                name: "AdminAddAnswer",
                url: "Admin/Contol/Tests/Edit_Create/Question/Edit_Create/Answer",
                defaults: new { controller = "Admin", action = "Edit_Create_Answer" }
                );

            routes.MapRoute(
                name: "AdminLogOut",
                url: "Admin/Control/LogOut",
                defaults: new { controller = "Admin", action = "LogOut" }
                );

            routes.MapRoute(
                null,
                "Test/{testID}",
                 new { controller = "Test", action = "Index" }
            );

            routes.MapRoute(
                null,
                "Tests/Search/{testname}",
                new { controller = "Tests", action = "SearchTest" }
            );

            routes.MapRoute(
                null,
                "Tests/{category}/Level{level}/{page}",
                new { controller = "Tests", action = "List" },
                new { level = @"\d+" }
            );

            routes.MapRoute(
               null,
               "Tests/Level{level}/{page}",
               new { controller = "Tests", action = "List", category=(string)null },
               new { level = @"\d+" }
           );

            routes.MapRoute(
                null,
                url: "Tests/{category}/{page}",
                defaults: new { controller = "Tests", action = "List", level=(int?)null }
            );

            routes.MapRoute(
                "MyProfile",
                url: "Profile/{UserID}",
                defaults: new { controller = "Profile", action = "Index", UserID = UrlParameter.Optional }
                );

            routes.MapRoute(
                "Raiting",
                url: "Raiting",
                defaults: new { controller = "Raiting", action = "Index" }
                );

            routes.MapRoute(
                null,
                url: "Raiting/SearchTest",
                defaults: new { controller = "Raiting", action = "SearchTestRaitings" }
                );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Tests", action = "List", category = (string)null, page = 1, level=(int?)null }
            );

            routes.MapRoute(
                name: "UserLogOut",
                url: "Tests/LogOut",
                defaults: new { controller = "Tests", action = "LogOut" }
                );


            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}

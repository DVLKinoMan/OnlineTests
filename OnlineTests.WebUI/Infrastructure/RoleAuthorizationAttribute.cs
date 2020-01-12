using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTests.WebUI.Infrastructure
{
    public class RoleAuthorizationAttribute : AuthorizeAttribute
    {
        private string RoleName;

        public RoleAuthorizationAttribute(string RoleName)
        {
            this.RoleName = RoleName;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session[RoleName] != null)
            {
                return true;
            }
            else 
            { 
                return false;
            }
        }
    }
}
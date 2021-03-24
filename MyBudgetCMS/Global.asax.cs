using MyBudgetCMS.Infrastructure;
using MyBudgetCMS.Models.Dto;
using MyBudgetCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyBudgetCMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutomappWebProfile.Run();
            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest()
        {
            // Check if user logged in
            if (User == null) { return; }

            // Get username
            string username = Context.User.Identity.Name;
            Context.User = null;
            // declare array of roles
            string[] roles = null;

            using (MyBudgetDB db = new MyBudgetDB())
            {
                // populate roles
                User dto = db.Users.Include(r => r.Roles).FirstOrDefault(x => x.Username == username);
                roles = dto.Roles.Select(x => x.RoleName).ToArray();
            }

            // Build IPrincipal object
            IIdentity userIdentity = new GenericIdentity(username);
            IPrincipal newUserObj = new GenericPrincipal(userIdentity, roles);

            // Update Context.User

            Context.User = newUserObj;
        }
    }
}

using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Services;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace MyBudgetCMS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers          
            container.RegisterType<IDashboardRepository, DashboardRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IPaymentPerMonthRepository, PaymentPerMonthRepository>();
            container.RegisterType<ITypeOfPaymentRepository, TypeOfPaymentRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IMonthlyBudgetRepository, MonthlyBudgetRepository>();
            container.RegisterType<ISearchRepository, SearchRepository>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}
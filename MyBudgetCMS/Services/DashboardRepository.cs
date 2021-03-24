using MyBudgetCMS.Enums;
using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Services
{
    public class DashboardRepository : IDashboardRepository
    {
        private MyBudgetDB context = new MyBudgetDB();

        public Dashboard GetDashboardData()
        {
            Dashboard entity = new Dashboard();

            bool ThereIsMonthlyBudget = context.MonthlyBudgets.FirstOrDefault(mb => mb.Date.Year == DateTime.Now.Year && mb.Date.Month == DateTime.Now.Month) != null;
            if (ThereIsMonthlyBudget)
            {
                entity.Income = context.MonthlyBudgets.Where(mb => mb.Date.Year == DateTime.Now.Year && mb.Date.Month == DateTime.Now.Month).Sum(x => x.Budget);
            }

            entity.FiftyPercentageNeedsList =
              context.PaymentsPerMonths.Include(ppm => ppm.Category)
              .Where(c => c.Category.TypeOfPaymentID == (int)TypeOfPayments.FiftyNeeds
               && c.ActionDate.Year == DateTime.Now.Year
               && c.ActionDate.Month == DateTime.Now.Month)
              .GroupBy(x => new { x.Category.Title })
              .Select(x =>
                   new ExpenseDashboardItem()
                   {
                       Sum = x.Sum(xx => xx.Sum),
                       Title = x.Key.Title
                   }).ToList();

            entity.ThirtyPercentageDesiresList =
                context.PaymentsPerMonths.Include(ppm => ppm.Category)
                .Where(c => c.Category.TypeOfPaymentID == (int)TypeOfPayments.ThirtyDesires
                 && c.ActionDate.Year == DateTime.Now.Year
                 && c.ActionDate.Month == DateTime.Now.Month)
                .GroupBy(x => new { x.Category.Title })
                .Select(x =>
                     new ExpenseDashboardItem()
                     {
                         Sum = x.Sum(xx => xx.Sum),
                         Title = x.Key.Title
                     }).ToList();

            entity.TwentyPercentageSavingsList =
                context.PaymentsPerMonths.Include(ppm => ppm.Category)
                .Where(c => c.Category.TypeOfPaymentID == (int)TypeOfPayments.TwentySavings
                 && c.ActionDate.Year == DateTime.Now.Year
                 && c.ActionDate.Month == DateTime.Now.Month)
                .GroupBy(x => new { x.Category.Title })
                .Select(x =>
                     new ExpenseDashboardItem()
                     {
                         Sum = x.Sum(xx => xx.Sum),
                         Title = x.Key.Title
                     }).ToList();

            entity.expense = (entity.FiftyPercentageNeedsList.Sum(x => x.Sum) + entity.ThirtyPercentageDesiresList.Sum(x => x.Sum) + entity.TwentyPercentageSavingsList.Sum(x => x.Sum));
            entity.CurrentBudget = entity.Income - entity.expense;

            return entity;
        }
    }
}
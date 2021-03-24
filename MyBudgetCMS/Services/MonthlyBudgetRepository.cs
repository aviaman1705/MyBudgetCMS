using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Dto;
using MyBudgetCMS.Models.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Services
{
    public class MonthlyBudgetRepository : IMonthlyBudgetRepository
    {
        private MyBudgetDB context = new MyBudgetDB();

        public void Add(MonthlyBudget item)
        {
            context.MonthlyBudgets.Add(item);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            MonthlyBudget entity = context.MonthlyBudgets.FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                context.MonthlyBudgets.Remove(entity);
                context.SaveChanges();
                isDeleted = true;
            }
            return isDeleted;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public MonthlyBudget Get(int id)
        {
            return context.MonthlyBudgets.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<MonthlyBudget> GetAll()
        {
            return context.MonthlyBudgets;
        }

        public bool Update(MonthlyBudget item)
        {
            bool isUpdated = false;
            MonthlyBudget entity = context.MonthlyBudgets.FirstOrDefault(p => p.Id == item.Id);
            if (entity != null)
            {
                entity.Budget = item.Budget;
                entity.Date = item.Date;
                entity.ShortDesc = item.ShortDesc;
                context.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }
    }
}
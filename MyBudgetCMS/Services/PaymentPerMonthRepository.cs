using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Services
{
    public class PaymentPerMonthRepository : IPaymentPerMonthRepository
    {
        private MyBudgetDB context = new MyBudgetDB();

        public void Add(PaymentPerMonth item)
        {
            context.PaymentsPerMonths.Add(item);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            PaymentPerMonth entity = context.PaymentsPerMonths.FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                context.PaymentsPerMonths.Remove(entity);
                context.SaveChanges();
                isDeleted = true;
            }
            return isDeleted;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public PaymentPerMonth Get(int id)
        {
            return context.PaymentsPerMonths.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<PaymentPerMonth> GetAll()
        {
            return context.PaymentsPerMonths.Include(p => p.Category);
        }

        public bool Update(PaymentPerMonth item)
        {
            bool isUpdated = false;
            PaymentPerMonth entity = context.PaymentsPerMonths.FirstOrDefault(p => p.Id == item.Id);
            if (entity != null)
            {
                entity.CategoryID = item.CategoryID;                
                entity.Sum = item.Sum;
                context.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }
    }
}
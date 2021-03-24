using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Services
{
    public class TypeOfPaymentRepository : ITypeOfPaymentRepository
    {
        private MyBudgetDB context = new MyBudgetDB();

        public void Add(TypeOfPayment item)
        {
            context.TypesOfPayments.Add(item);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            TypeOfPayment entity = context.TypesOfPayments.FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                context.TypesOfPayments.Remove(entity);
                context.SaveChanges();
                isDeleted = true;
            }
            return isDeleted;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public TypeOfPayment Get(int id)
        {
            return context.TypesOfPayments.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<TypeOfPayment> GetAll()
        {
            return context.TypesOfPayments;
        }

        public bool Update(TypeOfPayment item)
        {
            bool isUpdated = false;
            TypeOfPayment entity = context.TypesOfPayments.FirstOrDefault(p => p.Id == item.Id);
            if (entity != null)
            {
                entity.Code = item.Code;
                entity.Title = item.Title;
                context.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }
    }
}
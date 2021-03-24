using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private MyBudgetDB context = new MyBudgetDB();

        public void Add(Category item)
        {
            context.Categories.Add(item);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            Category entity = context.Categories.FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                context.Categories.Remove(entity);
                context.SaveChanges();
                isDeleted = true;
            }
            return isDeleted;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Category Get(int id)
        {
            return context.Categories.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories;
        }

        public bool Update(Category item)
        {
            bool isUpdated = false;
            Category entity = context.Categories.FirstOrDefault(p => p.Id == item.Id);
            if (entity != null)
            {
                entity.Title = item.Title;
                entity.ParentID = item.ParentID;
                entity.TypeOfPaymentID = item.TypeOfPaymentID;
                context.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }
    }
}
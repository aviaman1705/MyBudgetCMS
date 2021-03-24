using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Dto;
using MyBudgetCMS.Models.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Services
{
    public class UserRepository : IUserRepository
    {
        private MyBudgetDB context = new MyBudgetDB();
        public void Add(User item)
        {
            context.Users.Add(item);
            context.SaveChanges();
        }

        public void AddUserRole(UserRole item)
        {
            //context.UserRoles.Add(item);
            //context.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            User entity = context.Users.FirstOrDefault(p => p.UserId == id);
            if (entity != null)
            {
                context.Users.Remove(entity);
                context.SaveChanges();
                isDeleted = true;
            }
            return isDeleted;
        }

        public User Get(int id)
        {
            return context.Users.Include(x => x.Roles).FirstOrDefault(p => p.UserId == id);
        }

        public User Get(string username)
        {
            return context.Users.Include(x => x.Roles).FirstOrDefault(p => p.Username == username);
        }

        public IQueryable<User> GetAll()
        {
            return context.Users.Include(x => x.Roles);
        }

        public bool Update(User item)
        {
            bool isUpdated = false;
            User entity = context.Users.FirstOrDefault(p => p.UserId == item.UserId);
            if (entity != null)
            {
                entity.Email = item.Email;
                entity.FirstName = item.FirstName;
                entity.IsActive = item.IsActive;
                entity.LastName = item.LastName;
                entity.Password = item.Password;
                entity.Roles = item.Roles;
                entity.Username = item.Username;
                context.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
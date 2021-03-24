using MyBudgetCMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Entities
{
    public class MyBudgetDB: DbContext
    {
        public MyBudgetDB() : base("MyBudgetDB") { }

        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MonthlyBudget> MonthlyBudgets { get; set; }
        public DbSet<PaymentPerMonth> PaymentsPerMonths { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TypeOfPayment> TypesOfPayments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MyBudgetDB>(null);
        }
    }
}
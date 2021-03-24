namespace MyBudgetCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRealenseshipbetweenmontlbudgetandpaymentpermethoud : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentPerMonths", "MonthlyBudgetID", "dbo.MonthlyBudgets");
            DropIndex("dbo.PaymentPerMonths", new[] { "MonthlyBudgetID" });
            DropColumn("dbo.PaymentPerMonths", "MonthlyBudgetID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentPerMonths", "MonthlyBudgetID", c => c.Int(nullable: false));
            CreateIndex("dbo.PaymentPerMonths", "MonthlyBudgetID");
            AddForeignKey("dbo.PaymentPerMonths", "MonthlyBudgetID", "dbo.MonthlyBudgets", "Id", cascadeDelete: true);
        }
    }
}

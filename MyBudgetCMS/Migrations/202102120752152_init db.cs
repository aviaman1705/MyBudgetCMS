namespace MyBudgetCMS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ParentID = c.Int(nullable: false),
                        TypeOfPaymentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ParentID)
                .ForeignKey("dbo.TypeOfPayments", t => t.TypeOfPaymentID, cascadeDelete: true)
                .Index(t => t.ParentID)
                .Index(t => t.TypeOfPaymentID);
            
            CreateTable(
                "dbo.PaymentPerMonths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MonthlyBudgetID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.MonthlyBudgets", t => t.MonthlyBudgetID, cascadeDelete: true)
                .Index(t => t.MonthlyBudgetID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.MonthlyBudgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Slug = c.String(),
                        Body = c.String(),
                        Sorting = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        MetaKeywords = c.String(),
                        MetaDescription = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Sentences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Role_RoleId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Role_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Categories", "TypeOfPaymentID", "dbo.TypeOfPayments");
            DropForeignKey("dbo.PaymentPerMonths", "MonthlyBudgetID", "dbo.MonthlyBudgets");
            DropForeignKey("dbo.PaymentPerMonths", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentID", "dbo.Categories");
            DropIndex("dbo.UserRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.UserRoles", new[] { "User_UserId" });
            DropIndex("dbo.PaymentPerMonths", new[] { "CategoryID" });
            DropIndex("dbo.PaymentPerMonths", new[] { "MonthlyBudgetID" });
            DropIndex("dbo.Categories", new[] { "TypeOfPaymentID" });
            DropIndex("dbo.Categories", new[] { "ParentID" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Sentences");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Pages");
            DropTable("dbo.TypeOfPayments");
            DropTable("dbo.MonthlyBudgets");
            DropTable("dbo.PaymentPerMonths");
            DropTable("dbo.Categories");
        }
    }
}

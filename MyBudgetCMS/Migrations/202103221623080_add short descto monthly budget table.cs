namespace MyBudgetCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addshortdesctomonthlybudgettable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonthlyBudgets", "ShortDesc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MonthlyBudgets", "ShortDesc");
        }
    }
}

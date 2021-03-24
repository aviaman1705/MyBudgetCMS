namespace MyBudgetCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Addactiontypeculmanforpaymentpermethoudtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentPerMonths", "ActionDate", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.PaymentPerMonths", "ActionDate");
        }
    }
}

namespace DataDrivenViewEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDbForm1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataForms", "SubmitUrl", c => c.String());
            AddColumn("dbo.DataForms", "SubmitName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DataForms", "SubmitName");
            DropColumn("dbo.DataForms", "SubmitUrl");
        }
    }
}

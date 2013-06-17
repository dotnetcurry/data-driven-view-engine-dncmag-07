namespace DataDrivenViewEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Template = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldName = c.String(nullable: false),
                        DisplayLabel = c.String(nullable: false),
                        DisplayType = c.Int(nullable: false),
                        IsMandatory = c.Boolean(nullable: false),
                        FormId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DataForms", t => t.FormId, cascadeDelete: true)
                .Index(t => t.FormId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DataFields", new[] { "FormId" });
            DropForeignKey("dbo.DataFields", "FormId", "dbo.DataForms");
            DropTable("dbo.DataFields");
            DropTable("dbo.DataForms");
        }
    }
}

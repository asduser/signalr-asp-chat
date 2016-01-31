namespace SignalR_app.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegisteredFromField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RegisteredFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "RegisteredFrom");
        }
    }
}

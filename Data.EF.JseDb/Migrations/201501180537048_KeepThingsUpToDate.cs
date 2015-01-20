namespace Data.EF.JseDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeepThingsUpToDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Jobs", "Job_Id", c => c.Int());
            CreateIndex("dbo.Jobs", "Job_Id");
            AddForeignKey("dbo.Jobs", "Job_Id", "dbo.Jobs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "Job_Id", "dbo.Jobs");
            DropIndex("dbo.Jobs", new[] { "Job_Id" });
            DropColumn("dbo.Jobs", "Job_Id");
            DropColumn("dbo.Jobs", "Discriminator");
        }
    }
}

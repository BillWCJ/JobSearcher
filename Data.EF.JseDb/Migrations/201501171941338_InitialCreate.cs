namespace Data.EF.JseDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Discipline1 = c.Byte(nullable: false),
                        Discipline2 = c.Byte(nullable: false),
                        Discipline3 = c.Byte(nullable: false),
                        Discipline4 = c.Byte(nullable: false),
                        Discipline5 = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployerReviews",
                c => new
                    {
                        EmployerId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.EmployerId);
            
            CreateTable(
                "dbo.JobReviews",
                c => new
                    {
                        JobReviewId = c.Int(nullable: false),
                        Title = c.String(),
                        Location = c.String(),
                        AverageSalary = c.String(),
                        SalaryRange = c.String(),
                        NumberOfReviews = c.Int(nullable: false),
                        Popularity = c.Int(nullable: false),
                        JobDescription = c.String(),
                        AverageRating = c.Int(nullable: false),
                        EmployerReview_EmployerId = c.Int(),
                    })
                .PrimaryKey(t => t.JobReviewId)
                .ForeignKey("dbo.EmployerReviews", t => t.EmployerReview_EmployerId)
                .Index(t => t.EmployerReview_EmployerId);
            
            CreateTable(
                "dbo.JobRatings",
                c => new
                    {
                        JobRatingId = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Rating = c.Double(nullable: false),
                        Date = c.String(),
                        Salary = c.String(),
                        JobReview_JobReviewId = c.Int(),
                    })
                .PrimaryKey(t => t.JobRatingId)
                .ForeignKey("dbo.JobReviews", t => t.JobReview_JobReviewId)
                .Index(t => t.JobReview_JobReviewId);
            
            CreateTable(
                "dbo.Employers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UnitName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        JobTitle = c.String(),
                        Comment = c.String(),
                        JobDescription = c.String(),
                        Disciplines_Id = c.Int(),
                        Employer_Id = c.Int(),
                        JobLocation_Id = c.Int(),
                        Levels_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplines", t => t.Disciplines_Id)
                .ForeignKey("dbo.Employers", t => t.Employer_Id)
                .ForeignKey("dbo.JobLocations", t => t.JobLocation_Id)
                .ForeignKey("dbo.Levels", t => t.Levels_Id)
                .Index(t => t.Disciplines_Id)
                .Index(t => t.Employer_Id)
                .Index(t => t.JobLocation_Id)
                .Index(t => t.Levels_Id);
            
            CreateTable(
                "dbo.JobLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullAddress = c.String(),
                        Region = c.String(),
                        Longitude = c.Decimal(precision: 18, scale: 10),
                        Latitude = c.Decimal(precision: 18, scale: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsJunior = c.Boolean(nullable: false),
                        IsIntermediate = c.Boolean(nullable: false),
                        IsSenior = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocationOfInterests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FullAddress = c.String(),
                        Region = c.String(),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SearchDictionaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        WordId = c.Int(nullable: false),
                        JobTitleCount = c.Int(nullable: false),
                        CommentCount = c.Int(nullable: false),
                        DescriptionCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.Words", t => t.WordId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.WordId);
            
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        WordId = c.Int(nullable: false, identity: true),
                        DictionaryWord = c.String(),
                        Description = c.String(),
                        Catergory = c.String(),
                    })
                .PrimaryKey(t => t.WordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SearchDictionaries", "WordId", "dbo.Words");
            DropForeignKey("dbo.SearchDictionaries", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "Levels_Id", "dbo.Levels");
            DropForeignKey("dbo.Jobs", "JobLocation_Id", "dbo.JobLocations");
            DropForeignKey("dbo.Jobs", "Employer_Id", "dbo.Employers");
            DropForeignKey("dbo.Jobs", "Disciplines_Id", "dbo.Disciplines");
            DropForeignKey("dbo.JobRatings", "JobReview_JobReviewId", "dbo.JobReviews");
            DropForeignKey("dbo.JobReviews", "EmployerReview_EmployerId", "dbo.EmployerReviews");
            DropIndex("dbo.SearchDictionaries", new[] { "WordId" });
            DropIndex("dbo.SearchDictionaries", new[] { "JobId" });
            DropIndex("dbo.Jobs", new[] { "Levels_Id" });
            DropIndex("dbo.Jobs", new[] { "JobLocation_Id" });
            DropIndex("dbo.Jobs", new[] { "Employer_Id" });
            DropIndex("dbo.Jobs", new[] { "Disciplines_Id" });
            DropIndex("dbo.JobRatings", new[] { "JobReview_JobReviewId" });
            DropIndex("dbo.JobReviews", new[] { "EmployerReview_EmployerId" });
            DropTable("dbo.Words");
            DropTable("dbo.SearchDictionaries");
            DropTable("dbo.LocationOfInterests");
            DropTable("dbo.Levels");
            DropTable("dbo.JobLocations");
            DropTable("dbo.Jobs");
            DropTable("dbo.Employers");
            DropTable("dbo.JobRatings");
            DropTable("dbo.JobReviews");
            DropTable("dbo.EmployerReviews");
            DropTable("dbo.Disciplines");
        }
    }
}

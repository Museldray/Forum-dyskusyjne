namespace Forum_dyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelsCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 70),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 70),
                        Text = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Moderators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 70),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ThreadId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Threads", t => t.ThreadId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ThreadId);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Source = c.String(nullable: false, maxLength: 70),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 80),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        IsPinned = c.Boolean(nullable: false),
                        OwnerId = c.String(),
                        SubjectId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.SubjectId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.MessageUsers",
                c => new
                    {
                        MessageId = c.Int(nullable: false),
                        ReceiverId = c.String(nullable: false, maxLength: 128),
                        SenderId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MessageId, t.ReceiverId, t.SenderId })
                .ForeignKey("dbo.Messages", t => t.MessageId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId, cascadeDelete: true)
                .Index(t => t.MessageId)
                .Index(t => t.SenderId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 70),
                        Text = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Friend_Id = c.String(nullable: false, maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Friend_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Friend_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 70),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.AspNetUsers", "Rank", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Avatar", c => c.String());
            AddColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Privileges", c => c.String());
            AddColumn("dbo.AspNetUsers", "IfAdminChangedRank", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PostsOnPage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "Friend_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Moderators", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MessageUsers", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MessageUsers", "MessageId", "dbo.Messages");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Threads", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Threads", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Posts", "ThreadId", "dbo.Threads");
            DropForeignKey("dbo.Attachments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Moderators", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "CategoryId", "dbo.Categories");
            DropIndex("dbo.News", new[] { "UserId" });
            DropIndex("dbo.Friends", new[] { "User_Id" });
            DropIndex("dbo.Friends", new[] { "Friend_Id" });
            DropIndex("dbo.MessageUsers", new[] { "SenderId" });
            DropIndex("dbo.MessageUsers", new[] { "MessageId" });
            DropIndex("dbo.Threads", new[] { "User_Id" });
            DropIndex("dbo.Threads", new[] { "SubjectId" });
            DropIndex("dbo.Attachments", new[] { "PostId" });
            DropIndex("dbo.Posts", new[] { "ThreadId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Moderators", new[] { "SubjectId" });
            DropIndex("dbo.Moderators", new[] { "UserId" });
            DropIndex("dbo.Subjects", new[] { "CategoryId" });
            DropColumn("dbo.AspNetUsers", "PostsOnPage");
            DropColumn("dbo.AspNetUsers", "IfAdminChangedRank");
            DropColumn("dbo.AspNetUsers", "Privileges");
            DropColumn("dbo.AspNetUsers", "RegistrationDate");
            DropColumn("dbo.AspNetUsers", "Avatar");
            DropColumn("dbo.AspNetUsers", "Rank");
            DropTable("dbo.News");
            DropTable("dbo.Friends");
            DropTable("dbo.Messages");
            DropTable("dbo.MessageUsers");
            DropTable("dbo.Threads");
            DropTable("dbo.Attachments");
            DropTable("dbo.Posts");
            DropTable("dbo.Moderators");
            DropTable("dbo.Subjects");
            DropTable("dbo.Categories");
        }
    }
}

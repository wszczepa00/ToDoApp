namespace Calendar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.DateTime(nullable: false),
                        Mood = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Beggining = c.Time(nullable: false, precision: 7),
                        End = c.Time(nullable: false, precision: 7),
                        Done = c.Boolean(nullable: false),
                        DayId = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Days", t => t.DayId, cascadeDelete: true)
                .Index(t => t.DayId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "DayId", "dbo.Days");
            DropIndex("dbo.Events", new[] { "DayId" });
            DropTable("dbo.Events");
            DropTable("dbo.Days");
        }
    }
}

namespace Incubators.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedevents : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IncubatorMeasures", "IncubatorPeriodId", "dbo.IncubatorPeriods");
            DropIndex("dbo.IncubatorMeasures", new[] { "IncubatorPeriodId" });
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IncubatorId = c.Int(nullable: false),
                        Title = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incubators", t => t.IncubatorId, cascadeDelete: true)
                .Index(t => t.IncubatorId);
            
            AlterColumn("dbo.IncubatorMeasures", "IncubatorPeriodId", c => c.Int());
            CreateIndex("dbo.IncubatorMeasures", "IncubatorPeriodId");
            AddForeignKey("dbo.IncubatorMeasures", "IncubatorPeriodId", "dbo.IncubatorPeriods", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncubatorMeasures", "IncubatorPeriodId", "dbo.IncubatorPeriods");
            DropForeignKey("dbo.Events", "IncubatorId", "dbo.Incubators");
            DropIndex("dbo.IncubatorMeasures", new[] { "IncubatorPeriodId" });
            DropIndex("dbo.Events", new[] { "IncubatorId" });
            AlterColumn("dbo.IncubatorMeasures", "IncubatorPeriodId", c => c.Int(nullable: false));
            DropTable("dbo.Events");
            CreateIndex("dbo.IncubatorMeasures", "IncubatorPeriodId");
            AddForeignKey("dbo.IncubatorMeasures", "IncubatorPeriodId", "dbo.IncubatorPeriods", "Id", cascadeDelete: true);
        }
    }
}

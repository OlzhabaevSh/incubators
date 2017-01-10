namespace Incubators.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_dates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Incubators", "StartedOn", c => c.DateTime());
            AlterColumn("dbo.Incubators", "FinishingOn", c => c.DateTime());
            AlterColumn("dbo.IncubatorMeasures", "MeasuredOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IncubatorMeasures", "MeasuredOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Incubators", "FinishingOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Incubators", "StartedOn", c => c.DateTime(nullable: false));
        }
    }
}

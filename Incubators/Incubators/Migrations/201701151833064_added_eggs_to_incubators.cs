namespace Incubators.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_eggs_to_incubators : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Incubators", "Eggs", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Incubators", "Eggs");
        }
    }
}

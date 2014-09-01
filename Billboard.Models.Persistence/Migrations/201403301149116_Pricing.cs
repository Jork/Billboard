using System.Data.Entity.Migrations;

namespace Billboard.Models.Persistence.Migrations
{
	public partial class Pricing
		: DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Note", "Price", c => c.Decimal(precision: 18, scale: 2));
		}

		public override void Down()
		{
			DropColumn("dbo.Note", "Price");
		}
	}
}

using System.Data.Entity.Migrations;

namespace Billboard.Models.Persistence.Migrations
{
	/// <summary>
	/// Upgrades the datamodel to add a Note table to it.
	/// </summary>
	public partial class Notes
		: DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Note",
				c => new
				{
					Id = c.Guid(nullable: false),
					CreationDateTime = c.DateTime(nullable: false),
					Title = c.String(nullable: false, maxLength: 50),
					Message = c.String(nullable: false),
					EmailAddress = c.String(nullable: false, maxLength: 254),
				})
				.PrimaryKey(t => t.Id);

		}

		public override void Down()
		{
			DropTable("dbo.Note");
		}
	}
}

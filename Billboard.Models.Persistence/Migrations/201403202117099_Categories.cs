using System.Data.Entity.Migrations;

namespace Billboard.Models.Persistence.Migrations
{
	public partial class Categories
		: DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Category",
				c => new
				{
					Id = c.Guid(nullable: false),
					Name = c.String(nullable: false, maxLength: 50),
				})
				.PrimaryKey(t => t.Id)
				.Index(t => t.Name, unique: true);

			AddColumn("dbo.Note", "Category_Id", c => c.Guid());
			CreateIndex("dbo.Note", "Category_Id");
			AddForeignKey("dbo.Note", "Category_Id", "dbo.Category", "Id");
		}

		public override void Down()
		{
			DropForeignKey("dbo.Note", "Category_Id", "dbo.Category");
			DropIndex("dbo.Note", new[] { "Category_Id" });
			DropColumn("dbo.Note", "Category_Id");
			DropTable("dbo.Category");
		}
	}
}

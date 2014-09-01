using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Billboard.Models.Persistence.Migrations
{
	[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Will be instantiated by Migrations")]
	internal sealed class Configuration
		: DbMigrationsConfiguration<UnitOfWork>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(UnitOfWork context)
		{
			string seedFilePath = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data", "demo-seed.xml");

			if (File.Exists(seedFilePath))
			{
				XDocument doc = XDocument.Load(seedFilePath);

				XElement seedElement = doc.Element("seed");
				ReadSeedData(context, seedElement);
			}
		}

		private void ReadSeedData(UnitOfWork context, XElement seedElement)
		{
			if (seedElement != null)
			{
				IEnumerable<XElement> categoryElements = seedElement.Elements("category");
				foreach (XElement categoryElement in categoryElements)
				{
					ReadCategoryData(context, categoryElement);
				}
			}
		}

		private void ReadCategoryData(UnitOfWork context, XElement categoryElement)
		{
			var catName = (string)categoryElement.Attribute("name");
			Category category = Create(context, catName);

			IEnumerable<XElement> noteElements = categoryElement.Elements("note");
			foreach (XElement noteElement in noteElements)
			{
				ReadNoteData(context, noteElement, category);
			}
		}

		private void ReadNoteData(UnitOfWork context, XElement noteElement, Category category)
		{
			var title = (string)noteElement.Attribute("title");
			var email = (string)noteElement.Attribute("email");
			var price = (decimal?)noteElement.Attribute("price");
			var message = (string)noteElement.Value;

			Create(context, category, title, message, price, email);
		}

		private Category Create(UnitOfWork work, string categoryName)
		{
			Category category = work.Categories.SingleOrDefault(c => c.Name == categoryName);
			if (category == null)
			{
				category = new Category(categoryName);
				work.Categories.Add(category);
			}
			return category;
		}

		private Note Create(UnitOfWork work, Category category, string noteTitle, string noteMessage, decimal? price, string emailAddress)
		{
			Note note = work.Notes.SingleOrDefault(n => n.Title == noteTitle);
			if (note == null)
			{
				note = new Note(noteTitle, noteMessage, price, new EmailAddress(emailAddress));
				category.Notes.Add(note);
				work.Notes.Add(note);
			}

			return note;
		}
	}
}

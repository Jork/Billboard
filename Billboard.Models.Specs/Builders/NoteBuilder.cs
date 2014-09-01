using Beaker.TechDays.Billboard.Models.Specs.Helpers;
using Billboard.Models;

namespace Beaker.TechDays.Billboard.Models.Specs.Builders
{
	public sealed class NoteBuilder
	{
		private Maybe<string> _title;
		private Maybe<string> _message;
		private Maybe<EmailAddress> _email;
		private Maybe<Category> _category;

		public NoteBuilder WithTitle(string title)
		{
			_title = title;
			return this;
		}

		public NoteBuilder WithMessage(string message)
		{
			_message = message;
			return this;
		}

		public NoteBuilder WithEmailAddress(EmailAddress emailAddress)
		{
			_email = emailAddress;
			return this;
		}

		public NoteBuilder InCategory(Category category)
		{
			_category = category;
			return this;
		}

		public Note Build()
		{
			string title = _title.GetValueOrDefault(Some.String());
			string message = _message.GetValueOrDefault(Some.Sentence());
			EmailAddress email = _email.GetValueOrDefault(new EmailAddress(Some.EmailAddress()));
			Category category = _category.GetValueOrDefault();

			var note =  new Note(title, message, null, email);
			
			if( category != null )
				category.Notes.Add(note);

			return note;
		}

	}
}
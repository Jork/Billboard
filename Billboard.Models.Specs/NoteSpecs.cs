using System;
using Beaker.TechDays.Billboard.Models.Specs.Builders;
using Billboard.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Beaker.TechDays.Billboard.Models.Specs
{
	[TestClass]
	public class NoteSpecs
	{
		[TestMethod]
		public void WhenCreatingAnNewNoteWithNullForTitle_ThenShouldThrowArgumentNullException()
		{
			// arrange
			NoteBuilder subjectBuilder =
				new NoteBuilder()
					.WithTitle(null);

			// act
			Action act = () => subjectBuilder.Build();

			// assert
			act.ShouldThrow<ArgumentNullException>();
		}

		[TestMethod]
		public void WhenCreatingAnNewNoteWithNullForMessage_ThenShouldThrowArgumentNullException()
		{
			// arrange
			NoteBuilder subjectBuilder =
				new NoteBuilder()
					.WithMessage(null);

			// act
			Action act = () => subjectBuilder.Build();

			// assert
			act.ShouldThrow<ArgumentNullException>();
		}


		[TestMethod]
		public void GiveAnNewlyCreatedNote_WhenGettingCreatingDate_ThenValueShouldBeAroundConstructionTime()
		{
			// arrange
			DateTime before = DateTime.Now;
			Note subject =
				new NoteBuilder()
					.WithTitle("title")
					.Build();
			DateTime after = DateTime.Now;

			// act
			DateTime result = subject.CreationDateTime;

			// assert
			result.
				Should().BeOnOrAfter(before)
				.And.BeOnOrBefore(after);
		}

		[TestMethod]
		public void GiveAnNewlyCreatedNote_WhenGettingTitle_ThenValueShouldBeSameAsPassedInConstructor()
		{
			// arrange
			Note subject =
				new NoteBuilder()
					.WithTitle("title")
					.Build();

			// act
			string result = subject.Title;

			// assert
			result.Should().Be("title");
		}

		[TestMethod]
		public void GiveAnNewlyCreatedNote_WhenGettingMessage_ThenValueShouldBeSameAsPassedInConstructor()
		{
			// arrange
			Note subject =
				new NoteBuilder()
					.WithMessage("this is my message")
					.Build();

			// act
			string result = subject.Message;

			// assert
			result.Should().Be("this is my message");
		}

		[TestMethod]
		public void GiveAnNewlyCreatedNote_WhenGettingEmailAddress_ThenValueShouldBeSameAsPassedInConstructor()
		{
			// arrange
			Note subject =
				new NoteBuilder()
					.WithEmailAddress(new EmailAddress("jeroen.smits@prodware.nl"))
					.Build();

			// act
			EmailAddress result = subject.EmailAddress;

			// assert
			result.Should().Be(new EmailAddress("jeroen.smits@prodware.nl"));
		}
	}
}

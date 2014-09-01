using System;
using Billboard.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Beaker.TechDays.Billboard.Models.Specs
{
	[TestClass]
	public sealed class EmailAddressSpecs
	{
		[TestMethod]
		public void WhenConstructingWithInvalidEmailAdres_ThenShouldThrowFormatException()
		{
			// act
			Action act = () => new EmailAddress("no.at.sign");

			// assert
			act.ShouldThrow<FormatException>();
		}

		[TestMethod]
		public void WhenConstructingWithValidEmailAdres_ThenShouldNotThrow()
		{
			// act
			Action act = () => new EmailAddress("this.is@valid.adr");

			// assert
			act.ShouldNotThrow();
		}

		[TestMethod]
		public void WhenGivenAnEmailWithUpperCases_WhenMatchingSameEmailWithLowerCase_ThenShouldMatch()
		{
			// arrange
			var lower = new EmailAddress("my.email@prodware.nl");
			var upper = new EmailAddress("MY.EMAIL@prodware.nl");

			// act
			var result = lower.Equals(upper);

			// assert
			result.Should().BeTrue();
		}
	}
}

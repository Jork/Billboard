using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace Billboard.Models
{
	/// <summary>
	/// A valid email address.
	/// </summary>
	public struct EmailAddress
		: IEquatable<EmailAddress>, IEquatable<string>
	{
		// official W3C RFC2822 email regex.
		public static readonly Regex EmailRegex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

		/// <summary>
		/// An email address without any value.
		/// </summary>
		public static readonly EmailAddress None = new EmailAddress();

		private readonly string _value;

		/// <summary>
		/// Create a new valid email address.
		/// </summary>
		/// <param name="emailAddress">The email adres.</param>
		/// <exception cref="FormatException">Throw when the given string is not a valid email address.</exception>
		public EmailAddress(string emailAddress)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(emailAddress));
			Contract.Requires<ArgumentOutOfRangeException>(emailAddress.Length <= 254, "Email has a maximum length of 254.");
			Contract.Requires<FormatException>(emailAddress == null || EmailRegex.IsMatch(emailAddress));
			_value = emailAddress;
		}

		[ContractInvariantMethod]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
		private void ObjectInvariant()
		{
			Contract.Invariant(_value == null || EmailRegex.IsMatch(_value));
		}

		/// <summary>
		/// Gets the value of the email address.
		/// </summary>
		/// <returns>The email address.</returns>
		public string Value
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() == null || EmailRegex.IsMatch(Contract.Result<string>()));
				Contract.Ensures(Contract.Result<string>() == null || Contract.Result<string>().Length <= 254);
				return _value;
			}
		}

		/// <summary>
		/// Compares if the given object equals this email address.
		/// </summary>
		/// <param name="obj">The object to compare against this email address.</param>
		/// <returns><c>true</c> if both email addresses are the same, otherwise <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			if (obj is EmailAddress)
				return Equals((EmailAddress)obj);

			var s = obj as string;
			if (s != null)
				return Equals(s);

			return base.Equals(obj);
		}

		/// <summary>
		/// Compares if the given email address is the same as this email address.
		/// </summary>
		/// <param name="other">The other email address to compare against this email address.</param>
		/// <returns><c>true</c> if both email addresses are the same, otherwise <c>false</c>.</returns>
		public bool Equals(EmailAddress other)
		{
			return Equals(other._value);
		}

		/// <summary>
		/// Compares if the given email address is the same as this email address.
		/// </summary>
		/// <param name="other">The other email address to compare against this email address.</param>
		/// <returns><c>true</c> if both email addresses are the same, otherwise <c>false</c>.</returns>
		public bool Equals(string other)
		{
			// officialy the local part of the e-mail adres is case censitive,
			// but in practice it is not. We will handle it as totally case incensitive for comparisons.
			return StringComparer.OrdinalIgnoreCase.Equals(_value, other);
		}

		/// <summary>
		/// Compares if the two email addresses are the same.
		/// </summary>
		/// <param name="left">The email address at the left side of the == operator.</param>
		/// <param name="right">The email address at the right side of the == operator.</param>
		/// <returns><c>true</c> if both email addresses are the same, otherwise <c>false</c>.</returns>
		public static bool operator ==(EmailAddress left, EmailAddress right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Compares if the two email addresses are the same.
		/// </summary>
		/// <param name="left">The email address at the left side of the == operator.</param>
		/// <param name="right">The email address at the right side of the == operator.</param>
		/// <returns><c>true</c> if both email addresses are the same, otherwise <c>false</c>.</returns>
		public static bool operator ==(EmailAddress left, string right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Compares if the two email addresses are the different.
		/// </summary>
		/// <param name="left">The email address at the left side of the == operator.</param>
		/// <param name="right">The email address at the right side of the == operator.</param>
		/// <returns><c>true</c> if both email addresses are the different, otherwise <c>false</c>.</returns>
		public static bool operator !=(EmailAddress left, EmailAddress right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// Compares if the two email addresses are the different.
		/// </summary>
		/// <param name="left">The email address at the left side of the == operator.</param>
		/// <param name="right">The email address at the right side of the == operator.</param>
		/// <returns><c>true</c> if both email addresses are the different, otherwise <c>false</c>.</returns>
		public static bool operator !=(EmailAddress left, string right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// Get the hashcode for this email address
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ -287487297;
		}

		/// <summary>
		/// Converts the email address to a string.
		/// </summary>
		/// <returns>The email address.</returns>
		public override string ToString()
		{
			return _value ?? string.Empty;
		}

		/// <summary>
		/// Explicit cast from <see cref="EmailAddress"/> to a string with the value of the email address.
		/// </summary>
		/// <param name="emailAddress">The email address to cast to a string.</param>
		/// <returns>A string with the value of the email address.</returns>
		public static explicit operator string(EmailAddress emailAddress)
		{
			Contract.Ensures(Contract.Result<string>() == null || EmailRegex.IsMatch(Contract.Result<string>()));
			return emailAddress._value;
		}

		/// <summary>
		/// Explicit cast from a string to an <see cref="EmailAddress"/>.
		/// </summary>
		/// <param name="emailAddress">The string to convert into an <see cref="EmailAddress"/>.</param>
		/// <returns>An <see cref="EmailAddress"/> with the value of the passed string.</returns>
		public static explicit operator EmailAddress(string emailAddress)
		{
			Contract.Requires<FormatException>(emailAddress == null || EmailRegex.IsMatch(emailAddress));

			if (emailAddress == null)
				return None;

			return new EmailAddress(emailAddress);
		}

	}
}
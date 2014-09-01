using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Billboard.Models
{
	/// <summary>
	/// A single note for on the board.
	/// </summary>
	public class Note
		: Entity
	{
		private DateTime _creationDateTime;
		private string _title;
		private string _message;
		private decimal? _price;
		private EmailAddress _emailAddress;
		private Category _category;

		/// <summary>
		/// Create a new note
		/// </summary>
		/// <param name="title">The title of the note.</param>
		/// <param name="message">The message of the note.</param>
		/// <param name="price">The optional price of the item on the note.</param>
		/// <param name="emailAddress">The email address of the person that placed the note.</param>
		public Note(string title, string message, decimal? price, EmailAddress emailAddress)
		{
			Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(title));
			Contract.Requires<ArgumentOutOfRangeException>(title.Length <= 50, "Title has a maximum length of 50.");
			Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(message));
			Contract.Requires<ArgumentOutOfRangeException>(price == null || price >= 0m);
			Contract.Requires<ArgumentException>(emailAddress != EmailAddress.None);

			_creationDateTime = DateTime.Now;
			_title = title;
			_message = message;
			_price = price;
			_emailAddress = emailAddress;
		}

		internal protected Note()
			: base()
		{
		}

		/// <summary>
		/// Gets the creation date of this notition.
		/// </summary>
		public DateTime CreationDateTime
		{
			get { return _creationDateTime; }
			private set { _creationDateTime = value; }
		}

		/// <summary>
		/// Gets the title of this notition.
		/// </summary>
		[Required]
		[StringLength(50)]
		public string Title
		{
			get
			{
				Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));
				Contract.Ensures(Contract.Result<string>().Length <= 50);
				return _title;
			}
			set
			{
				Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(value));
				Contract.Requires<ArgumentOutOfRangeException>(value.Length <= 50);
				_title = value;
			}
		}

		/// <summary>
		/// Gets or sets the message of this notition.
		/// </summary>
		[Required]
		public string Message
		{
			get
			{
				Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));
				return _message;
			}
			set
			{
				Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(value));
				_message = value;
			}
		}

		/// <summary>
		/// Gets or sets the email adres of the user that placed this notition.
		/// </summary>
		public EmailAddress EmailAddress
		{
			get { return _emailAddress; }
			set { _emailAddress = value; }
		}

		[Browsable(false)]
		[Required]
		[StringLength(254)] // RFC-5321, max email address length == 256, including quotes, thus 254 for email without any quotes.
		private string EmailAddressEF
		{
			get
			{
				Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));
				Contract.Assume(_emailAddress.Value != null);
				return _emailAddress.Value;
			}
			set
			{
				Contract.Requires(!String.IsNullOrEmpty(value));
				Contract.Requires(EmailAddress.EmailRegex.IsMatch(value));
				_emailAddress = new EmailAddress(value);
			}

		}

		/// <summary>
		/// Gets the price of the item, if any.
		/// </summary>
		public decimal? Price
		{
			get
			{
				Contract.Ensures(Contract.Result<decimal?>() == null || Contract.Result<decimal?>() >= 0m);
				return _price;
			}
			set
			{
				Contract.Requires<ArgumentException>(value == null || value >= 0m);
				_price = value;
			}
		}

		/// <summary>
		/// Gets the category this note belongs to.
		/// </summary>
		public virtual Category Category
		{
			get { return _category; }
			internal set { _category = value; }
		}
	}
}

using System;
using System.Text;

namespace Beaker.TechDays.Billboard.Models.Specs.Helpers
{
	/// <summary>
	/// Creates random values
	/// </summary>
	public static class Some
	{
		private static readonly Random Random = new Random();

		/// <summary>
		/// Creates some random date between 1980 and 2030
		/// </summary>
		public static DateTime Date()
		{
			int days = Random.Next( 365 * 50 );
			return new DateTime( 1980, 1, 1 ).AddDays( days );
		}

		/// <summary>
		/// Creates some random date and time between 1980 and 2030
		/// </summary>
		public static DateTime DateTime()
		{
			int seconds = Random.Next( 365 * 50 * 24 * 60 * 60 );
			return new DateTime( 1980, 1, 1 ).AddSeconds( seconds );
		}

		/// <summary>
		/// Creates some random time (00:00:00 - 23:59:59)
		/// </summary>
		/// <returns></returns>
		public static TimeSpan Time()
		{
			int seconds = Random.Next( 24 * 60 * 60 );
			return new TimeSpan( seconds * 1000 );
		}

		/// <summary>
		/// Creates some random byte
		/// </summary>
		/// <returns>A random byte</returns>
		public static Byte Byte()
		{
			var buffer = new byte[1];
			Random.NextBytes( buffer );
			return buffer[0];
		}

		/// <summary>
		/// Creates some random short number
		/// </summary>
		/// <returns>A random number</returns>
		public static Int16 Int16()
		{
			var buffer = new byte[2];
			Random.NextBytes( buffer );
			return BitConverter.ToInt16( buffer, 0 );
		}


		/// <summary>
		/// Creates some random int number
		/// </summary>
		/// <returns>A random number</returns>
		public static Int32 Int32()
		{
			var buffer = new byte[4];
			Random.NextBytes( buffer );
			return BitConverter.ToInt32( buffer, 0 );
		}

		/// <summary>
		/// Creates some random int number
		/// </summary>
		/// <returns>A random number</returns>
		public static Int32 Int32(int minValue, int maxValue)
		{
			return Random.Next( minValue, maxValue );
		}

		/// <summary>
		/// Creates some random long number
		/// </summary>
		/// <returns>A random number</returns>
		public static Int64 Int64()
		{
			var buffer = new byte[8];
			Random.NextBytes( buffer );
			return BitConverter.ToInt64( buffer, 0 );
		}


		/// <summary>
		/// Creates some random long within range
		/// </summary>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		/// <returns></returns>
		public static Int64 Int64(long minValue, long maxValue = long.MaxValue)
		{
			var buf = new byte[8];
			Random.NextBytes( buf );
			long longRand = BitConverter.ToInt64( buf, 0 );

			return ( Math.Abs( longRand % ( maxValue - minValue ) ) + minValue );
		}

		private const string DefaultAllowedChars = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		/// <summary>
		/// Creates some random string between <see cref="minChars"/> and <see cref="maxChars"/> in length
		/// </summary>
		/// <param name="minChars">The min chars of the string</param>
		/// <param name="maxChars">The max chars of the string</param>
		/// <param name="allowedChars">The list of allowed characters</param>
		/// <returns>A random string between <see cref="minChars"/> and <see cref="maxChars"/> characters in length</returns>
		public static string String(int minChars = 8, int maxChars = 32, string allowedChars = DefaultAllowedChars)
		{
			int charCount = Random.Next( minChars, maxChars );

			var sb = new StringBuilder(charCount);
			for( int i = 0; i < charCount; i++ )
			{
				int cix = Random.Next(0, allowedChars.Length);
				sb.Append(allowedChars[cix]);
			}

			return sb.ToString();
		}

		public static string Sentence( int maxLength = 128 )
		{
			int wordCount = Random.Next(3, maxLength / 6);

			StringBuilder sentence = new StringBuilder(maxLength);
			for (var i = 0; i < wordCount; i++)
			{
				var word = String(3, 10);
				if (sentence.Length > 0)
					sentence.Append(' ');
				sentence.Append(word);
			}

			if (sentence.Length > maxLength - 1)
				sentence.Length = maxLength - 1;

			sentence.Append('.');
			return sentence.ToString();
		}

		private const string Chars = "abcdefghijklmnopqrstuvwxyz";

		public static string EmailAddress()
		{
			return String(8, 20, Chars) + "@" + String(4, 12, Chars) + "." + String(2, 3, Chars);
		}

	}
}
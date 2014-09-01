#region Using References

using System;
using System.Diagnostics.Contracts;

#endregion

namespace Beaker.TechDays.Billboard.Models.Specs.Helpers
{
	/// <summary>
	/// Some value that might be, or not.
	/// </summary>
	/// <typeparam name="T">The type of the value that might be.</typeparam>
	public struct Maybe<T>
	{
		private readonly bool _hasValue;
		private readonly T _value;

		public Maybe( T value )
		{
			_hasValue = true;
			_value = value;
		}

		/// <summary>
		/// Gets if there is a value set.
		/// </summary>
		public bool HasValue
		{
			get { return _hasValue; }
		}

		/// <summary>
		/// Gets the value, if any
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		/// <exception cref="System.InvalidOperationException">Thrown when there is no value.</exception>
		public T Value
		{
			get
			{
				if( !_hasValue )
					throw new InvalidOperationException();

				return _value;
			}
		}

		/// <summary>
		/// Gets the value, or when not set, the default value
		/// </summary>
		public T ValueOrDefault
		{
			get
			{
				if( !_hasValue )
					return default( T );
				return _value;
			}
		}

		/// <summary>
		/// Gets the value, or when not set, the provided default value.
		/// </summary>
		/// <param name="defaultValue">The default value to use.</param>
		/// <returns>When maybe has a value, the value of the maybe, otherwise the provided default value.</returns>
		public T GetValueOrDefault( T defaultValue = default(T) )
		{
			if( !_hasValue )
				return defaultValue;

			return _value;
		}

		/// <summary>
		/// Gets the value, or when not set, calls the provided function to create a default value.
		/// </summary>
		/// <param name="createDefaultValue">Function to create  a default value.</param>
		/// <returns>When maybe has a vlaue, the value of the maybe; otherwise the value created by the provided function.</returns>
		public T GetValueOrDefault( Func<T> createDefaultValue )
		{
			Contract.Requires<ArgumentNullException>( createDefaultValue != null  );

			if( !_hasValue )
				return createDefaultValue();

			return _value;
		}

		public static explicit operator T( Maybe<T> maybe )
		{
			if( !maybe.HasValue )
				throw new InvalidCastException( "Maybe has no value, so cannot be cast to a value" );
			
			return maybe.Value;
		}

		public static implicit operator Maybe<T>( T value )
		{
			return new Maybe<T>( value );
		}

	}
}
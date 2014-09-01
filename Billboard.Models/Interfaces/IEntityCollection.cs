using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Billboard.Models.Interfaces
{
	/// <summary>
	/// Interface for a set of entities.
	/// </summary>
	[ContractClass(typeof(EntityCollectionContracts<>))]
	public interface IEntityCollection<TEntity>
		: ICollection<TEntity>
		where TEntity : class
	{
	}

	/// <summary>
	/// Code Contracts for the <see cref="IEntityCollection{TEntity}"/> interface.
	/// </summary>
	[ContractClassFor(typeof(IEntityCollection<>))]
	abstract class EntityCollectionContracts<TEntity>
		: IEntityCollection<TEntity>
		where TEntity : class
	{
		#region Enumerator

		IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
		#region Collection

		public void Add(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(TEntity item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(TEntity[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		bool ICollection<TEntity>.Remove(TEntity item)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		public void Remove(TEntity entity)
		{
			Contract.Requires<ArgumentNullException>(entity != null, "entity");
			throw new NotImplementedException();
		}

		#endregion
	}

}

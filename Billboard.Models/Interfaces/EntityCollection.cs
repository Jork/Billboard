using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace Billboard.Models.Interfaces
{
	internal sealed class EntityCollection<TEntity>
		: IEntityCollection<TEntity>
		where TEntity : class
	{
		private readonly ICollection<TEntity> _entities = new Collection<TEntity>();
		private readonly Action<TEntity> _coupleEntity;
		private readonly Action<TEntity> _decoupleEntity;

		public EntityCollection(Action<TEntity> coupleEntity, Action<TEntity> decoupleEntity)
		{
			Contract.Requires<ArgumentNullException>(coupleEntity != null);
			Contract.Requires<ArgumentNullException>(decoupleEntity != null);

			_coupleEntity = coupleEntity;
			_decoupleEntity = decoupleEntity;
		}

		IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
		{
			return _entities.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _entities.GetEnumerator();
		}

		public void Add(TEntity entity)
		{
			_coupleEntity(entity);
			_entities.Add(entity);
		}

		public void Clear()
		{
			foreach (var entity in this)
				_decoupleEntity(entity);

			_entities.Clear();
		}

		public bool Contains(TEntity item)
		{
			return _entities.Contains(item);
		}

		void ICollection<TEntity>.CopyTo(TEntity[] array, int arrayIndex)
		{
			_entities.CopyTo(array, arrayIndex);
		}

		public bool Remove(TEntity item)
		{
			if (!_entities.Contains(item))
				return false;

			_decoupleEntity(item);
			return _entities.Remove(item);
		}

		public int Count
		{
			get { return _entities.Count; }
		}

		bool ICollection<TEntity>.IsReadOnly
		{
			get { return false; }
		}
	}
}

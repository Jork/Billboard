using System;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using Billboard.Models.Interfaces;

namespace Billboard.Models.Persistence
{
	public static class RepositoryExtensions
	{
		/// <summary>
		/// Gets the item with the given id.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity to get.</typeparam>
		/// <param name="dbSet">The db set to get the entity from.</param>
		/// <param name="id">The id of the entity to get.</param>
		/// <returns></returns>
		public static TEntity GetById<TEntity>(this IDbSet<TEntity> dbSet, Guid id)
			where TEntity : class, IIdentifiable
		{
			Contract.Requires<ArgumentNullException>(dbSet != null, "dbSet");

			return dbSet.SingleOrDefault(e => e.Id == id);
		}

		/// <summary>
		/// Gets the item with the given name.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity to get.</typeparam>
		/// <param name="dbSet">The db set to get the entity from.</param>
		/// <param name="name">The name of the entity to get.</param>
		/// <returns>The entity with the given name, or when not found <c>null</c>.</returns>
		public static TEntity GetByName<TEntity>(this IDbSet<TEntity> dbSet, string name)
			where TEntity : class, INamed
		{
			Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(name));

			return dbSet.SingleOrDefault(e => e.Name == name);
		}
	}
}

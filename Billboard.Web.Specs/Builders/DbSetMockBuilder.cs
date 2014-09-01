using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace Beaker.TechDays.Billboard.Web.Specs.Builders
{
	public sealed class DbSetMockBuilder<TEntity>
		where TEntity : class
	{
		private readonly ISet<TEntity> _entities = new HashSet<TEntity>();

		public DbSetMockBuilder<TEntity> ContainingEntity(TEntity entity)
		{
			_entities.Add(entity);
			return this;
		}

		public IDbSet<TEntity> Build()
		{
			// create local queryably copy
			IQueryable<TEntity> query = _entities.ToList().AsQueryable();

			// setup mock for dbset
			var dbSet = new Mock<IDbSet<TEntity>>(MockBehavior.Strict);

			dbSet.SetupGet(dbs => dbs.Provider).Returns(query.Provider);
			dbSet.SetupGet(dbs => dbs.Expression).Returns(query.Expression);
			dbSet.SetupGet(dbs => dbs.ElementType).Returns(query.ElementType);
			dbSet.Setup(dbs => dbs.GetEnumerator()).Returns(query.GetEnumerator());

			return dbSet.Object;
		}
	}
}
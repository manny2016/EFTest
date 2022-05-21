using LygIM.DataAccess.EntityFramework;
using LygIM.Models;
using Mehdime.Entity;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LygIM.Buiness.Services.Repository {
	public abstract class ReposityBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase {

		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		protected IAmbientDbContextLocator _ambientDbContextLocator;
		public ReposityBase(IAmbientDbContextLocator ambientDbContextLocator) {

			_ambientDbContextLocator = ambientDbContextLocator;

		}
		protected LygIMDbContext DbContext
		{
			get
			{
				return _ambientDbContextLocator.Get<LygIMDbContext>();
			}
		}
		public TEntity Add(TEntity entity) {

			DbContext.Set<TEntity>().Add(entity);
			DbContext.Entry(entity).State= EntityState.Added;
			return entity;
		}

		public virtual async Task<TEntity> GetAsync(Guid id) {

			var result = await DbContext.Set<TEntity>().FindAsync(id);

			return result;
		}

		public TEntity Remove(TEntity entity) {

			DbContext.Set<TEntity>().Remove(entity);

			DbContext.Entry(entity).State = EntityState.Deleted;

			return entity;
		}

		public TEntity Update(TEntity entity) {
			
			DbContext.Entry(entity).State = EntityState.Modified;
			return entity;
		}
	}
}

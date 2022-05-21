using LygIM.DataAccess.EntityFramework;
using LygIM.Models;
using Mehdime.Entity;
using Newtonsoft.Json.Linq;
using NLog;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace LygIM.Buiness.Services
{
	public class MyDbContextScope : DbContextScope, IDbContextScope
	{

		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		public MyDbContextScope(AuditContext auditContext,
			DbContextScopeOption joingOption,
			bool readOnly = false,
			IsolationLevel? isolationLevel = null,
			IDbContextScopeFactory dbContextScopeFactory = null) : this()
		{

			_auditContext = auditContext;
			_dbContextScopeFactory = dbContextScopeFactory;


		}
		private MyDbContextScope()
		{
			handlers.Add(EntityState.Added, HandleAddedEntry);
			handlers.Add(EntityState.Modified, HandleModifiedEntry);
			handlers.Add(EntityState.Deleted, HandleDeletedEntry);

		}
		private readonly AuditContext _auditContext;

		private readonly IDictionary<EntityState, int> priorities = new Dictionary<EntityState, int>()
		{
			{ EntityState.Modified,1},
			{ EntityState.Deleted,2},
			{ EntityState.Added, 3},
		};

		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		private readonly IDictionary<EntityState, Action<LygIMDbContext, ObjectStateEntry[], AuditContext>> handlers
			= new Dictionary<EntityState, Action<LygIMDbContext, ObjectStateEntry[], AuditContext>>();

		public new Task<int> SaveChangesAsync()
		{
			var dbContext = this.DbContexts.Get<LygIMDbContext>();

			var transation = dbContext.Database.BeginTransaction();

			try
			{
				var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;

				foreach (var g in objectContext.ObjectStateManager.GetObjectStateEntries(
					EntityState.Modified | EntityState.Added | EntityState.Deleted)
					.GroupBy(x => x.State)
					.Select((x) => new
					{
						Key = x.Key,
						Entries = x.ToArray(),
						Priority = priorities[x.Key]

					}).OrderBy(x => x.Priority))
				{
					handlers[g.Key].Invoke(dbContext, g.Entries, _auditContext);
				}

				transation.Commit();

				return base.SaveChangesAsync();

			}
			catch (Exception ex)
			{
				transation.Rollback();
			}

			throw new InvalidOperationException();

		}

		private void HandleAddedEntry(LygIMDbContext dbContext, ObjectStateEntry[] stateEntries, AuditContext auditContext)
		{

			if (stateEntries == null || stateEntries.Count().Equals(0)) return;

			dbContext.SaveChanges();

			foreach (var stateEntry in stateEntries)
			{
				var newValue = new JObject();

				var propertyNames = dbContext.Entry(stateEntry.Entity).CurrentValues.PropertyNames;

				foreach (var propertyName in propertyNames)
				{
					newValue.Add(propertyName, stateEntry.CurrentValues[propertyName].ToString());
				}

				dbContext.Set<Audit>().Add(new Audit(auditContext, null, newValue));
			}

		}
		private void HandleDeletedEntry(LygIMDbContext dbContext, ObjectStateEntry[] stateEntries, AuditContext auditContext)
		{
			if (stateEntries == null || stateEntries.Count().Equals(0)) return;

			foreach (var stateEntry in stateEntries)
			{
				var oldValue = new JObject();

				foreach (var propertyName in dbContext.Entry(stateEntry.Entity)
					.OriginalValues.PropertyNames)
				{
					oldValue.Add(propertyName, stateEntry.OriginalValues[propertyName].ToString());
				}

				dbContext.Set<Audit>().Add(new Audit(auditContext, oldValue, null));
			}
		}
		private void HandleModifiedEntry(LygIMDbContext dbContext, IEnumerable<ObjectStateEntry> stateEntries, AuditContext auditContext)
		{

			foreach (var stateEntry in stateEntries)
			{
				var newValue = new JObject();
				var oldValue = new JObject();

				foreach (var propertyName in stateEntry.GetModifiedProperties())
				{
					oldValue.Add(propertyName, stateEntry.OriginalValues[propertyName].ToString());
					newValue.Add(propertyName, stateEntry.CurrentValues[propertyName].ToString());
				}

				dbContext.Set<Audit>().Add(new Audit(auditContext, oldValue, newValue));
			}
		}
	}
}

using LygIM.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LygIM.DataAccess.EntityFramework {
	public class LygIMDbContext : DbContext {

		public LygIMDbContext() : base("LygIMDB") {
			
		}

		public DbSet<Workspace> Workspace { get; set; }

		public DbSet<Audit> Audit { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {

			modelBuilder.Entity<Workspace>().HasIndex(x => x.Name).IsClustered(false).IsUnique();
			
			modelBuilder.Entity<Audit>().HasIndex(x => x.Timestamp).IsClustered(false).IsUnique(false);
			
			base.OnModelCreating(modelBuilder);
		}

		
	}
}

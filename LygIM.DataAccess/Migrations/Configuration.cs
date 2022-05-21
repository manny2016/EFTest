using LygIM.DataAccess.EntityFramework;
using System.Data.Entity.Migrations;

namespace LygIM.DataAccess.Migration {
	internal sealed class Configuration : DbMigrationsConfiguration<LygIMDbContext> {
		public Configuration() {
			AutomaticMigrationsEnabled = false;
		}
		protected override void Seed(LygIMDbContext context) {

		}
	}
}

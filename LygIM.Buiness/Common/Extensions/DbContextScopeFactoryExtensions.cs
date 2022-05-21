using LygIM.Buiness.Services;
using LygIM.Models;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LygIM.Buiness {
	public static class DbContextScopeFactoryExtensions {
		public static IDbContextScope Create(
			this IDbContextScopeFactory dbContextScopeFactory,			
			AuditContext context) {
			
			return new MyDbContextScope(context,  
				DbContextScopeOption.JoinExisting, dbContextScopeFactory: dbContextScopeFactory);
		}
	}
}

using LygIM.Models;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LygIM.Buiness.Services.Repository
{
	public class WorkspaceRepository : ReposityBase<Workspace>, IWorkspaceRepository
	{
		public WorkspaceRepository(IAmbientDbContextLocator ambientDbContextLocator)
			: base(ambientDbContextLocator)
		{

		}
		
		public override Workspace Update(Workspace entity)
		{
			foreach (var config in entity.Configurations)
			{
				if (config.Id == Guid.Empty)
				{
					DbContext.Set<WorkspaceConfiguration>().Add(config);
					DbContext.Entry(config).State = System.Data.Entity.EntityState.Added;
				}
				else
				{
					//DbContext.Set<WorkspaceConfiguration>().Attach(config);
					DbContext.Entry(config).State = System.Data.Entity.EntityState.Modified;
				}
			}
			return base.Update(entity);
		}
	}
}

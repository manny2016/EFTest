using LygIM.Models;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LygIM.Buiness.Services.Repository {
	public class WorkspaceRepository : ReposityBase<Workspace>, IWorkspaceRepository {
		public WorkspaceRepository(IAmbientDbContextLocator ambientDbContextLocator)
			: base(ambientDbContextLocator) {

		}
	}
}

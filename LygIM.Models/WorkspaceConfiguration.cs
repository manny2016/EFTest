using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LygIM.Models {
	public class WorkspaceConfiguration : EntityBase {


		public Guid WorkspaceId { get; set; }

		public string Key { get; set; }

		public string Value { get; set; }

		public virtual Workspace Workspace { get; set; }
	}
}

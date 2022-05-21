using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LygIM.Models {
	public class AuditContext {
		public AuditContext(Guid requestId, string action, string user) {

			RequestId = requestId;
			Action = action;
			User = user;
		}
		public Guid RequestId { get; set; }

		public string Action { get; set; }

		public string User { get; set; }
	}
}

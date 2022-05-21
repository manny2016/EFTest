using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LygIM.Models {
	public class Audit : EntityBase {
		public Audit(AuditContext auditContext,JObject oldValue,JObject newValue) {

			Action= auditContext.Action;			
			User = auditContext.User;
			Timestamp = DateTime.UtcNow;
			OldValue = oldValue?.ToString();
			NewValue = newValue?.ToString();

		}
		[Required]
		[MaxLength(200)]
		public string Action { get;set;}

		[Required]
		[MaxLength(50)]
		public string User { get; set; }

		[MaxLength(-1)]
		public string OldValue { get; set; }

		[MaxLength(-1)]
		public string NewValue { get; set; }

		[Required]
		public DateTime Timestamp { get;set;}
	}
}

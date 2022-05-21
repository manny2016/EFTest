using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LygIM.Models {
	public class Workspace : EntityBase {

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }


		public virtual ICollection<WorkspaceConfiguration> Configurations { get; set; }
	}
}

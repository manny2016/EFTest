using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LygIM.Lanucher {
	public class ClazzIns {

		
		public static readonly string AmbientDbContextScopeKey = "AmbientDbcontext_" + Guid.NewGuid();
	}
}

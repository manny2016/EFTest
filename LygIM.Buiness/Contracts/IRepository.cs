using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LygIM.Buiness {
	public interface IRepository<TEntity> where TEntity : class {
		Task<TEntity> GetAsync(Guid id);
		TEntity Add(TEntity entity);
		TEntity Update(TEntity entity);
		TEntity Remove(TEntity entity);
	}
}

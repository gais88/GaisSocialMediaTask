using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Data.Repositories
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T?> GetByIdAsync(int id);
		Task<T?> FindAsync(Expression<Func<T, bool>> match, string[]? includes = null);
		Task<List<T>> GetAllAsync();
		Task<T> AddAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task<T> DeleteAsync(T entity);

		
	}
}

using GaisSocialMediaTask.Core.Entities;
using GaisSocialMediaTask.Data.Context;
using GaisSocialMediaTask.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Data.Contracts
{
	public class GenericRepository<T> : IGenericRepository<T> where T : AuditEntity
	{
		private readonly AppDbContext _dbContext;
		public GenericRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public virtual async Task<T?> GetByIdAsync(int id)
		{
			T? t = await _dbContext.Set<T>().FindAsync(id);
			return t;
		}
		public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> match, string[]? includes = null)
		{
			IQueryable<T> query = _dbContext.Set<T>();
			if (includes != null)
			{
				foreach (var include in includes)
				{
					query.Include(include);
				}
			}
			T? t = await query.SingleOrDefaultAsync(match);

			return t;
		}

		public virtual async Task<List<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().AsNoTracking()
											.ToListAsync();
		}

		public async Task<T> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);

			return entity;
		}
		public async Task<T> UpdateAsync(T entity)
		{
			T? result = await _dbContext.Set<T>().FindAsync(entity.Id);
			if (result != null)
			{
				_dbContext.Entry(entity).State = EntityState.Modified;
			}

			return entity;
		}
		public async Task<T> DeleteAsync(T entity)
		{
			T? result = await _dbContext.Set<T>().FindAsync(entity.Id);
			if (result != null)
			{
				_dbContext.Set<T>().Remove(entity);
			}

			return entity;
		}
		
	}
}

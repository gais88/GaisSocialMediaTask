using GaisSocialMediaTask.Core.Entities;
using GaisSocialMediaTask.Data.Context;
using GaisSocialMediaTask.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GaisSocialMediaTask.Data.Contracts
{
	public class UnitOfWork:IUnitOfWork
	{
		private readonly AppDbContext _dbContext;

		public UnitOfWork(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public IAppUserRepository AppUserRepository => new AppUserRepository(_dbContext, new Logger<AppUserRepository>(new NullLoggerFactory()));
		public IPostRepository PostRepository => new PostRepository(_dbContext, new Logger<PostRepository>(new NullLoggerFactory()));
		public ICommentRepository CommentRepository => new CommentRepository(_dbContext);

		public async Task<bool> SaveAsync()
		{
			return await _dbContext.SaveChangesAsync() > 0;
		}
	}
}

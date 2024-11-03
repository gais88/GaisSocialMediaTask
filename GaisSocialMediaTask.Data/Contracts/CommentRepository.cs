using GaisSocialMediaTask.Core.Entities;
using GaisSocialMediaTask.Data.Context;
using GaisSocialMediaTask.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Data.Contracts
{
	public class CommentRepository : GenericRepository<Comment>, ICommentRepository
	{
		private readonly AppDbContext _dbContext;
		public CommentRepository(AppDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public override async Task<List<Comment>> GetAllAsync()
		{
			var posts = await _dbContext.Comments.Include(x => x.AppUser).ToListAsync();
			return posts;
		}
	}
}

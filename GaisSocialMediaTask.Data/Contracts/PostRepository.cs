using GaisSocialMediaTask.Core.Entities;
using GaisSocialMediaTask.Data.Context;
using GaisSocialMediaTask.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Data.Contracts
{
	public class PostRepository : GenericRepository<Post>, IPostRepository
	{
		private readonly AppDbContext _dbContext;
		private readonly ILogger<PostRepository> _logger;
		public PostRepository(AppDbContext dbContext, ILogger<PostRepository> logger) : base(dbContext)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		public async Task<bool> AddLike(int postId)
		{
			try
			{
				_logger.LogInformation("AddLikes for Post was Called");
				var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
				if (post == null)
				{
					return false;
				}
				post.Like += 1;
				var result = _dbContext.SaveChanges();
				return result != 0;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Faild to IsValidIdAsync for AppUser: {ex.Message}");
				return true;
			}
		}

		public override async Task<List<Post>> GetAllAsync()
		{
			var posts = await _dbContext.Posts.Include(x=>x.AppUser).Include(x=>x.Comments).ToListAsync();
			return posts;
		}
	}
}

using GaisSocialMediaTask.Core.Entities;
using GaisSocialMediaTask.Data.Context;
using GaisSocialMediaTask.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Data.Contracts
{
	public class AppUserRepository : IAppUserRepository
	{
		private readonly AppDbContext _dbContext;
		private readonly ILogger<AppUserRepository> _logger;


		public AppUserRepository(AppDbContext dbContext, ILogger<AppUserRepository> logger)
		{
			_dbContext = dbContext;
			_logger = logger;

		}

		public async Task<AppUser?> GetByIdAsync(string id)
		{
			try
			{
				_logger.LogInformation("GetByIdAsync for AppUser was Called");

				return await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Faild to GetByIdAsync for AppUser: {ex.Message}");
				return null;
			}
		}
		public async Task<AppUser?> GetByUserNameAsync(string userName)
		{
			try
			{
				_logger.LogInformation("GetByUserNameAsync for AppUser was Called");

				return await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.UserName!.ToLower() == userName.ToLower());
			}
			catch (Exception ex)
			{
				_logger.LogError($"Faild to GetByUserNameAsync for AppUser: {ex.Message}");
				return null;
			}
		}


		public async Task<bool> IsValidIdAsync(int id)
		{
			try
			{
				_logger.LogInformation("IsValidIdAsync for AppUser was Called");
				return await _dbContext.AppUsers.AnyAsync(x => x.Id == id.ToString());
			}
			catch (Exception ex)
			{
				_logger.LogError($"Faild to IsValidIdAsync for AppUser: {ex.Message}");
				return true;
			}
		}
		public async Task<bool> IsValidUserNameAsync(string userName)
		{
			try
			{
				_logger.LogInformation("IsValidUserNameAsync for AppUser was Called");
				return await _dbContext.AppUsers.AnyAsync(x => x.UserName!.ToLower() == userName.ToLower());
			}
			catch (Exception ex)
			{
				_logger.LogError($"Faild to IsValidUserNameAsync for AppUser: {ex.Message}");
				return true;
			}
		}

		public async Task<IEnumerable<AppUser>?> GetAllAsync()
		{
			try
			{
				_logger.LogInformation("GetAllAsync for AppUser was Called");

				return await _dbContext.AppUsers.ToListAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Faild to GetAllAsync for AppUser: {ex.Message}");
				return null;
			}
		}
		


	}
}

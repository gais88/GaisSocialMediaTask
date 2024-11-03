using GaisSocialMediaTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Data.Repositories
{
	public interface IAppUserRepository
	{
		Task<AppUser?> GetByIdAsync(string id);
		Task<AppUser?> GetByUserNameAsync(string userName);
		Task<bool> IsValidIdAsync(int id);
		Task<bool> IsValidUserNameAsync(string userName);

		Task<IEnumerable<AppUser>?> GetAllAsync();
	}
}

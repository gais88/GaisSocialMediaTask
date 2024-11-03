using GaisSocialMediaTask.Core.Entities;

namespace GaisSocialMediaTask.Api.Services
{
	public interface ITokenService
	{
		Task<string> CreateToken(AppUser appUser);
	}
}

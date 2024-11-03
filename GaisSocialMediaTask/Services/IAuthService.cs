using AutoMapper.Execution;
using GaisSocialMediaTask.Api.Dtos;

namespace GaisSocialMediaTask.Api.Services
{
	public interface IAuthService
	{
		Task<UserDto> RegisterAsync(RegisterDto model);
		Task<UserDto> LoginAsync(LoginDto model);
	}
}

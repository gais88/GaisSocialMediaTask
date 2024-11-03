using AutoMapper.Execution;
using GaisSocialMediaTask.Api.Dtos;
using GaisSocialMediaTask.Api.Responses;
using GaisSocialMediaTask.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GaisSocialMediaTask.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{


		private readonly IAuthService _authService;
		public AccountController(IAuthService authService)
		{

			_authService = authService;
		}
		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.RegisterAsync(model);

			if (!result.IsAuthenticated)
				return BadRequest(new DataResponse(false,result.Message));

			return Ok(new DataResponse<UserDto>(true,result,"User Was Created Succefully"));
		}

		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.LoginAsync(model);

			if (!result.IsAuthenticated)
				return BadRequest(new DataResponse(false, result.Message));

			return Ok(new DataResponse<UserDto>(true, result, "Login Success"));

		}


	}
}

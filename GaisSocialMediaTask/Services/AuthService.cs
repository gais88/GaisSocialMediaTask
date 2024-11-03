using AutoMapper.Execution;
using GaisSocialMediaTask.Api.Dtos;
using GaisSocialMediaTask.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GaisSocialMediaTask.Api.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _config;
		private readonly SymmetricSecurityKey _key;



		public AuthService(UserManager<AppUser> userManager,
						   IConfiguration config)
		{
			_userManager = userManager;
			_config = config;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));

		}

		public async Task<UserDto> RegisterAsync(RegisterDto model)
		{
			if (await _userManager.FindByEmailAsync(model.Email) is not null)
				return new UserDto { Message = "Email is already registered!" };


			var user = new AppUser
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,

			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				var errors = string.Empty;

				foreach (var error in result.Errors)
					errors += $"{error.Description},";

				return new UserDto { Message = errors };
			}
			var jwtSecurityToken = await CreateToken(user);

			return new UserDto
			{
				Email = user.Email,

				IsAuthenticated = true,
				Token = jwtSecurityToken,

			};
		}

		public async Task<UserDto> LoginAsync(LoginDto model)
		{
			var authModel = new UserDto();

			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
			{
				authModel.Message = "Email or Password is incorrect!";
				return authModel;
			}

			var jwtSecurityToken = await CreateToken(user);
			

			authModel.Id = user.Id;
			authModel.IsAuthenticated = true;
			authModel.Token = jwtSecurityToken;
			authModel.Email = user.Email;
			

			return authModel;
		}



		public async Task<string> CreateToken(AppUser? appUser)
		{
			// Add Claims
			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.NameId, appUser.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, appUser.UserName)
			};

			// Get User Roles
			var roles = await _userManager.GetRolesAsync(appUser);

			// Add Roles to Claim List
			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

			// Create Credential
			var credential = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			// Describe Token Look 
			var tokenDescriptor = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(30),
				SigningCredentials = credential
			};

			// Create Token Handler 
			var tokenHandler = new JwtSecurityTokenHandler();

			// Create Token
			var token = tokenHandler.CreateToken(tokenDescriptor);

			// Return Created Token
			return tokenHandler.WriteToken(token);
		}
	}
}

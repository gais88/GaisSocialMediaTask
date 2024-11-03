
using GaisSocialMediaTask.Core.Entities;
using GaisSocialMediaTask.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GaisSocialMediaTask.Api.Extensions
{
	public static class IdentityServiceExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
		{
			// CORS Configurations.
			services.AddCors(option =>
			{
				option.AddPolicy("CorsePolicy", policy =>
				{
					policy.AllowAnyHeader();
					policy.AllowAnyMethod();
					policy.AllowAnyOrigin();
				});
			});

			// Add Authentication
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
					.AddCookie()
					.AddJwtBearer(options =>
					{
						options.TokenValidationParameters = new TokenValidationParameters()
						{
							ValidateIssuerSigningKey = true,
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
							ValidateIssuer = false,
							ValidateAudience = false
						};
					});

			// Add Identity
			services.AddIdentityCore<AppUser>(option =>
			{
				option.Password.RequireNonAlphanumeric = false;
				option.Password.RequireDigit = true;
				option.Password.RequireUppercase = false;
			})
			.AddRoles<IdentityRole>()
			.AddRoleManager<RoleManager<IdentityRole>>()
			.AddSignInManager<SignInManager<AppUser>>()
			.AddRoleValidator<RoleValidator<IdentityRole>>()
			.AddEntityFrameworkStores<AppDbContext>();

			return services;
		}
	}
}


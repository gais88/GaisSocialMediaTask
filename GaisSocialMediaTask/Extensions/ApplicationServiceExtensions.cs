using GaisSocialMediaTask.Api.Errors;
using GaisSocialMediaTask.Api.Services;
using GaisSocialMediaTask.Data.Context;
using GaisSocialMediaTask.Data.Contracts;
using GaisSocialMediaTask.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GaisSocialMediaTask.Api.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{


			services.AddHttpContextAccessor();

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<ITokenService, TokenService>();
			//services.AddScoped<IAppUserRepository, AppUserRepository>();
			//services.AddScoped<IPostRepository, PostRepository>();
			//services.AddScoped<ICommentRepository,CommentRepository>();



			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
			});

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = actionContext =>
				{
					var errors = actionContext.ModelState
						.Where(e => e.Value.Errors.Count > 0)
						.SelectMany(x => x.Value.Errors)
						.Select(x => x.ErrorMessage)
						.ToArray();

					var errorResponse = new ApiValidationErrorResponse
					{
						Errors = errors
					};

					return new BadRequestObjectResult(errorResponse);
				};
			});

			return services;
		}
	}
}

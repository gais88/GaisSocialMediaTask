
using GaisSocialMediaTask.Api.Extensions;
using GaisSocialMediaTask.Api.Middleware;
using GaisSocialMediaTask.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GaisSocialMediaTask
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddIdentityServices(builder.Configuration);
			builder.Services.AddApplicationServices(builder.Configuration);
			builder.Services.AddSwaggerDocumentation();
			builder.Services.AddResponseCaching();

			var app = builder.Build();

			app.UseMiddleware<ExceptionMiddleware>();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStatusCodePagesWithReExecute("/Errors/{0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseResponseCaching();

			app.MapControllers();

			// Data Seed
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<AppDbContext>();
			try
			{
				if (context != null)
				{
					await context.Database.MigrateAsync();
				}
					
			}
			catch (Exception ex)
			{
				var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
				logger.LogError(ex, "Error Occured During Migration");
			}

			app.Run();
		}
	}
}

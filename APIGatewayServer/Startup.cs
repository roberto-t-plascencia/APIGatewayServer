using Microservice.gateway.api.DbContexts;
using Microservice.gateway.api.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplication1
{
	public class Startup
	{
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddDbContext<ApplicationDbContext>(options =>
							options.UseSqlServer(
							Configuration.GetConnectionString("DefaultConnection"),
							ef => ef.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
			services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
			services.AddTransient<IOrderRepository, OrderRepository>(); services.AddControllers();
			services.AddTransient<IUserRepository, UserRepository>(); services.AddControllers();

			//Active Directory Authentication
			//services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			//	.AddJwtBearer(opt =>
			//	{
			//		opt.Audience = Configuration["AzureActiveDirectory:ResourceId"];
			//		opt.Authority = $"{Configuration["AzureActiveDirectory:Instance"]}{Configuration["AzureActiveDirectory:TenantId"]}";
			//	});

			services.AddApiVersioning(apiVerConfig =>
			{
				apiVerConfig.AssumeDefaultVersionWhenUnspecified = true;
				apiVerConfig.DefaultApiVersion = new ApiVersion(new DateTime(2020, 11, 20));
			});

			services.AddHealthChecks()
				.AddDbContextCheck<ApplicationDbContext>();

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "Microservice - Order Web API",
					Version = "v1",
					Description = "Sample microservice for order",
				});
			});

			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
								  builder =>
								  {
									  //builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
									  //.AllowCredentials();

									  builder.WithOrigins("https://localhost:5012", "https://localhost:5011",

														"https://localhost")
									  .AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
								  });
			});

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			//app.UseCors(builder => builder.WithOrigins("http://localhost:5012").AllowAnyMethod().AllowAnyHeader());
			app.UseCors();
			//app.UseAuthentication();
			//app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseHealthChecks("/checkhealth");

			app.UseSwagger();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "PlaceInfo Services"));
		}
	}
}

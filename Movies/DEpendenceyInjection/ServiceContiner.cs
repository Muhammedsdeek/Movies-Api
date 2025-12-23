using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.Data;
using Movies.Entities;
using Movies.Mapping;
using Movies.Middleware;
using Movies.Services.Interfaces;
using Movies.Services.Repositries;
using Movies.Services.ServiceImplimentiation;
using Movies.Services.ServiceInterfacse;
using System.Text;

namespace Movies.DEpendenceyInjection
{
	public static class ServiceContiner
	{

	

		public static IServiceCollection AddMovieServices(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddScoped<Igeneric<Movie>, GenericRepositry<Movie>>();
			services.AddScoped<Igeneric<Genre>, GenericRepositry<Genre>>();
			services.AddScoped<Igenreservice, GenreService>();
			services.AddScoped<ImovieService, MOvieService>();
			services.AddAutoMapper(cfg => cfg.AddProfile<MapingConfigurations>());
			services.AddProblemDetails();

			services.AddExceptionHandler<SQlExceptionHandeler>();

			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});
			services.AddIdentity<AppUser, IdentityRole>(options =>
			{

				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 6;
			})
			.AddEntityFrameworkStores<AppDbContext>();
			//.AddDefaultTokenProviders();



				services.AddAuthentication(options =>
				{

					options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;

					options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
					options.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options=>
				{
					options.SaveToken = true;
					options.MapInboundClaims = true;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
					
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = configuration["Jwt:Issuer"],
						ValidAudience = configuration["Jwt:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
						ClockSkew = TimeSpan.Zero
					};
				});

				return services;
			}

	
	}
}

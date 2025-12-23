using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Data;
using Movies.DEpendenceyInjection;
using Movies.Entities;
using Movies.Mapping;
using Movies.Middleware;
using Movies.Seeders;
using Movies.Services.Interfaces;
using Movies.Services.Repositries;
using Movies.Services.ServiceImplimentiation;
using Movies.Services.ServiceInterfacse;
using Scalar.AspNetCore;
using System.Threading.Tasks;

namespace Movies
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            #region services Continer
            //// service for DbContext


            //builder.Services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //});

            //         builder.Services.AddScoped<Igeneric<Movie>, GenericRepositry<Movie>>();

            //builder.Services.AddScoped<Igeneric<Genre>, GenericRepositry<Genre>>();
            //builder.Services.AddScoped<Igenreservice,GenreService>();
            //builder.Services.AddScoped<ImovieService, MOvieService>();


            //builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapingConfigurations>());

            //builder.Services.AddProblemDetails();

            //builder.Services.AddExceptionHandler<SQlExceptionHandeler>();
            builder.Services.AddMovieServices(builder.Configuration);

			///
			#endregion
			var app = builder.Build();

          await RolesSeeder.SeedAsync(app.Services);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {

                app.MapScalarApiReference();
                app.MapOpenApi();
            }
            app.UseExceptionHandler();
			app.UseHttpsRedirection();
            app.UseAuthentication();
			app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

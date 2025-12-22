using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Data;
using Movies.Entities;
using Movies.Mapping;
using Movies.Middleware;
using Movies.Services.Interfaces;
using Movies.Services.Repositries;
using Movies.Services.ServiceImplimentiation;
using Movies.Services.ServiceInterfacse;
using Scalar.AspNetCore;

namespace Movies
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            #region services Continer
            //// service for DbContext


            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<Igeneric<Movie>, GenericRepositry<Movie>>();

			builder.Services.AddScoped<Igeneric<Genre>, GenericRepositry<Genre>>();
			builder.Services.AddScoped<Igenreservice,GenreService>();
			builder.Services.AddScoped<ImovieService, MOvieService>();


			builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapingConfigurations>());

			builder.Services.AddProblemDetails();

			builder.Services.AddExceptionHandler<SQlExceptionHandeler>();

			///
			#endregion
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.MapScalarApiReference();
                app.MapOpenApi();
            }
            app.UseExceptionHandler();
			app.UseHttpsRedirection();
                
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

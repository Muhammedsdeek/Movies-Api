	using Microsoft.AspNetCore.Diagnostics;
	using Microsoft.Data.SqlClient;
	using Microsoft.EntityFrameworkCore;
using Movies.Entities;
using Movies.Services.ServiceInterfacse;

namespace Movies.Middleware
	{
		public class SQlExceptionHandeler : IExceptionHandler
		{
		
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
			{
			


				if(exception is DbUpdateException dbUpdateException && 
					dbUpdateException.InnerException is SqlException  sqlexc)
				{

				httpContext.Response.ContentType= "application/json";
					 var ErrorNumber = sqlexc.Number;
					var (statusCode, message) = ErrorNumber switch
					{
						2627 or 2601 => (StatusCodes.Status409Conflict,
							"Duplicate value. This record already exists."),

						547 => (StatusCodes.Status409Conflict,
							"Operation failed due to related records (foreign key constraint)."),	

						515 => (StatusCodes.Status400BadRequest,
							"A required field is missing."),


						_ => (StatusCodes.Status500InternalServerError,
							"A database error occurred.")
					};
					httpContext.Response.StatusCode = statusCode;

					await httpContext.Response.WriteAsJsonAsync(new
					{
						Error = message,
						ErrorCode = ErrorNumber
					}, cancellationToken);
					return true;
				}
			
				return false;


			}
		}
	}

using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Services.Interfaces;

namespace Movies.Services.Repositries
{
	public class GenericRepositry<T>: Igeneric<T>  where T : class
	{
		private readonly AppDbContext context;

		public GenericRepositry(AppDbContext context)
		{
			this.context = context;
		}
		public async Task<int> AddAsync(T entity)
		{
		 	await context.Set<T>().AddAsync(entity);
		
			return await context.SaveChangesAsync();
		}

		public async Task<int> DeleteAsync(int id)
		{
			 var entity = await context.Set<T>().FindAsync(id);
			  context.Set<T>().Remove(entity!);
			return await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await context.Set<T>().ToListAsync();
		}

		public async Task<IEnumerable<T>> GetAllWithName(string value, string propertyName)
		{
			
			return await context.Set<T>()
				.Where(e => EF.Property<string>(e, propertyName).Contains(value))
				.ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await context.Set<T>().FindAsync(id);
		}

		
		public async Task<int> UpdateAsync(T entity)
		{
			
			 context.Set<T>().Update(entity);
			return await context.SaveChangesAsync();
		}
	}
}

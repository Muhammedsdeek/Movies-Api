namespace Movies.Services.Interfaces
{
	public interface Igeneric<T> where T : class
	{

		Task<IEnumerable<T>> GetAllAsync();
		Task<T?> GetByIdAsync(int id);
		Task<int> AddAsync(T entity);
		Task<int> UpdateAsync(T entity);
		Task<int> DeleteAsync(int id);

		Task<IEnumerable<T>> GetAllWithName(string value,string propertyName);
	}
}

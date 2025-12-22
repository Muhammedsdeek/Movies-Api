using Movies.Entities;

namespace Movies.Services.ServiceInterfacse
{
	public interface Igenreservice
	{

		Task<IEnumerable<Genre?>> GetAll();
	}
}

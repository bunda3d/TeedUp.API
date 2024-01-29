using TeedUp.API.Models.Domain;

namespace TeedUp.API.Repositories.Interface
{
	public interface ICategoryRepository
	{
		Task<Category> CreateAsync(Category category);

		Task<IEnumerable<Category>> GetAllAsync();

		//Category? => null returned if not found
		Task<Category?> GetByIdAsync(Guid id);
	}
}
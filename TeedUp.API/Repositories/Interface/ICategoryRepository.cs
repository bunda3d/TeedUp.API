using TeedUp.API.Models.Domain;

namespace TeedUp.API.Repositories.Interface
{
	public interface ICategoryRepository
	{
		//Task<Type?> => null to be returned if input param not found

		Task<Category> CreateAsync(Category category);

		Task<IEnumerable<Category>> GetAllAsync();

		Task<Category?> GetByIdAsync(Guid id);

		Task<Category?> UpdateAsync(Category category);

		Task<Category?> DeleteAsync(Guid id);
	}
}
using TeedUp.API.Models.Domain;

namespace TeedUp.API.Repositories.Interface
{
	public interface ICategoryRepository
	{
		Task<Category> CreateAsync(Category category);
	}
}
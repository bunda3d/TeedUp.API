using TeedUp.API.Models.Domain;

namespace TeedUp.API.Repositories.Interface
{
	public interface IBlogPostRepository
	{
		Task<BlogPost> CreateAsync(BlogPost blogPost);

		Task<IEnumerable<BlogPost>> GetAllAsync();
	}
}
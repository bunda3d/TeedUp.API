using TeedUp.API.Models.Domain;

namespace TeedUp.API.Repositories.Interface
{
	public interface IImageRepository
	{
		Task<BlogImage> Upload(IFormFile file, BlogImage blogImg);
	}
}
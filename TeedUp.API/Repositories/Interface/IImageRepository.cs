using System.Collections.Generic;
using TeedUp.API.Models.Domain;

namespace TeedUp.API.Repositories.Interface
{
	public interface IImageRepository
	{
		Task<IEnumerable<BlogImage>> GetAll();

		Task<BlogImage> Upload(IFormFile file, BlogImage blogImg);
	}
}
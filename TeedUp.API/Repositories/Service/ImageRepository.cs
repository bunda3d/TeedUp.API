using Microsoft.EntityFrameworkCore;
using TeedUp.API.Data;
using TeedUp.API.Models.Domain;
using TeedUp.API.Repositories.Interface;

namespace TeedUp.API.Repositories.Service
{
	public class ImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment _webHostEnv;
		private readonly IHttpContextAccessor _httpCtxtAccessor;
		private readonly ApplicationDbContext _context;

		public ImageRepository(
			IWebHostEnvironment webHostEnvironment,
			IHttpContextAccessor httpContextAccessor,
			ApplicationDbContext context
		)
		{
			this._webHostEnv = webHostEnvironment;
			this._httpCtxtAccessor = httpContextAccessor;
			this._context = context;
		}

		public async Task<IEnumerable<BlogImage>> GetAll()
		{
			return await _context.BlogImages.ToListAsync();
		}

		public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImg)
		{
			//1.) upload image to api/images
			var baseDirectory = Path.Combine(_webHostEnv.ContentRootPath, "Assets", "Images");
			if (!Directory.Exists(baseDirectory)) Directory.CreateDirectory(baseDirectory);
			var localPath = Path.Combine(baseDirectory, $"{blogImg.FileName}{blogImg.FileExtension}");
			using var stream = new FileStream(localPath, FileMode.Create);
			await file.CopyToAsync(stream);

			//2.) update database
			//https://{example.com}/images/somefilename.png
			var httpRequest = _httpCtxtAccessor.HttpContext.Request;
			var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Assets/Images/{blogImg.FileName}{blogImg.FileExtension}";

			blogImg.Url = urlPath;

			await _context.BlogImages.AddAsync(blogImg);
			await _context.SaveChangesAsync();

			return blogImg;
		}
	}
}
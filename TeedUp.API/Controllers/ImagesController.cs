using Microsoft.AspNetCore.Mvc;
using TeedUp.API.Models.Domain;
using TeedUp.API.Models.DTO.BlogImage;
using TeedUp.API.Repositories.Interface;

namespace TeedUp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository imageRepository;

		public ImagesController(IImageRepository imageRepository)
		{
			this.imageRepository = imageRepository;
		}

		//GET: {apibaseurl}/api/images
		[HttpGet]
		public async Task<IActionResult> GetAllImages()
		{
			//get all imgs from repo
			var images = await imageRepository.GetAll();

			//convert domain model to dto
			var response = new List<BlogImageDTO>();
			foreach (var image in images)
			{
				response.Add(new BlogImageDTO
				{
					Id = image.Id,
					Title = image.Title,
					CreatedDate = image.CreatedDate,
					FileExtension = image.FileExtension,
					FileName = image.FileName,
					Url = image.Url
				});
			}

			return Ok(response);
		}

		//POST: {apibaseurl}/api/images
		[HttpPost]
		public async Task<IActionResult> UploadImage(
			[FromForm] IFormFile file,
			[FromForm] string fileName,
			[FromForm] string title
		)
		{
			ValidateImageFileUpload(file);

			if (ModelState.IsValid)
			{
				//FILE UPLOAD
				var image = new BlogImage
				{
					FileExtension = Path.GetExtension(file.FileName).ToLower(),
					FileName = fileName,
					Title = title,
					CreatedDate = DateTime.Now
				};

				image = await imageRepository.Upload(file, image);

				//convert domain model to dto
				var response = new BlogImageDTO
				{
					Id = image.Id,
					Title = image.Title,
					CreatedDate = image.CreatedDate,
					FileExtension = image.FileExtension,
					FileName = image.FileName,
					Url = image.Url
				};

				return Ok(response);
			}

			return BadRequest(ModelState);
		}

		private void ValidateImageFileUpload(IFormFile file)
		{
			var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".webp" };

			if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
			{
				ModelState.AddModelError("file", "Unsupported file type for upload ");
			}

			if (file.Length > 26214400)
			{
				ModelState.AddModelError("file", "File size cannot exceed 25MB ");
			}
		}
	}
}
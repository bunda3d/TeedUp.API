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
				var blogImg = new BlogImage
				{
					FileExtension = Path.GetExtension(file.FileName).ToLower(),
					FileName = fileName,
					Title = title,
					CreatedDate = DateTime.Now
				};

				blogImg = await imageRepository.Upload(file, blogImg);

				//convert domain model to dto
				var response = new BlogImageDTO
				{
					Id = blogImg.Id,
					Title = blogImg.Title,
					CreatedDate = blogImg.CreatedDate,
					FileExtension = blogImg.FileExtension,
					FileName = blogImg.FileName,
					Url = blogImg.Url
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
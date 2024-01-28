using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeedUp.API.Data;
using TeedUp.API.Models.Domain;
using TeedUp.API.Models.DTO;
using TeedUp.API.Repositories.Interface;

namespace TeedUp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryRepository categoryRepository;

		public CategoriesController(ICategoryRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryRequestDTO request)
		{
			//map dto to domain model
			var category = new Category
			{
				Name = request.Name,
				UrlHandle = request.UrlHandle
			};

			await categoryRepository.CreateAsync(category);

			//map model to dto after response
			var response = new CategoryDTO
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			};

			return Ok(response);
		}
	}
}
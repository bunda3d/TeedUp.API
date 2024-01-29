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
		public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDTO request)
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


		//GET: https://localhost:7079/api/Categories 
		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			var categories = await categoryRepository.GetAllAsync();

			//map domain model to dto
			var response = new List<CategoryDTO>();
			foreach(var category in categories)
			{
				response.Add(new CategoryDTO
				{
					Id = category.Id,
					Name = category.Name,
					UrlHandle = category.UrlHandle
				});
			}

			return Ok(response);
		}

		//GET: https://localhost:7079/api/Categories/{id}
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
		{
			var existingRecord = await categoryRepository.GetByIdAsync(id);

			if (existingRecord == null) 
				return NotFound();

			var response = new CategoryDTO
			{
				Id = existingRecord.Id,
				Name = existingRecord.Name,
				UrlHandle = existingRecord.UrlHandle
			};
			return Ok(response);
		}
	}
}
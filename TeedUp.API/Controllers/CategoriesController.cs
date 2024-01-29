using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeedUp.API.Data;
using TeedUp.API.Models.Domain;
using TeedUp.API.Models.DTO;
using TeedUp.API.Repositories.Interface;
using TeedUp.API.Repositories.Service;

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
			foreach (var category in categories)
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

		//PUT: https://localhost:7079/api/Categories/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryRequestDTO request)
		{
			//convert DTO to domain model
			var category = new Category
			{
				Id = id,
				Name = request.Name,
				UrlHandle = request.UrlHandle
			};

			category = await categoryRepository.UpdateAsync(category);

			if (category == null)
				return NotFound();

			//convert domain model to DTO
			var response = new CategoryDTO
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			};

			return Ok(response);
		}

		//DELETE: https://localhost:7079/api/Categories/{id}
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
		{
			var category = await categoryRepository.DeleteAsync(id);

			if (category == null) return NotFound();

			//convert domain model to dto
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
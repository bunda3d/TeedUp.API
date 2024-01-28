using Microsoft.EntityFrameworkCore;
using TeedUp.API.Data;
using TeedUp.API.Models.Domain;
using TeedUp.API.Repositories.Interface;

namespace TeedUp.API.Repositories.Service
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext _context;

		public CategoryRepository(ApplicationDbContext _context)
		{
			this._context = _context;
		}

		public async Task<Category> CreateAsync(Category category)
		{
			await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();

			return category;
		}
	}
}
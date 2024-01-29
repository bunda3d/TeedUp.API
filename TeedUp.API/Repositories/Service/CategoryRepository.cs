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

		public async Task<Category?> DeleteAsync(Guid id)
		{
			var existingRecord = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRecord == null)
				return null;

			_context.Categories.Remove(existingRecord);
			await _context.SaveChangesAsync();
			return existingRecord;
		}

		public async Task<IEnumerable<Category>> GetAllAsync()
		{
			return await _context.Categories.ToListAsync();
		}

		public async Task<Category?> GetByIdAsync(Guid id)
		{
			return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Category?> UpdateAsync(Category category)
		{
			var existingRecord = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

			if (existingRecord != null)
			{
				_context.Entry(existingRecord).CurrentValues.SetValues(category);
				await _context.SaveChangesAsync();
				return category;
			}
			return null;
		}
	}
}
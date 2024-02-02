using Microsoft.EntityFrameworkCore;
using TeedUp.API.Data;
using TeedUp.API.Models.Domain;
using TeedUp.API.Repositories.Interface;

namespace TeedUp.API.Repositories.Service
{
	public class BlogPostRepository : IBlogPostRepository
	{
		public readonly ApplicationDbContext _context;

		public BlogPostRepository(ApplicationDbContext _context)
		{
			this._context = _context;
		}

		public async Task<BlogPost> CreateAsync(BlogPost blogPost)
		{
			await _context.BlogPosts.AddAsync(blogPost);
			await _context.SaveChangesAsync();
			return blogPost;
		}

		public async Task<IEnumerable<BlogPost>> GetAllAsync()
		{
			return await _context.BlogPosts.ToListAsync();
		}
	}
}
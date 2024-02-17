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

		public async Task<BlogPost?> DeleteAsync(Guid id)
		{
			var existingBlogPost = await _context.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

			if(existingBlogPost is not null)
			{
				_context.BlogPosts.Remove(existingBlogPost);
				await _context.SaveChangesAsync();
				return existingBlogPost;
			}
			return null;
		}

		public async Task<IEnumerable<BlogPost>> GetAllAsync()
		{
			return await _context.BlogPosts.Include(x => x.Categories).ToListAsync();
		}

		public async Task<BlogPost?> GetByIdAsync(Guid id)
		{
			//include categories
			return await _context.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
		{
			//include categories
			return await _context.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
		}

		public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
		{
			var existingBlogPost = await _context.BlogPosts
				.Include(x=>x.Categories)
				.FirstOrDefaultAsync(x => x.Id == blogPost.Id);

			if (existingBlogPost == null) 
				return null;

			//update blog post
			_context.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);
			//update categories
			existingBlogPost.Categories = blogPost.Categories;
			await _context.SaveChangesAsync();

			return blogPost;
		}
	}
}
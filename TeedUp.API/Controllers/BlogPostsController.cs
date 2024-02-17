using Microsoft.AspNetCore.Mvc;
using TeedUp.API.Models.Domain;
using TeedUp.API.Models.DTO.BlogPost;
using TeedUp.API.Models.DTO.Category;
using TeedUp.API.Repositories.Interface;

namespace TeedUp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostsController : ControllerBase
	{
		private readonly IBlogPostRepository blogPostRepository;
		private readonly ICategoryRepository categoryRepository;

		public BlogPostsController(
			IBlogPostRepository blogPostRepository,
			ICategoryRepository categoryRepository
		)
		{
			this.blogPostRepository = blogPostRepository;
			this.categoryRepository = categoryRepository;
		}

		//POST: {apibaseurl}/api/blogposts
		[HttpPost]
		public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDTO request)
		{
			//convert from dto to domain model
			var blogPost = new BlogPost
			{
				Author = request.Author,
				Content = request.Content,
				DatePublished = request.DatePublished,
				DateUpdated = request.DateUpdated,
				FeaturedImageUrl = request.FeaturedImageUrl,
				IsVisible = request.IsVisible,
				ShortDescription = request.ShortDescription,
				Title = request.Title,
				UrlHandle = request.UrlHandle,
				Categories = new List<Category>()
			};

			foreach (var categoryGuid in request.Categories)
			{
				var existingCategory = await categoryRepository.GetByIdAsync(categoryGuid);
				if (existingCategory != null)
				{
					blogPost.Categories.Add(existingCategory);
				}
			}

			await blogPostRepository.CreateAsync(blogPost);

			//convert domain model back to dto
			var response = new BlogPostDTO
			{
				Id = blogPost.Id,
				Author = blogPost.Author,
				Content = blogPost.Content,
				DatePublished = blogPost.DatePublished,
				DateUpdated = blogPost.DateUpdated,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				IsVisible = blogPost.IsVisible,
				ShortDescription = blogPost.ShortDescription,
				Title = blogPost.Title,
				UrlHandle = blogPost.UrlHandle,
				Categories = blogPost.Categories.Select(x => new CategoryDTO
				{
					Id = x.Id,
					Name = x.Name,
					UrlHandle = x.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}

		//GET: {apibaseurl}/api/blogposts
		[HttpGet]
		public async Task<IActionResult> GetAllBlogPosts()
		{
			var blogPosts = await blogPostRepository.GetAllAsync();

			//convert domain model to dto
			var response = new List<BlogPostDTO>();
			foreach (var blogPost in blogPosts)
			{
				response.Add(new BlogPostDTO
				{
					Id = blogPost.Id,
					Author = blogPost.Author,
					Content = blogPost.Content,
					DatePublished = blogPost.DatePublished,
					DateUpdated = blogPost.DateUpdated,
					FeaturedImageUrl = blogPost.FeaturedImageUrl,
					IsVisible = blogPost.IsVisible,
					ShortDescription = blogPost.ShortDescription,
					Title = blogPost.Title,
					UrlHandle = blogPost.UrlHandle,
					Categories = blogPost.Categories.Select(x => new CategoryDTO
					{
						Id = x.Id,
						Name = x.Name,
						UrlHandle = x.UrlHandle
					}).ToList()
				});
			}

			return Ok(response);
		}

		//GET: {apibaseurl}/api/blogposts/{id}
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
		{
			var blogPost = await blogPostRepository.GetByIdAsync(id);

			if (blogPost is null)
				return NotFound();

			//convert domain model to dto
			var response = new BlogPostDTO
			{
				Id = blogPost.Id,
				Author = blogPost.Author,
				Content = blogPost.Content,
				DatePublished = blogPost.DatePublished,
				DateUpdated = blogPost.DateUpdated,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				IsVisible = blogPost.IsVisible,
				ShortDescription = blogPost.ShortDescription,
				Title = blogPost.Title,
				UrlHandle = blogPost.UrlHandle,
				Categories = blogPost.Categories.Select(x => new CategoryDTO
				{
					Id = x.Id,
					Name = x.Name,
					UrlHandle = x.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}

		//GET: {apibaseurl}/api/blogposts/{url}
		[HttpGet]
		[Route("{urlHandle}")]
		public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
		{
			var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);

			if (blogPost is null)
			{
				return NotFound();
			}

			//convert domain model to dto
			var response = new BlogPostDTO
			{
				Id = blogPost.Id,
				Author = blogPost.Author,
				Content = blogPost.Content,
				DatePublished = blogPost.DatePublished,
				DateUpdated = blogPost.DateUpdated,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				IsVisible = blogPost.IsVisible,
				ShortDescription = blogPost.ShortDescription,
				Title = blogPost.Title,
				UrlHandle = blogPost.UrlHandle,
				Categories = blogPost.Categories.Select(x => new CategoryDTO
				{
					Id = x.Id,
					Name = x.Name,
					UrlHandle = x.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}

		//PUT: {apibaseurl}/api/blogposts/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id, UpdateBlogPostRequestDTO request)
		{
			//convert from dto to domain model
			var blogPost = new BlogPost
			{
				Id = id,
				Author = request.Author,
				Content = request.Content,
				DatePublished = request.DatePublished,
				DateUpdated = DateTime.UtcNow,
				FeaturedImageUrl = request.FeaturedImageUrl,
				IsVisible = request.IsVisible,
				ShortDescription = request.ShortDescription,
				Title = request.Title,
				UrlHandle = request.UrlHandle,
				Categories = new List<Category>()
			};

			foreach (var categoryGuid in request.Categories)
			{
				var existingCategory = await categoryRepository.GetByIdAsync(categoryGuid);

				if (existingCategory != null)
				{
					blogPost.Categories.Add(existingCategory);
				}
			}

			//call repo to update record per domain model
			var updatedBlogPost = await blogPostRepository.UpdateAsync(blogPost);

			if (updatedBlogPost is null)
				return NotFound();

			//convert domain model back to dto
			var response = new BlogPostDTO
			{
				Id = blogPost.Id,
				Author = blogPost.Author,
				Content = blogPost.Content,
				DatePublished = blogPost.DatePublished,
				DateUpdated = blogPost.DateUpdated,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				IsVisible = blogPost.IsVisible,
				ShortDescription = blogPost.ShortDescription,
				Title = blogPost.Title,
				UrlHandle = blogPost.UrlHandle,
				Categories = blogPost.Categories.Select(x => new CategoryDTO
				{
					Id = x.Id,
					Name = x.Name,
					UrlHandle = x.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}

		//DELETE: {apibaseurl}/api/blogposts/{id}
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
		{
			var deletedBlogPost = await blogPostRepository.DeleteAsync(id);

			if (deletedBlogPost is null) return NotFound();

			//convert domain model to dto
			var response = new BlogPostDTO
			{
				Id = deletedBlogPost.Id,
				Author = deletedBlogPost.Author,
				Content = deletedBlogPost.Content,
				DatePublished = deletedBlogPost.DatePublished,
				DateUpdated = deletedBlogPost.DateUpdated,
				FeaturedImageUrl = deletedBlogPost.FeaturedImageUrl,
				IsVisible = deletedBlogPost.IsVisible,
				ShortDescription = deletedBlogPost.ShortDescription,
				Title = deletedBlogPost.Title,
				UrlHandle = deletedBlogPost.UrlHandle
			};

			return Ok(response);
		}
	}
}
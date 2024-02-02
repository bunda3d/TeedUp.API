using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeedUp.API.Models.Domain;
using TeedUp.API.Models.DTO;
using TeedUp.API.Models.DTO.BlogPost;
using TeedUp.API.Repositories.Interface;

namespace TeedUp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostsController : ControllerBase
	{
		private readonly IBlogPostRepository blogPostRepository;

		public BlogPostsController(IBlogPostRepository blogPostRepository)
		{
			this.blogPostRepository = blogPostRepository;
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
			};

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
				UrlHandle = blogPost.UrlHandle
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
			foreach(var blogPost in blogPosts)
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
					UrlHandle = blogPost.UrlHandle
				});
			}

			return Ok(response);
		}
	}
}
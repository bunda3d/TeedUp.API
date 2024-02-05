namespace TeedUp.API.Models.DTO.BlogPost
{
	public class CreateBlogPostRequestDTO
	{
		public string Title { get; set; }
		public string ShortDescription { get; set; }
		public string Content { get; set; }
		public string FeaturedImageUrl { get; set; }
		public string UrlHandle { get; set; }
		public DateTime DatePublished { get; set; }
		public DateTime DateUpdated { get; set; }
		public string Author { get; set; }
		public bool IsVisible { get; set; }

		//FK Many-To-One relation: Multiple category IDs per blog created
		public Guid[] Categories { get; set; }
	}
}
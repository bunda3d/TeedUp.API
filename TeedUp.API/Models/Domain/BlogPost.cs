namespace TeedUp.API.Models.Domain
{
	public class BlogPost
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string ShortDescription { get; set; }
		public string Content { get; set; }
		public string FeaturedImageUrl { get; set; }
		public string UrlHandle { get; set; }
		public DateTime DatePublished { get; set; }
		public DateTime DateUpdated { get; set; } = DateTime.Now;
		public string Author { get; set; }
		public bool IsVisible { get; set; }
	}
}
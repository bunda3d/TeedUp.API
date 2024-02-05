namespace TeedUp.API.Models.Domain
{
	public class Category
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string UrlHandle { get; set; }

		//EF relations
		public ICollection<BlogPost> BlogPosts { get; set; }
	}
}
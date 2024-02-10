namespace TeedUp.API.Models.DTO.BlogImage
{
	public class BlogImageDTO
	{
		public Guid Id { get; set; }
		public string FileName { get; set; }
		public string FileExtension { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal? Latitude { get; set; }
		public decimal? Longitude { get; set; }
		public string Url { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime UpdatedDate { get; set; }
		public string UpdatedBy { get; set; }
	}
}
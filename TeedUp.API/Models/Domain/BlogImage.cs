using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeedUp.API.Models.Domain
{
	public class BlogImage
	{
		public Guid Id { get; set; }

		[Column(TypeName = "VARCHAR")]
		[StringLength(150)]
		public string FileName { get; set; }

		[Column(TypeName = "VARCHAR")]
		[StringLength(10)]
		public string FileExtension { get; set; }

		[Column(TypeName = "VARCHAR")]
		[StringLength(250)]
		public string? Title { get; set; }

		[Column(TypeName = "VARCHAR")]
		[StringLength(2000)]
		public string? Description { get; set; }

		[Column(TypeName = "DECIMAL(12,9)")]
		public decimal? Latitude { get; set; }

		[Column(TypeName = "DECIMAL(12,9)")]
		public decimal? Longitude { get; set; }

		[Column(TypeName = "VARCHAR")]
		[StringLength(500)]
		public string? Url { get; set; }

		public DateTime CreatedDate { get; set; }

		[Column(TypeName = "VARCHAR")]
		[StringLength(30)]
		public string? CreatedBy { get; set; }

		public DateTime? UpdatedDate { get; set; }

		[Column(TypeName = "VARCHAR")]
		[StringLength(30)]
		public string? UpdatedBy { get; set; }
	}
}
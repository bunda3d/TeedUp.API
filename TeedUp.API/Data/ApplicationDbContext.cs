using Microsoft.EntityFrameworkCore;
using TeedUp.API.Models.Domain;

namespace TeedUp.API.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Category> Categories { get; set; }
	}
}
using Adlib.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Adlib.Database.Contexts
{
	public class AdContext : DbContext
	{
		public AdContext(DbContextOptions<AdContext> options) 
			: base(options)
		{
		}

		public DbSet<Ad> Ads { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Ad>()
				.Property(ad => ad.Created)
				.HasDefaultValueSql("CURRENT_TIMESTAMP");
			base.OnModelCreating(modelBuilder);
		}
	}
}

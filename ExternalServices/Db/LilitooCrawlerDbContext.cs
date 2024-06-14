using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExternalServices.Db;

public class LilitooCrawlerDbContext:DbContext
{
	public LilitooCrawlerDbContext(DbContextOptions<LilitooCrawlerDbContext> options) :base(options)
	{
	}

	public DbSet<ProductIns> Products { get; set; }
}

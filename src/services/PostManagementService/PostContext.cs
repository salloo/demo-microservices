
namespace PostService.Data;

using PostService.Model;

public class PostContext : DbContext
{
    public PostContext(DbContextOptions<PostContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }


}
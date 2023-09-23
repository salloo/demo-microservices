
namespace PostService.Data;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PostManagementService.Repository;
using PostService.Model;

public class PostContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    
    public PostContext(DbContextOptions<PostContext> options) : base(options)
    {
    }

    public PostContext(DbContextOptions<PostContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


        System.Diagnostics.Debug.WriteLine("PostContext::ctor ->" + this.GetHashCode());
    }

    public DbSet<Post> Posts { get; set; }

    public async Task<bool> SaveEntitiesAsync(CancellationToken token = default)
    {

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        _ = await base.SaveChangesAsync(token);

        return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<PostContext>
{
    public PostContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostContext>()
            .UseSqlServer("Server=127.0.0.1,5433;Initial Catalog=PostDB;User Id=sa;Password=Pass@word;Encrypt=false");

        return new PostContext(optionsBuilder.Options);
    }

        
    }

}
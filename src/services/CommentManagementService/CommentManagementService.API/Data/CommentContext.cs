
using CommentManagementService.Model;
using CommentManagementService.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class CommentContext: DbContext, IUnitOfWork
{
    public CommentContext(DbContextOptions<CommentContext> options): base(options)
    {
        
    } 
    public CommentContext(DbContextOptions<CommentContext> options, IMediator mediator) : base(options)
        {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


        System.Diagnostics.Debug.WriteLine("CommentContext::ctor ->" + this.GetHashCode());
    }
    private readonly IMediator _mediator;

    public DbSet<Comment> Comments { get; set; }
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

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CommentContext>
    {
        public CommentContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CommentContext>()
                .UseSqlServer("Server=127.0.0.1,5433;Initial Catalog=CommentDB;User Id=sa;Password=Pass@word;Encrypt=false");

            return new CommentContext(optionsBuilder.Options);
        }
    }
}
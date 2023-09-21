namespace PostManagementService.Data;

public interface IUnitOfWork: IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken token = default);
    Task<bool> SaveEntitiesAsync(CancellationToken token = default);
}
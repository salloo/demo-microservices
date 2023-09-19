namespace PostManagementService.Data;

public interface IRepository<T>
{
    IUnitOfWork UnitOfWork { get; }
}
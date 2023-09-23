using PostService.Model;

namespace PostManagementService.Repository;

public interface IRepository<T>
{
    IUnitOfWork UnitOfWork { get; }
}

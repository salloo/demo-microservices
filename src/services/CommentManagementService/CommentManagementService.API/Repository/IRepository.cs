
namespace CommentManagementService.Repository;

public interface IRepository<T>
{
    IUnitOfWork UnitOfWork { get; }
}


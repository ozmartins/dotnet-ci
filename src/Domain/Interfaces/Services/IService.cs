using Domain.Misc;

namespace Domain.Interfaces.Services
{
    public interface IService<TEntity> where TEntity : IEntity
    {
        ServiceResult<TEntity> Delete(Guid id);
        TEntity Get(Guid id);
        List<TEntity> Get();
    }
}

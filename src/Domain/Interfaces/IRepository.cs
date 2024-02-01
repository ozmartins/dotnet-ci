using Domain.Interfaces.Filters;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        public void Create(TEntity entity);

        public void Update(Guid id, TEntity entity);

        void Delete(Guid id);

        public List<TEntity> Recover();

        public List<TEntity> Recover(IFilterBuilder<TEntity> filterBuilder);

        public TEntity RecoverById(Guid id);
    }
}

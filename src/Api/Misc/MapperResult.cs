using Domain.Interfaces;

namespace Api.Infra
{
    public class MapperResult<TEntity> where TEntity : IEntity, new()
    {
        public MapperResult()
        {
            Entity = new TEntity();
            Errors = new List<string>();
        }

        public MapperResult(TEntity entity, List<string> errors)
        {
            Entity = entity;
            Errors = errors;
        }

        public bool Success => !Errors.Any();
        public List<string> Errors { get; private set; }
        public TEntity Entity { get; private set; }

        public void Clear()
        {
            Errors.Clear();
            Entity = new TEntity();
        }

        public void DefineEntity(TEntity entity)
        {
            Entity = entity;
        }
    }
}

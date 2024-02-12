using Domain.Infra;
using Domain.Interfaces;
using Domain.Interfaces.Filters;
using MongoDB.Driver;

namespace Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private IMongoCollection<TEntity> _collection { get; set; }
        private IMongoDatabase _database { get; set; }

        private static FilterDefinition<TEntity> translateToFilterDefinition(List<IFilter<TEntity>> filters)
        {
            var mongoBuilder = Builders<TEntity>.Filter;

            var filterDefinition = mongoBuilder.Empty;

            foreach (var item in filters)
            {
                switch (item.Operator)
                {
                    case FilterOperator.Equal:
                        filterDefinition &= mongoBuilder.Eq(item.GetFieldName(item.Field), item.Value);
                        break;
                    case FilterOperator.Unequal:
                        filterDefinition &= mongoBuilder.Not(mongoBuilder.Eq(item.GetFieldName(item.Field), item.Value));
                        break;
                    case FilterOperator.GreaterThan:
                        filterDefinition &= mongoBuilder.Gt(item.GetFieldName(item.Field), item.Value);
                        break;
                    case FilterOperator.LessThan:
                        filterDefinition &= mongoBuilder.Lt(item.GetFieldName(item.Field), item.Value);
                        break;
                    default:
                        break;
                }
            }

            return filterDefinition;
        }

        public Repository(DatabaseConfig databaseConfig)
        {
            var settings = MongoClientSettings.FromConnectionString(databaseConfig.ConnectionString);

            var client = new MongoClient(settings);

            _database = client.GetDatabase(databaseConfig.DatabaseAlias);

            _collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public void Create(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(Guid id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

            if (RecoverById(id).Version != entity.Version)
            {
                throw new BusinessException("O registro já foi alterado por outro usuário.");
            }

            entity.DefineIdAndVersion(id, Guid.NewGuid());

            _collection.ReplaceOne(filter, entity);
        }

        public void Delete(Guid id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

            var update = Builders<TEntity>.Update.Set(x => x.Removed, true);

            _collection.UpdateOne(filter, update);
        }

        public List<TEntity> Recover(IFilterBuilder<TEntity> filterBuilder)
        {
            var filters = filterBuilder.Build();

            var filterDefinition = Repository<TEntity>.translateToFilterDefinition(filters);

            filterDefinition &= Builders<TEntity>.Filter.Eq(x => x.Removed, false);

            return _collection.Find(filterDefinition).ToList();
        }

        public List<TEntity> Recover()
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Removed, false);

            return _collection.Find(filter).ToList();
        }

        public TEntity RecoverById(Guid id)
        {
            var builder = Builders<TEntity>.Filter;

            var filter = builder.And(builder.Eq(x => x.Id, id), builder.Eq(x => x.Removed, false));

            return _collection.Find(filter).FirstOrDefault<TEntity>();
        }
    }
}
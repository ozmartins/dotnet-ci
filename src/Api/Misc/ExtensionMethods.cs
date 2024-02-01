using Domain.Interfaces;

namespace Api.Infra
{
    public static class ExtensionMethods
    {
        public static MapperResult<T> DefineIdAndVersion<T>(this MapperResult<T> mapperResult, Guid id, Guid version) where T : IEntity, new()
        {
            if (mapperResult.Entity != null)
            {
                mapperResult.Entity.DefineIdAndVersion(id, version);
            }

            return mapperResult;
        }

        public static MapperResult<T> DefineId<T>(this MapperResult<T> mapperResult, Guid id) where T : IEntity, new()
        {
            if (mapperResult.Entity != null)
            {
                mapperResult.Entity.DefineId(id);
            }

            return mapperResult;
        }
    }
}

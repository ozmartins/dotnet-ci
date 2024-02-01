using FluentValidation.Results;
using Domain.Interfaces;

namespace Domain.Misc
{
    public class ServiceResult<TEntity> where TEntity : IEntity
    {
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public TEntity? Entity { get; private set; }
        public static ServiceResult<TEntity> SuccessResult(TEntity entity)
        {
            return new ServiceResult<TEntity>()
            {
                Success = true,
                Errors = new List<string>(),
                Entity = entity
            };
        }

        public static ServiceResult<TEntity> FailureResult(ValidationResult validationResult)
        {
            return new ServiceResult<TEntity>()
            {
                Success = false,
                Errors = validationResult.Errors.Select(p => p.ErrorMessage).ToList(),
            };
        }

        public static ServiceResult<TEntity> FailureResult(string errorMessage)
        {
            return new ServiceResult<TEntity>()
            {
                Success = false,
                Errors = new List<string>() { errorMessage },
            };
        }

        public static ServiceResult<TEntity> FailureResult(List<string> errors)
        {
            return new ServiceResult<TEntity>()
            {
                Success = false,
                Errors = errors,
            };
        }
    }
}

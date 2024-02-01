using FluentValidation;
using Domain.Misc;
using Domain.Interfaces;

namespace Domain.Services
{
    public class BasicService<TEntity> where TEntity : IEntity
    {
        private IRepository<TEntity> _repository;

        private IValidator<TEntity> _validation;

        public BasicService(IRepository<TEntity> repository, IValidator<TEntity> validation)
        {
            _repository = repository;
            _validation = validation;
        }

        public ServiceResult<TEntity> Create(TEntity entity)
        {
            var result = _validation.Validate(entity);

            if (!result.IsValid)
                return ServiceResult<TEntity>.FailureResult(result);

            _repository.Create(entity);

            return ServiceResult<TEntity>.SuccessResult(entity);
        }

        public ServiceResult<TEntity> Update(Guid id, TEntity entity)
        {
            if (Get(entity.Id) == null)
                return ServiceResult<TEntity>.FailureResult("Não foi possível localizar o registro informado.");

            var result = _validation.Validate(entity);

            if (!result.IsValid)
                return ServiceResult<TEntity>.FailureResult(result);

            _repository.Update(id, entity);

            return ServiceResult<TEntity>.SuccessResult(entity);
        }

        public ServiceResult<TEntity> Delete(Guid id)
        {
            var entity = Get(id);

            if (entity == null)
                return ServiceResult<TEntity>.FailureResult("Não foi possível localizar o registro informado.");

            _repository.Delete(id);

            return ServiceResult<TEntity>.SuccessResult(entity);
        }

        public TEntity Get(Guid id)
        {
            return _repository.RecoverById(id);
        }

        public List<TEntity> Get()
        {
            return _repository.Recover();
        }
    }
}

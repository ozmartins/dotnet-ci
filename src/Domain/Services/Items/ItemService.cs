using Domain.Misc;
using Domain.Interfaces.Services;
using Domain.Interfaces.Validations;
using Domain.Models.Items;
using Domain.Interfaces;

namespace Domain.Services.Items
{
    public class ItemService : IItemService
    {
        private readonly BasicService<Item> _basicService;

        private readonly IRepository<Item> _repository;

        private readonly IScheduleValidation _scheduleValidation;

        private readonly IItemScheduleValidation _itemScheduleValidation;

        public ItemService(IRepository<Item> repository, IItemValidation itemValidation, IScheduleValidation scheduleValidation, IItemScheduleValidation itemScheduleValidation)
        {
            _repository = repository;
            _scheduleValidation = scheduleValidation;
            _itemScheduleValidation = itemScheduleValidation;
            _basicService = new BasicService<Item>(repository, itemValidation);
        }

        public ServiceResult<Item> Create(Item item)
        {
            return _basicService.Create(item);
        }

        public ServiceResult<Item> Update(Guid id, Item item)
        {
            return _basicService.Update(id, item);
        }

        public ServiceResult<Item> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public Item Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public List<Item> Get()
        {
            return _basicService.Get();
        }

        public ServiceResult<Item> AddSchedule(Guid itemId, Schedule schedule)
        {
            var item = Get(itemId);

            if (item == null)
                return ServiceResult<Item>.FailureResult("Não foi possível localizar o item informado.");

            var result = _scheduleValidation.Validate(schedule);

            if (!result.IsValid)
                return ServiceResult<Item>.FailureResult(result);

            item.Schedules.Add(schedule);

            result = _itemScheduleValidation.Validate(item);

            if (!result.IsValid)
                return ServiceResult<Item>.FailureResult(result);

            _repository.Update(itemId, item);

            return ServiceResult<Item>.SuccessResult(item);
        }

        public ServiceResult<Item> ReplaceSchedule(Guid itemId, Guid scheduleId, Schedule schedule)
        {
            var item = Get(itemId);

            if (item == null)
                return ServiceResult<Item>.FailureResult("Não foi possível localizar o item informado.");

            var result = _scheduleValidation.Validate(schedule);

            if (!result.IsValid)
                return ServiceResult<Item>.FailureResult(result);

            item.ReplaceSchedule(scheduleId, schedule);

            result = _itemScheduleValidation.Validate(item);

            if (!result.IsValid)
                return ServiceResult<Item>.FailureResult(result);

            _repository.Update(itemId, item);

            return ServiceResult<Item>.SuccessResult(item);
        }

        public ServiceResult<Item> RemoveSchedule(Guid itemId, Guid scheduleId)
        {
            var item = Get(itemId);

            if (item == null)
                return ServiceResult<Item>.FailureResult("Não foi possível localizar o item informado.");

            item.RemoveSchedule(scheduleId);

            var result = _itemScheduleValidation.Validate(item);

            if (!result.IsValid)
                return ServiceResult<Item>.FailureResult(result);

            _repository.Update(itemId, item);

            return ServiceResult<Item>.SuccessResult(item);
        }

        public ServiceResult<Item> IncreaseAvailableQuantity(Guid itemId, decimal quantity)
        {
            var item = Get(itemId);

            if (item == null)
                return ServiceResult<Item>.FailureResult("Não foi possível localizar o item informado.");

            item.ProductInfo.IncreaseQauntity(quantity);

            _repository.Update(itemId, item);

            return ServiceResult<Item>.SuccessResult(item);
        }

        public ServiceResult<Item> DecreaseAvailableQuantity(Guid itemId, decimal quantity)
        {
            var item = Get(itemId);

            if (item == null)
                return ServiceResult<Item>.FailureResult("Não foi possível localizar o item informado.");

            if ((item.ProductInfo.AvailableQuantity - quantity) < 0)
                return ServiceResult<Item>.FailureResult("Não foi possível diminuir o estoque do item, pois sua quantidade ficaria negativa.");

            item.ProductInfo.DecreaseQauntity(quantity);

            _repository.Update(itemId, item);

            return ServiceResult<Item>.SuccessResult(item);
        }
    }
}

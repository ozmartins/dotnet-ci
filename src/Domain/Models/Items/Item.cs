using Domain.Infra;

namespace Domain.Models.Items
{
    public class Item : Entity
    {
        public Item() : base() { }
        public Item(PersonForItem supplier, string sku, string name, string details, object photo, decimal price, MeasurementUnit unit)
        {
            Supplier = supplier;
            SKU = sku;
            Name = name;
            Details = details;
            Photo = photo;
            Price = price;
            Unit = unit;
            Schedules = new List<Schedule>();
        }
        public PersonForItem Supplier { get; private set; } = new PersonForItem();
        public string SKU { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Details { get; private set; } = string.Empty;
        public object? Photo { get; private set; }
        public decimal Price { get; private set; }
        public MeasurementUnit Unit { get; private set; }
        public ProductOrService ProductOrService { get; private set; }
        public Product ProductInfo { get; private set; } = new Product();
        public List<Schedule> Schedules { get; private set; } = new List<Schedule>();
        public void DefineIfIsProductOrService(ProductOrService productOrService, Product productInfo)
        {
            ProductOrService = productOrService;
            if (productOrService == ProductOrService.Product)
                ProductInfo = productInfo;
        }
        public void AddSchedule(Guid scheduleId, Schedule newSchedule)
        {
            newSchedule.DefineIdAndVersion(scheduleId, Guid.NewGuid());

            Schedules.Add(newSchedule);
        }
        public void ReplaceSchedule(Guid scheduleId, Schedule newSchedule)
        {
            var currentSchedule = Schedules.Find(x => x.Id == scheduleId);

            if (currentSchedule == null)
                throw new BusinessException("Não foi possível localizar a agenda informada");

            var index = Schedules.IndexOf(currentSchedule);

            Schedules.Remove(currentSchedule);

            newSchedule.DefineIdAndVersion(scheduleId, Guid.NewGuid());

            Schedules.Insert(index, newSchedule);
        }
        public void RemoveSchedule(Guid scheduleId)
        {
            var currentSchedule = Schedules.Find(x => x.Id == scheduleId);

            if (currentSchedule != null)
                Schedules.Remove(currentSchedule);
            else
                throw new BusinessException("Não foi possível localizar a agenda informada");
        }
    }
}

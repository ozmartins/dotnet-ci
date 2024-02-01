namespace Domain.Models.Bookmark
{
    public class ItemForBookmark
    {
        public ItemForBookmark()
        {
        }
        public ItemForBookmark(Guid id, string sku, string name, object? photo)
        {
            Id = id;
            SKU = sku;
            Name = name;
            Photo = photo;
        }
        public Guid Id { get; private set; }
        public string SKU { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public object? Photo { get; private set; }
    }
}
namespace Domain.Models.Bookmark
{
    public class Bookmark : Entity
    {
        public Bookmark() : base() { }
        public Bookmark(PersonForBookmark customer, ItemForBookmark item, DateTime dateTime)
        {
            Customer = customer;
            Item = item;
            DateTime = dateTime;
        }
        public PersonForBookmark Customer { get; private set; } = new PersonForBookmark();
        public ItemForBookmark Item { get; private set; } = new ItemForBookmark();
        public DateTime DateTime { get; private set; }
    }
}

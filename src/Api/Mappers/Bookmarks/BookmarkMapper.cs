using AutoMapper;
using Api.Dtos.Bookmarks;
using Api.Interfaces.Mappers;
using Api.Views.Bookmarks;
using Domain.Infra.Extensions;
using Domain.Models.Bookmark;
using Domain.Models.Items;
using Domain.Models.People;
using Domain.Interfaces;

namespace Api.Infra.Bookmarks
{
    public class BookmarkMapper : BaseMapper<Bookmark>, IBookmarkMapper
    {
        private readonly IRepository<Person> _personRepository;

        private readonly IRepository<Item> _itemRepository;

        private readonly IMapper _autoMapper;

        public BookmarkMapper(IRepository<Person> personRepository, IRepository<Item> itemRepository, IMapper autoMapper)
        {
            _personRepository = personRepository;
            _itemRepository = itemRepository;
            _autoMapper = autoMapper;
        }

        public MapperResult<Bookmark> Map(BookmarkDto dto)
        {
            var customer = _personRepository.RecoverById(dto.CustomerId).IfNull(() => { AddError("O cliente informado não existe."); });

            var item = _itemRepository.RecoverById(dto.ItemId).IfNull(() => { AddError("O item informado não existe."); });

            if (!SuccessResult()) return GetResult();

            SetEntity(new Bookmark(new PersonForBookmark(customer!.Id, customer.Name), new ItemForBookmark(item!.Id, item.SKU, item.Name, item!.Photo), DateTime.Now));

            return GetResult();
        }

        public BookmarkView? Map(Bookmark? entity)
        {
            return MapToView(entity);
        }

        public List<BookmarkView> Map(List<Bookmark> entities)
        {
            var bookmarks = new List<BookmarkView>();

            foreach (var bookmark in entities)
            {
                var item = MapToView(bookmark);
                if (item != null)
                    bookmarks.Add(item);
            }

            return bookmarks;
        }

        public static BookmarkView? MapToView(Bookmark? entity)
        {
            if (entity == null) return null;

            var bookmarkView = new BookmarkView()
            {
                Id = entity.Id,
                Version = entity.Version,
                CustomerId = entity.Customer.Id,
                ItemId = entity.Item.Id,
                DateTime = entity.DateTime
            };

            return bookmarkView;
        }
    }
}

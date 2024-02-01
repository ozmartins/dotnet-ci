using Api.Dtos.Bookmarks;
using Api.Infra;
using Api.Views.Bookmarks;
using Domain.Models.Bookmark;

namespace Api.Interfaces.Mappers
{
    public interface IBookmarkMapper
    {
        MapperResult<Bookmark> Map(BookmarkDto dto);

        BookmarkView? Map(Bookmark? entity);

        List<BookmarkView> Map(List<Bookmark> entities);
    }
}

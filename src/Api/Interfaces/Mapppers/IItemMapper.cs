using Api.Dtos.Items;
using Api.Infra;
using Api.Views.Items;
using Domain.Models.Items;

namespace Api.Interfaces.Mappers
{
    public interface IItemMapper
    {
        MapperResult<Item> Map(ItemDto dto);

        ItemView? Map(Item? item);

        List<ItemView> Map(List<Item> items);
    }
}

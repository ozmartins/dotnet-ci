using AutoMapper;
using Api.Dtos.Reservations;
using Api.Interfaces.Mappers;
using Api.Views.Reservations;
using Domain.Infra.Extensions;
using Domain.Interfaces;
using Domain.Models.Items;
using Domain.Models.Reservation;

namespace Api.Infra.Reservations
{
    public class ReservationMapper : BaseMapper<Reservation>, IReservationMapper
    {
        private readonly IRepository<Item> _itemRepository;

        private readonly IMapper _autoMapper;

        public ReservationMapper(IRepository<Item> itemRepository, IMapper autoMapper)
        {
            _itemRepository = itemRepository;
            _autoMapper = autoMapper;
        }

        public MapperResult<Reservation> Map(ReservationDto dto)
        {
            var item = _itemRepository.RecoverById(dto.ItemId).IfNull(() => { AddError("O item informado não existe."); });

            if (!SuccessResult())
            {
                return GetResult();
            }

            SetEntity(new Reservation(dto.Date, dto.InitialHour, dto.FinalHour, item!, null, dto.ReservationReason));

            return GetResult();
        }

        public ReservationView? Map(Reservation? entity)
        {
            return MapToView(entity);
        }

        public List<ReservationView> Map(List<Reservation> entities)
        {
            var reservations = new List<ReservationView>();

            foreach (var reservation in entities)
            {
                var item = MapToView(reservation);
                if (item != null)
                    reservations.Add(item);
            }

            return reservations;
        }

        public ReservationView? MapToView(Reservation? entity)
        {
            if (entity == null) return null;

            var reservationView = new ReservationView()
            {
                Id = entity.Id,
                Version = entity.Version,
                Date = entity.Date,
                InitialHour = entity.InitialHour,
                FinalHour = entity.FinalHour,
                ItemId = entity.Item.Id,
                ReservationReason = entity.ReservationReason,
                OrderItemId = Guid.Empty
            };

            return reservationView;
        }
    }
}

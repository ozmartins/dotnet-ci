using AutoMapper;
using Api.Dtos.Addresses;
using Api.Dtos.Cities;
using Api.Dtos.Items;
using Api.Dtos.Messages;
using Api.Dtos.Notifications;
using Api.Dtos.PaymentPlans;
using Api.Dtos.People;
using Api.Dtos.Reviews;
using Api.Dtos.Users;
using Api.Views.Addresses;
using Api.Views.Cities;
using Api.Views.Items;
using Api.Views.Messages;
using Api.Views.Notifications;
using Api.Views.Orders;
using Api.Views.PaymentPlans;
using Api.Views.People;
using Api.Views.Reviews;
using Api.Views.Users;
using Domain.Models.Addresses;
using Domain.Models.Cities;
using Domain.Models.Items;
using Domain.Models.Messages;
using Domain.Models.Notications;
using Domain.Models.Orders;
using Domain.Models.PaymentPlans;
using Domain.Models.People;
using Domain.Models.Review;
using Domain.Models.Users;

namespace Api.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressView>().ReverseMap();

            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<City, CityView>().ReverseMap();

            CreateMap<CityForAddress, CityDto>().ReverseMap();
            CreateMap<CityForAddress, CityView>().ReverseMap();

            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Person, PersonView>().ReverseMap();

            CreateMap<Person, CustomerDto>().ReverseMap();
            CreateMap<Person, CustomerView>().ReverseMap();

            CreateMap<PersonForOrder, CustomerDto>().ReverseMap();
            CreateMap<PersonForOrder, SupplierDto>().ReverseMap();
            CreateMap<PersonForOrder, Person>().ReverseMap();

            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Message, MessageView>().ReverseMap();

            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<Notification, NotificationView>().ReverseMap();

            CreateMap<Phone, PhoneDto>().ReverseMap();
            CreateMap<Phone, PhoneView>().ReverseMap();

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressView>().ReverseMap();

            CreateMap<PaymentPlan, PaymentPlanDto>().ReverseMap();
            CreateMap<PaymentPlan, PaymentPlanView>().ReverseMap();

            CreateMap<PaymentPlanInstalment, PaymentPlanInstalmentDto>().ReverseMap();
            CreateMap<PaymentPlanInstalment, PaymentPlanInstalmentView>().ReverseMap();

            CreateMap<Schedule, ScheduleDto>().ReverseMap();
            CreateMap<Schedule, ScheduleView>().ReverseMap();

            CreateMap<ScheduleItem, ScheduleItemDto>().ReverseMap();
            CreateMap<ScheduleItem, ScheduleItemView>().ReverseMap();

            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, ReviewView>().ReverseMap();

            CreateMap<OrderItem, OrderItemView>().ReverseMap();

            CreateMap<ItemForOrder, ItemDto>().ReverseMap();
            CreateMap<ItemForOrder, Item>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserView>().ReverseMap();

        }
    }
}

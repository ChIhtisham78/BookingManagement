using AutoMapper;
using Shaghaf.Core.Dtos.OrderDtos;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Entities.BookingEntities;
using Shaghaf.Core.Entities.BirthdayEntity;
using Shaghaf.Core.Entities.Cart_Entities;
using Shaghaf.Core.Entities.MembershipEntity;
using Shaghaf.Core.Entities.OrderEntities;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.API.Helpers;
using Shaghaf.Core.Dtos.BirthdayDtos;
using Shaghaf.Core.Dtos.BookingDtos;
using Shaghaf.Core.Dtos.MembershipDtos;
using Shaghaf.Core.Dtos.PaymentDtos;
using Shaghaf.Core.Dtos.PhotoSessionDtos;
using Shaghaf.Core.Dtos.RoomDtos;
using Shaghaf.Core.Dtos.CartDtos;
using Shaghaf.Core.Dtos.MenuItemDtos;

namespace Shaghaf.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // Birthday mappings
            CreateMap<Decoration, DecorationDto>().ReverseMap();
            CreateMap<Cake, CakeDto>().ReverseMap();

            CreateMap<Birthday, BirthdayDto>()
                .ForMember(dest => dest.Cakes, opt => opt.MapFrom(src => src.Cakes))
                .ForMember(dest => dest.Decorations, opt => opt.MapFrom(src => src.Decorations))
                .ReverseMap();

            CreateMap<BirthdayToCreateDto, Birthday>();


            // Room mappings
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.MembershipIds, opt => opt.MapFrom(src => src.Memberships.Select(m => m.Id).ToList()))
                .ReverseMap();

            // PhotoSession mappings
            CreateMap<PhotoSessionToCreateDto, PhotoSession>()
                .ForMember(dest => dest.Room, opt => opt.Ignore());
            CreateMap<PhotoSession, PhotoSessionDto>().ReverseMap();

            // Room mappings
            CreateMap<Room, RoomDto>()
     .ForMember(dest => dest.MembershipIds, opt => opt.MapFrom(src => src.Memberships.Select(m => m.Id).ToList()))
     .ReverseMap();


            CreateMap<RoomToCreateDto, Room>()
                .ForMember(dest => dest.Plan, opt => opt.MapFrom(src => Enum.Parse<RoomPlan>(src.Plan)))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<RoomType>(src.Type)));

            CreateMap<RoomToCreateDto, RoomDto>().ReverseMap();


            // Booking mappings
            CreateMap<BookingToCreateDto, Booking>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<BookingStatus>(src.Status, true)))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders)); // Mapping associated orders
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders)); // Mapping associated orders

            // Payment mappings
            CreateMap<PaymentDto, Booking>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.Discount, opt => opt.Ignore())
                .ReverseMap();

            // Membership mappings

            CreateMap<Membership, MembershipDto>()
     .ForMember(dest => dest.RoomIds, opt => opt.MapFrom(src => src.Rooms.Select(r => r.Id).ToList()))
                .ReverseMap();
            CreateMap<MembershipToCreateDto, Membership>()
       .ForMember(dest => dest.Rooms, opt => opt.Ignore()); // Mapping rooms should be handled separately in your service layer


            // MenuItem mappings
            CreateMap<MenuItemToCreateDto, MenuItem>()
                .ForMember(dest => dest.PictureUrl, opt => opt.Ignore());
            CreateMap<MenuItem, MenuItemDto>();

            // CustomerCart and CartItem mappings
            CreateMap<CartItemDto, CartItem>().ReverseMap();
            CreateMap<CustomerCartDto, CustomerCart>().ReverseMap();

            // Order mappings
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.PaymentIntentId, opt => opt.MapFrom(src => src.PaymentIntentId))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus));
        }
    }
}

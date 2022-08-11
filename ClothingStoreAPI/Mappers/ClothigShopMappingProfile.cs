using AutoMapper;
using ClothingStoreAPI.Entities;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Create;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;

namespace ClothingStoreAPI.Mappers
{
    public class ClothigShopMappingProfile : Profile
    {
        public ClothigShopMappingProfile()
        {
            CreateMap<ClothingStore, ClothingStoreDto>()
                .ForMember(m => m.OwnerContactEmail, c => c.MapFrom(o => o.Owner.ContactEmail))
                .ForMember(m => m.OwnerContactNumber, c => c.MapFrom(o => o.Owner.ContactNumber))
                .ForMember(m => m.Country, c => c.MapFrom(a => a.Address.Country))
                .ForMember(m => m.City, c => c.MapFrom(a => a.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(a => a.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(a => a.Address.PostalCode));

            CreateMap<StoreReview, StoreReviewDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductReview, ProductReviewDto>();

            CreateMap<CreateClothingStoreDto, ClothingStore>()
                .ForMember(s => s.Owner, c => c.MapFrom(dto => new Owner()
                {
                    ContactEmail = dto.OwnerContactEmail,
                    ContactNumber = dto.OwnerContactNumber
                }))
                .ForMember(s => s.Address, c => c.MapFrom(dto => new Address()
                {
                    Country = dto.Country,
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    Street = dto.Street
                }));

            CreateMap<CreateStoreReviewDto, StoreReview>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<CreateProductReviewDto, ProductReview>();

            CreateMap<UpdateClothingStoreDto, ClothingStore>();
            CreateMap<UpdateStoreReviewDto, StoreReview>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<UpdateProductReviewDto, ProductReview>();

            CreateMap<Product, CreateOrderDto>()
                .ForMember(p => p.ProductName, c => c.MapFrom(s => s.Name))
                .ForMember(p => p.ProductPrice, c => c.MapFrom(s => s.Price))
                .ForMember(p => p.ProductSize, c => c.MapFrom(s => s.Size))
                .ForMember(p => p.ProductGender, c => c.MapFrom(s => s.Gender))
                .ForMember(p => p.ProductQuantity, c => c.MapFrom(s => s.Quantity));

            // mapujemy z 1 typu na 2 <1,2> !!!
            CreateMap<CreateOrderDto, Order>();
            CreateMap<Order, OrderDto>();
            CreateMap<Basket, BasketDto>();

        }
    }
}

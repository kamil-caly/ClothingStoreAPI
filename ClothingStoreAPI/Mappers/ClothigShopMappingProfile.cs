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


        }
    }
}
